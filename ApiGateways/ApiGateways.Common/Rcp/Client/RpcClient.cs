using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Concurrent;
using System.Text;

namespace Common.Rcp.Client
{
    public class RpcClient : IRpcClient, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _replyQueueName;
        private readonly EventingBasicConsumer _consumer;
        private readonly BlockingCollection<string> _respQueue;
        private readonly IBasicProperties _selfProps;
        private readonly string _correlationId;
        private readonly string _serverQueueName;

        public RpcClient(RpcOptions options)
        {
            _respQueue = new BlockingCollection<string>();

            var factory = new ConnectionFactory { HostName = options.Host };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            
            _replyQueueName = _channel.QueueDeclare(
                queue: options.QueueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            _consumer = new EventingBasicConsumer(_channel);
            _consumer.Received += ConsumerReceived;
            _serverQueueName = options.QueueName;
            _correlationId = "test"; //Guid.NewGuid().ToString();

            _selfProps = _channel.CreateBasicProperties();
            _selfProps.CorrelationId = _correlationId;
            _selfProps.ReplyTo = _replyQueueName;
        }

        public string Call(string message)
        {
            var messageBytes = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(
                exchange: string.Empty,
                routingKey: _serverQueueName,
                basicProperties: _selfProps,
                body: messageBytes);

            _channel.BasicConsume(
                consumer: _consumer,
                queue: _replyQueueName,
                autoAck: true);

            return _respQueue.Take();
        }

        private void ConsumerReceived(object sender, BasicDeliverEventArgs e)
        {
            var body = e.Body.ToArray();
            var response = Encoding.UTF8.GetString(body);
            
            if (e.BasicProperties.CorrelationId == _correlationId)
                _respQueue.Add(response);
            
            return;
        }

        public void Dispose()
        {
            _consumer.Received -= ConsumerReceived;
            _channel.Close();
            _connection.Close();
        }
    }
}
