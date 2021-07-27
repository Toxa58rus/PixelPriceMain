using Contracts.Mail.MailEvent;
using Contracts.Mail.MailRequest;
using Contracts.Mail.MailRespounse;
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
using Contracts.User.UserEvent;
using Pixel.Context;

namespace Mail.Command.Consumers
{
    public class CreateUser : IConsumer<CreateUserEvent>
    {
        private readonly PixelDbContext _dbContext;
        private readonly IPublishEndpoint publish;

        public CreateUser(PixelDbContext dbContext, IPublishEndpoint publish)
        {
            _dbContext = dbContext;
            this.publish = publish;
        }

        public async Task Consume(ConsumeContext<CreateUserEvent> context)
        {

            await _dbContext.PixelGroup.AddAsync(
                new Common.Models.Pixels.PixelGroup()
                {
                    Id = NewId.NextGuid().ToString(),
                    Name = "Default user group",
                    IsDefault = true,
                    UserId = context.Message.Userid.ToString()
                });

            await _dbContext.SaveChangesAsync();
            return;
        }
    }

    public class CreateUserDefinition : ConsumerDefinition<CreateUser>
    {
        public CreateUserDefinition()
        {
            EndpointName = "CreateUserEventPixel";
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<CreateUser> consumerConfigurator)
        {

        }
    }
}
