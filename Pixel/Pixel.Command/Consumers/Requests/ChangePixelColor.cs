﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Common.Errors;
using Contracts.PixelContract.PixelRequest;
using Contracts.PixelContract.PixelResponse;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Courier;
using MassTransit.Courier.Contracts;
using MassTransit.Definition;
using PixelService.Context;
using PixelService.Context.Models;

namespace PixelService.Command.Consumers.Requests
{
    public class ChangePixelColor : IConsumer<ChangePixelColorRequestDto>
    {
        private readonly PixelContext _dbContext;

        public ChangePixelColor(PixelContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<ChangePixelColorRequestDto> context)
        {

	        var request = context.Message;

			var isExist = _dbContext.Pixels
		        .Join(_dbContext.PixelGroups, x => x.GroupId, y => y.Id, (x, y) => x)
		        .Any(x => request.PixelIds.Contains(x.Id));
	        
            if (!isExist)
	        {

		        await context.RespondAsync(new ResultWithError<ChangePixelColorResponseDto>(
			        (int)HttpStatusCode.BadRequest,
			        "Выбранные пиксели отсутвуют в данной группе",
			        null)
		        );

		        return;
	        }

            try
            {


	            var listChangeData = request.PixelIds.Select(x =>
		            new PixelColor()
		            {
			            Id = x,
			            Color = request.Color
		            }).ToList();

	            _dbContext.PixelColors.AttachRange(listChangeData);

	            foreach (var color in listChangeData)
	            {
		            _dbContext.Entry(color).Property(nameof(color.Color)).IsModified = true;
	            }

	            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            catch
            {
	            await context.RespondAsync(new ResultWithError<ChangePixelColorResponseDto>(
		            (int)HttpStatusCode.BadRequest,
		            "Ошибка изменения цвета пикселей",
		            null));

				return;
            }

            await context.RespondAsync(new ResultWithError<ChangePixelColorResponseDto>(
				(int)HttpStatusCode.OK,
				null,
				new ChangePixelColorResponseDto() { Color = request.Color }));
        }
    }

    public class ChangePixelColorCommandDefinition : ConsumerDefinition<ChangePixelColor>
    {
        public ChangePixelColorCommandDefinition()
        {
            EndpointName = "ChangePixelColorRequest";
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<ChangePixelColor> consumerConfigurator)
        {

        }
    }
}