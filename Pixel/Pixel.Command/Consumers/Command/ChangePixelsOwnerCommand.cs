using Common.Extensions;
using Contracts.PixelContract.PixelRequest;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;
using Microsoft.EntityFrameworkCore;
using PixelService.Context.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PixelService.Command.Consumers.Command
{
    public class ChangePixelsOwnerCommand : IConsumer<ChangePixelsOwnerRequest>
    {
        private readonly PixelContext _dbContext;

        public ChangePixelsOwnerCommand(PixelContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Consume(ConsumeContext<ChangePixelsOwnerRequest> context)
        {
            var value = context.ToString().DeserializeToObject<ChangePixelsOwnerRequest>();
            var result = new List<Pixel>();

            foreach (var item in value.Pixels)
            {
                var pixel = await _dbContext.Pixels.FirstOrDefaultAsync(s => Guid.Parse(s.Id) == item.Id);
                pixel.UserId = value.UserId;

                await _dbContext.SaveChangesAsync();
                result.Add(pixel);
            }
        }


    }
    public class ChangePixelsOwnerCommandDefinition : ConsumerDefinition<ChangePixelsOwnerCommand>
    {
        public ChangePixelsOwnerCommandDefinition()
        {
            EndpointName = "ChangePixelGroupOwnerCommand";
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<ChangePixelsOwnerCommand> consumerConfigurator)
        {

        }
    }
}
