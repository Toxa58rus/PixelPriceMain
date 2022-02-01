using System.Threading.Tasks;
using Contracts.PixelContract.PixelRequest;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;
using PixelService.Context;
using PixelService.Context.Models;

namespace PixelService.Command.Consumers.Requests
{
    public class CreatePixelGroup : IConsumer<CreatePixelGroupRequest>
    {
        private readonly PixelContext _dbContext;

        public CreatePixelGroup(PixelContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Consume(ConsumeContext<CreatePixelGroupRequest> context)
        {
	        var value = context.Message;
	        var newGroup = new PixelGroup();
            //{
            //    Id = NewId.NextGuid(),
            //    IsDefault = false,
            //    Name = value.Name,
	        //    
            //};
            await _dbContext.PixelGroups.AddAsync(newGroup);
            await _dbContext.SaveChangesAsync();
        }
    }
    public class CreatePixelGroupCommandDefinition : ConsumerDefinition<CreatePixelGroup>
    {
        public CreatePixelGroupCommandDefinition()
        {
            EndpointName = "CreatePixelGroupRequest";
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<CreatePixelGroup> consumerConfigurator)
        {

        }
    }
}
