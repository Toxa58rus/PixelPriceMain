using Contracts.MailContract.MailEvent;
using Contracts.MailContract.MailRespounse;
using GreenPipes;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ImageParserService.Command
{

    public class SendMailCommand : IConsumer<SendMailEvent>
    {
        private readonly IPublishEndpoint publishEndpoint;

        public SendMailCommand(IPublishEndpoint publishEndpoint)
        {

            this.publishEndpoint = publishEndpoint;
        }


        public async Task Consume(ConsumeContext<SendMailEvent> context)
        {

            await publishEndpoint.Publish(new SendMailEvent() { UserId = context.Message.UserId });

            await context.RespondAsync(new SendMailRespounse() { RespounseJson = "asda" });
        }
    }

    /*  public class SendMailCommandDefinition :
      ConsumerDefinition<SendMailCommand>
      {
          public SendMailCommandDefinition()
          {
              EndpointName = "Test";

          }

          protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
              IConsumerConfigurator<SendMailCommand> consumerConfigurator)
          {
              base.ConfigureConsumer(endpointConfigurator, consumerConfigurator);
          }
      }*/
}
