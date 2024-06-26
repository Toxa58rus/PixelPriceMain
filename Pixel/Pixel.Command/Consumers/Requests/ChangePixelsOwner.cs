﻿using System;
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
    public class ChangePixelsOwner : IConsumer<ChangePixelsOwnerRequestDto>
    {
        private readonly PixelContext _dbContext;

        public ChangePixelsOwner(PixelContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Consume(ConsumeContext<ChangePixelsOwnerRequestDto> context)
        {
	        var value = context.Message;
            var result = new List<Pixel>();

            foreach (var item in value.PixelIds)
            {
                var pixel = await _dbContext.Pixels.FirstOrDefaultAsync(s => s.Id == item);
                pixel.UserId = value.UserId;

                await _dbContext.SaveChangesAsync();
                result.Add(pixel);
            }
        }


    }
    public class ChangePixelsOwnerDefinition : ConsumerDefinition<ChangePixelsOwner>
    {
        public ChangePixelsOwnerDefinition()
        {
            EndpointName = "ChangePixelGroupOwnerRequest";
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<ChangePixelsOwner> consumerConfigurator)
        {

        }
    }
}
