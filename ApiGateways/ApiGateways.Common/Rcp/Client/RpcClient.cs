using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Concurrent;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Rcp.Client
{
    public class RpcClient : IRpcClient, IDisposable
    {
        private readonly string _queueName;
        private readonly IModel _channel;
        private readonly string _replayQueueName;
        private readonly EventingBasicConsumer _consumer;

        private readonly ConcurrentDictionary<string, TaskCompletionSource<string>>
            _callbackMapper = new ConcurrentDictionary<string, TaskCompletionSource<string>>();

        public RpcClient(RpcOptions options)
        {
            _queueName = options.QueueName;

            var factory = new ConnectionFactory()
            {
                HostName = options.Host,
                UserName = options.UserName,
                Password = options.Password
            };

            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();
            _replayQueueName = _channel.QueueDeclare().QueueName;

            _consumer = new EventingBasicConsumer(_channel);
            _consumer.Received += OnReceived;
        }

        public Task<string> CallAsync(string message, CancellationToken cancellationToken = default)
        {
            var correlationId = Guid.NewGuid().ToString();
            var tcs = new TaskCompletionSource<string>();
            _callbackMapper.TryAdd(correlationId, tcs);

            var props = _channel.CreateBasicProperties();
            props.CorrelationId = correlationId;
            props.ReplyTo = _replayQueueName;

            var messageBytes = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(
                exchange: string.Empty,
                routingKey: _queueName,
                props,
                messageBytes);

            _channel.BasicConsume(
                consumer: _consumer,
                queue: _replayQueueName,
                autoAck: true);

            cancellationToken.Register(() =>
                _callbackMapper.TryRemove(correlationId, out _));

            return tcs.Task;
        }

        private void OnReceived(object model, BasicDeliverEventArgs eventArgs)
        {
            var suchTaskExists =
                _callbackMapper.TryRemove(eventArgs.BasicProperties.CorrelationId, out var tcs);

            if (!suchTaskExists) return;

            var body = eventArgs.Body.ToArray();
            var responce = Encoding.UTF8.GetString(body);

            tcs.TrySetResult(responce);
        }

        public void Dispose()
        {
            _consumer.Received -= OnReceived;
            _channel.Close();
        }
    }
}
