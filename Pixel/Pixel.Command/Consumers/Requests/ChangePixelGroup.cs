using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Common.Errors;
using Contracts.PixelContract.PixelRequest;
using Contracts.PixelContract.PixelResponse;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;
using Microsoft.EntityFrameworkCore;
using PixelService.Context;
using PixelService.Context.Models;

namespace PixelService.Command.Consumers.Requests
{
    public class ChangePixelGroup : IConsumer<ChangePixelGroupRequestDto>
    {
        private readonly PixelContext _dbContext;

        public ChangePixelGroup(PixelContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Consume(ConsumeContext<ChangePixelGroupRequestDto> context)
        {
	        var request = context.Message;
	        try
	        {
		        var isExist = await _dbContext.PixelGroups.AnyAsync(s => s.Id == request.GroupId);

		        if (isExist)
		        {
			        var listPixel = await _dbContext.Pixels.AsNoTracking()
						.Select(x => new Pixel()
				        {
					        GroupId = request.GroupId,
					        UserId = x.UserId,
					        Id = x.Id,
					        Color = x.Color,
					        X = x.X,
					        Y = x.Y
				        })
				        .Where(x => request.PixelIds.Contains(x.Id) && x.GroupId !=request.GroupId)
				        .ToListAsync();

			        if (listPixel.Count == 0)
			        {
				        await context.RespondAsync(new ResultWithError((int)HttpStatusCode.OK, null));
				        return;
					}

			        _dbContext.Pixels.AttachRange(listPixel);

					foreach (var pixel in listPixel)
			        {
				        _dbContext.Entry(pixel).Property(nameof(pixel.GroupId)).IsModified = true;
			        }
					
					await _dbContext.SaveChangesAsync();
		        }
	        }
	        catch (Exception ex)
	        {
		         await context.RespondAsync(new ResultWithError((int)HttpStatusCode.BadRequest, null));
		         return;
	        }

	        await context.RespondAsync(new ResultWithError((int)HttpStatusCode.OK, null));
        }
    }

    public class ChangePixelGroupDefinition : ConsumerDefinition<ChangePixelGroup>
    {
        public ChangePixelGroupDefinition()
        {
            EndpointName = "ChangePixelGroupRequest";
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<ChangePixelGroup> consumerConfigurator)
        {

        }
    }
}
