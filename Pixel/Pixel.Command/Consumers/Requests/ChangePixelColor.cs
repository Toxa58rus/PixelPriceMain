using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Common.Errors;
using Contracts.PixelContract.PixelRequest;
using Contracts.PixelContract.PixelResponse;
using LinqKit;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Courier;
using MassTransit.Courier.Contracts;
using MassTransit.Definition;
using Microsoft.EntityFrameworkCore;
using PixelService.Context;
using PixelService.Context.Migrations;
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
	        try
            {
	            var ids = request.Pixels.Select(x => x.Id);

	            var pixels = await _dbContext.Pixels.Where(x => ids.Contains(x.Id)).ToListAsync();

                pixels.ForEach(x => x.Color = request.Pixels.First(y => y.Id == x.Id).Color);
	            
	            await _dbContext.SaveChangesAsync();
            }
            catch
            {
	            await context.RespondAsync(new ResultWithError(
		            (int)HttpStatusCode.BadRequest,
		            "Ошибка изменения цвета пикселей")
	            );

				return;
            }

            await context.RespondAsync(new ResultWithError(
				(int)HttpStatusCode.OK,
				null));
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
