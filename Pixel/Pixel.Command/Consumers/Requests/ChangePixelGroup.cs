using System;
using System.Threading.Tasks;
using Contracts.PixelContract.PixelRequest;
using Contracts.PixelContract.PixelResponse;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;
using Microsoft.EntityFrameworkCore;
using PixelService.Context;

namespace PixelService.Command.Consumers.Requests
{
    public class ChangePixelGroup : IConsumer<ChangePixelGroupRequest>
    {
        private readonly PixelContext _dbContext;

        public ChangePixelGroup(PixelContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Consume(ConsumeContext<ChangePixelGroupRequest> context)
        {
	        var value = context.Message; 
	        var hasGroup = await _dbContext.PixelGroups.AsNoTracking().AnyAsync(s => s.Id == value.GroupId);

            if (hasGroup)
            {
                foreach (var item in value.PixelIds)
                {
                    var pixel = await _dbContext.Pixels.FirstOrDefaultAsync(s => Guid.Parse(s.Id) == item);
                    if (pixel != null) pixel.GroupId = value.GroupId;

                    await _dbContext.SaveChangesAsync();
                }
            }

            await context.RespondAsync(new CreatePixelGroupResponse() { Name = "Test" });
        }
    }

    public class ChangePixelGroupCommandDefinition : ConsumerDefinition<ChangePixelGroup>
    {
        public ChangePixelGroupCommandDefinition()
        {
            EndpointName = "ChangePixelGroupRequest";
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<ChangePixelGroup> consumerConfigurator)
        {

        }
    }
}
