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
using Contracts.User.UserEvent;

namespace Mail.Command.Consumers
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
            _dbContext.Mail.Add(new Domain.Model.DB.Mail() 
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
