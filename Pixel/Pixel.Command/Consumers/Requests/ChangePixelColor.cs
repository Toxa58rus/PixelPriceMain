using System;
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
using Microsoft.EntityFrameworkCore;
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
		        .All(x => x.UserId == request.UserId);
	        
            if (!isExist)
	        {

		        await context.RespondAsync(new ResultWithError<ChangePixelColorResponseDto>(
			        (int)HttpStatusCode.BadRequest,
			        null,
			        null)
		        );

		        return;
	        }

            try
            {

	            var pixelsId = request.Pixels.Select(x => x.Id).ToList();
	            var pixels =  _dbContext.Pixels.Where(x => pixelsId.Contains(x.Id)).ToList();
	           
	           pixels.ForEach(x => x.Color = request.Pixels.First(t => t.Id == x.Id).Color);

	           await _dbContext.SaveChangesAsync();
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

    public class ChangePixelColorDefinition : ConsumerDefinition<ChangePixelColor>
    {
        public ChangePixelColorDefinition()
        {
            EndpointName = "ChangePixelColorRequest";
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<ChangePixelColor> consumerConfigurator)
        {

        }
    }
}
