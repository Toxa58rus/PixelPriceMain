using System.Threading.Tasks;
using Contracts.MailContract.MailEvent;
using Contracts.MailContract.MailRequest;
using Contracts.MailContract.MailRespounse;
using MailService.Context;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;
using Microsoft.EntityFrameworkCore;

namespace MailService.Command.Consumers.Requests
{

    public class SendMail : IConsumer<SendMailRequestDto>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly MailDbContext _dbContext;

        public SendMail(IPublishEndpoint publishEndpoint, MailDbContext dbContext)
        {
            _publishEndpoint = publishEndpoint;
            _dbContext = dbContext;
        }


        public async Task Consume(ConsumeContext<SendMailRequestDto> context)
        {
            var temp = await _dbContext.Mail.FirstOrDefaultAsync(x => x.UserId == context.Message.UserId);

            await _publishEndpoint.Publish(new SendMailEventDto()
            {
	            UserId = context.Message.UserId
            });

            await context.RespondAsync(new SendMailRespounseDto()
            {
	            RespounseJson = "asda"
            });
        }
    }

    public class SendMailCommandDefinition : ConsumerDefinition<SendMail>
    {
        public SendMailCommandDefinition()
        {
            EndpointName = "SendMailRequest";
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<SendMail> consumerConfigurator)
        {

        }
    }
}
