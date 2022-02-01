using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.PixelContract.PixelRequest;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;
using Microsoft.EntityFrameworkCore;
using PixelService.Context;
using PixelService.Context.Models;

namespace PixelService.Command.Consumers.Requests
{
    public class ChangePixelsOwner : IConsumer<ChangePixelsOwnerRequest>
    {
        private readonly PixelContext _dbContext;

        public ChangePixelsOwner(PixelContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Consume(ConsumeContext<ChangePixelsOwnerRequest> context)
        {
	        var value = context.Message;
            var result = new List<Pixel>();

            foreach (var item in value.PixelIds)
            {
                var pixel = await _dbContext.Pixels.FirstOrDefaultAsync(s => Guid.Parse(s.Id) == item);
                pixel.UserId = value.UserId;

                await _dbContext.SaveChangesAsync();
                result.Add(pixel);
            }
        }


    }
    public class ChangePixelsOwnerCommandDefinition : ConsumerDefinition<ChangePixelsOwner>
    {
        public ChangePixelsOwnerCommandDefinition()
        {
            EndpointName = "ChangePixelGroupOwnerRequest";
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<ChangePixelsOwner> consumerConfigurator)
        {

        }
    }
}
