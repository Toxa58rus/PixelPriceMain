using GreenPipes;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Linq;
using Contracts.UserContract.UserEvent;
using MailService.Context;
using MailService.Domain.Infra.DB;

namespace MailService.Command.Consumers
{
    public class CreateUser : IConsumer<CreateUserEvent>
    {
        private readonly MailDbContext _dbContext;

        public CreateUser(MailDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<CreateUserEvent> context)
        {
            _dbContext.Mail.Add(new Mail()
            {
                Id = NewId.NextGuid(),
                MailAddress = context.Message.MailAddress,
                UserId = context.Message.Userid
            });

            await _dbContext.SaveChangesAsync();

            return;
        }
    }

    public class CreateUserDefinition : ConsumerDefinition<CreateUser>
    {
        public CreateUserDefinition()
        {
            EndpointName = "CreateUserEventMail";
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<CreateUser> consumerConfigurator)
        {

        }
    }
}
