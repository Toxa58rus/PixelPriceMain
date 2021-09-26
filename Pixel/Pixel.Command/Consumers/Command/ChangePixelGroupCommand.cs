using Common.Extensions;
using Contracts.PixelContract.PixelRequest;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;
using Microsoft.EntityFrameworkCore;
using PixelService.Context.Models;
using System;
using System.Threading.Tasks;

namespace PixelService.Command.Consumers.Command
{
    public class ChangePixelGroupCommand : IConsumer<ChangePixelGroupRequest>
    {
        private readonly PixelContext _dbContext;

        public ChangePixelGroupCommand(PixelContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Consume(ConsumeContext<ChangePixelGroupRequest> context)
        {
            var value = context.ToString().DeserializeToObject<ChangePixelGroupRequest>();
            var hasGroup = await _dbContext.PixelGroups.AsNoTracking().AnyAsync(s => s.Id == value.GroupId);

            if (hasGroup)
            {
                foreach (var item in value.Pixels)
                {
                    var pixel = await _dbContext.Pixels.FirstOrDefaultAsync(s => Guid.Parse(s.Id) == item.Id);
                    pixel.GroupId = value.GroupId;

                    await _dbContext.SaveChangesAsync();
                }
            }
        }
    }

    public class ChangePixelGroupCommandDefinition : ConsumerDefinition<ChangePixelGroupCommand>
    {
        public ChangePixelGroupCommandDefinition()
        {
            EndpointName = "ChangePixelGroupCommand";
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<ChangePixelGroupCommand> consumerConfigurator)
        {

        }
    }
}
