using Common.Extensions;
using Contracts.PixelContract.PixelRequest;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;
using PixelService.Context.Models;
using System.Threading.Tasks;

namespace PixelService.Command.Consumers.Command
{
    public class CreatePixelGroupCommand : IConsumer<CreatePixelGroupRequest>
    {
        private readonly PixelContext _dbContext;

        public CreatePixelGroupCommand(PixelContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Consume(ConsumeContext<CreatePixelGroupRequest> context)
        {
            var value = context.ToString().DeserializeToObject<CreatePixelGroupRequest>();
            var newGroup = new PixelGroup()
            {
                Id = NewId.NextGuid(),
                IsDefault = false,
                Name = value.Name,
                UserId = value.UserId
            };
            await _dbContext.PixelGroups.AddAsync(newGroup);
            await _dbContext.SaveChangesAsync();
        }
    }
    public class CreatePixelGroupCommandDefinition : ConsumerDefinition<CreatePixelGroupCommand>
    {
        public CreatePixelGroupCommandDefinition()
        {
            EndpointName = "CreatePixelGroupCommand";
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<CreatePixelGroupCommand> consumerConfigurator)
        {

        }
    }
}
