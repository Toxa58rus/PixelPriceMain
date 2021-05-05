using Common.Extensions;
using Common.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Common.Rcp.Server
{
    public class RpcServer : IRpcServer, IDisposable
    {
        private readonly IModel _channel;
        private readonly string _queueName;
        private readonly CommandGroup _commands;

        public RpcServer(RpcOptions options, CommandGroup commands)
        {
            _queueName = options.QueueName;
            _commands = commands;

            var factory = new ConnectionFactory
            {
                HostName = options.Host,
                UserName = options.UserName,
                Password = options.Password,
                DispatchConsumersAsync = true
            };

            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();
        }

        public void Start()
        {
            _channel.QueueDeclare(
                _queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            _channel.BasicQos(
                prefetchSize: 0,
                prefetchCount: 1,
                global: false);

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += OnReceived;

            _channel.BasicConsume(
                queue: _queueName,
                autoAck: false,
                consumer);
        }

        private async Task OnReceived(object model, BasicDeliverEventArgs eventArgs)
        {
            string responce = null;

            var body = eventArgs.Body.ToArray();
            var props = eventArgs.BasicProperties;
            var replayProps = _channel.CreateBasicProperties();
            replayProps.CorrelationId = props.CorrelationId;

            try
            {
                var message = Encoding.UTF8.GetString(body);
                var commandResponce = message.DeserializeToObject<CommandResponce>();

                var command = _commands.FindCommand(commandResponce.CommandName);

                if (command != null)
                {
                    responce = await command.Execute(commandResponce.Value);
                }
            }
            catch (Exception ex)
            {
                responce = string.Empty;
            }
            finally
            {
                var responceByte = Encoding.UTF8.GetBytes(responce);

                _channel.BasicPublish(
                    exchange: string.Empty,
                    routingKey: props.ReplyTo,
                    replayProps,
                    responceByte);

                _channel.BasicAck(eventArgs.DeliveryTag, multiple: false);
            }
        }

        public void Dispose()
        {
            _channel.Dispose();
        }
    }
}
