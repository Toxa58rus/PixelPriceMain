﻿using System.Threading.Tasks;
using Contracts.UserContract.UserEvent;
using MailService.Context;
using MailService.Domain.Infra.DB;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;

namespace MailService.Command.Consumers.Events
{
    public class CreateUser : IConsumer<CreateUserEventDto>
    {
        private readonly MailDbContext _dbContext;

        public CreateUser(MailDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<CreateUserEventDto> context)
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
