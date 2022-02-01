using Contracts.UserContract.UserEvent;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;
using PixelService.Context.Models;
using System.Threading.Tasks;
using PixelService.Context;

namespace PixelService.Command.Consumers.Event
{
    public class CreateUser : IConsumer<CreateUserEvent>
    {
        private readonly PixelContext _dbContext;
        private readonly IPublishEndpoint publish;

        public CreateUser(PixelContext dbContext, IPublishEndpoint publish)
        {
            _dbContext = dbContext;
            this.publish = publish;
        }

        public async Task Consume(ConsumeContext<CreateUserEvent> context)
        {

            await _dbContext.PixelGroups.AddAsync(
                new PixelGroup()
                {
                    Id = NewId.NextGuid(),
                    Name = "Default user group",
                    IsDefault = true,
                    UserId = context.Message.Userid
                });

            await _dbContext.SaveChangesAsync();
        }
    }

    public class CreateUserDefinition : ConsumerDefinition<CreateUser>
    {
        public CreateUserDefinition()
        {
            EndpointName = "CreateUserEvent";
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<CreateUser> consumerConfigurator)
        {

        }
    }
}
