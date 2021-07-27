using Contracts.Mail.MailEvent;
using Contracts.Mail.MailRequest;
using Contracts.Mail.MailRespounse;
using GreenPipes;
using Mail.Context;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Mail.Command
{

    public class SendMailCommand : IConsumer<SendMailRequest>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly MailDbContext _dbContext;

        public SendMailCommand(IPublishEndpoint publishEndpoint, MailDbContext dbContext)
        {
            _publishEndpoint = publishEndpoint;
            _dbContext = dbContext;
        }


        public async Task Consume(ConsumeContext<SendMailRequest> context)
        {
            var temp = await _dbContext.Mail.FirstOrDefaultAsync(x => x.UserId == context.Message.UserId);

            await _publishEndpoint.Publish(new SendMailEvent() { UserId = context.Message.UserId });

            await context.RespondAsync(new SendMailRespounse() { RespounseJson = "asda" });
        }
    }

    public class SendMailCommandDefinition :
    ConsumerDefinition<SendMailCommand>
    {
        public SendMailCommandDefinition()
        {
            EndpointName = "SendMailCommand";
        }
        
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<SendMailCommand> consumerConfigurator)
        {
           
        }
    }
}
