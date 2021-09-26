
using Common.Extensions;
using Common.Models.Pixels;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;
using Microsoft.EntityFrameworkCore;
using PixelService.Context.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PixelService.Command.Consumers.Command
{
    public class RemovePixelGroupCommand : IConsumer<RemovePixelGroupResponseModel>
    {
        private readonly PixelContext _dbContext;

        public RemovePixelGroupCommand(PixelContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Consume(ConsumeContext<RemovePixelGroupResponseModel> context)
        {
            var value = context.ToString().DeserializeToObject<RemovePixelGroupResponseModel>();
            var group = await _dbContext.PixelGroups.FirstOrDefaultAsync(s => s.Id == value.GroupId);

            if (!group.IsDefault)
            {
                var defaultGroup =
                    await _dbContext.PixelGroups.FirstOrDefaultAsync(s => s.IsDefault && s.UserId == value.UserId);
                
                var pixels = _dbContext.Pixels.Where(s => s.GroupId == value.GroupId).ToList();

                foreach (var item in pixels)
                {
                    item.GroupId = defaultGroup.Id;
                }

                _dbContext.PixelGroups.Remove(group);
                await _dbContext.SaveChangesAsync();
            }
        }

    }
    public class RemovePixelGroupCommandDefinition : ConsumerDefinition<RemovePixelGroupCommand>
    {
        public RemovePixelGroupCommandDefinition()
        {
            EndpointName = "RemovePixelGroupCommand";
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<RemovePixelGroupCommand> consumerConfigurator)
        {

        }
    }
}
