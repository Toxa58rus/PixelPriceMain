using Newtonsoft.Json;
using NLog;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using Common.Models.Pixels;

namespace Common.Rcp.Server
{
    public class RpcServer : IRpcServer, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly EventingBasicConsumer _consumer;
        private readonly string _queueName;

        public RpcServer(RpcOptions options)
        {
            _queueName = options.QueueName;

            var factory = new ConnectionFactory { HostName = options.Host };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _consumer = new EventingBasicConsumer(_channel);
            _consumer.Received += ConsumerReceived;
        }

        public void Start()
        {
            _channel.QueueDeclare(queue: _queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            _channel.BasicQos(
                prefetchSize: 0,
                prefetchCount: 1,
                global: false);

            _channel.BasicConsume(queue: _queueName,
                autoAck: false,
                consumer: _consumer);
        }

        private void ConsumerReceived(object sender, BasicDeliverEventArgs e)
        {
            var response = JsonConvert.SerializeObject(new List<Pixels>());

            var body = e.Body.ToArray();
            var props = e.BasicProperties;
            var replyProps = _channel.CreateBasicProperties();

            replyProps.CorrelationId = props.CorrelationId;

            try
            {
                var message = Encoding.UTF8.GetString(body);

                // тут бахаем обработку команд

            }
            catch (Exception ex)
            {
                response = "";
            }
            finally
            {
                var responceBytes = Encoding.UTF8.GetBytes(response);
                
                _channel.BasicPublish(
                    exchange: "",
                    routingKey: _queueName,
                    basicProperties: replyProps,
                    body: responceBytes);

                _channel.BasicAck(deliveryTag: e.DeliveryTag, multiple: false);
            }
        }

        public void Dispose()
        {
            _consumer.Received += ConsumerReceived; 
            _channel.Dispose();
            _connection.Dispose();
        }
    }
}
