using System.Threading.Tasks;
using Contracts.PixelContract.PixelRequest;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;
using PixelService.Context;

namespace PixelService.Command.Consumers.Requests
{
    public class RemovePixelGroup : IConsumer<RemovePixelGroupRequestDto>
    {
        private readonly PixelContext _dbContext;

        public RemovePixelGroup(PixelContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Consume(ConsumeContext<RemovePixelGroupRequestDto> context)
        {
            //var value = context.ToString().DeserializeToObject<RemovePixelGroupResponseModel>();
            //var group = await _dbContext.PixelGroups.FirstOrDefaultAsync(s => s.Id == value.GroupId);
            
            //if (!group.IsDefault)
            //{
            //    var defaultGroup =
            //        await _dbContext.PixelGroups.FirstOrDefaultAsync(s => s.IsDefault && s.UserId == value.UserId);
            //    
            //    var pixels = _dbContext.Pixels.Where(s => s.GroupId == value.GroupId).ToList();
            //
            //    foreach (var item in pixels)
            //    {
            //        item.GroupId = defaultGroup.Id;
            //    }
            //
            //    _dbContext.PixelGroups.Remove(group);
            //    await _dbContext.SaveChangesAsync();
            //}
        }

    }
    public class RemovePixelGroupCommandDefinition : ConsumerDefinition<RemovePixelGroup>
    {
        public RemovePixelGroupCommandDefinition()
        {
            EndpointName = "RemovePixelGroupRequest";
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<RemovePixelGroup> consumerConfigurator)
        {

        }
    }
}
