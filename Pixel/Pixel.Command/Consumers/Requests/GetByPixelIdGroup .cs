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
    public class GetByPixelIdGroup : IConsumer<GetGroupByPixelIdRequestDto>
    {
        private readonly PixelContext _dbContext;

        public GetByPixelIdGroup(PixelContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Consume(ConsumeContext<GetGroupByPixelIdRequestDto> context)
        {
	        var request = context.Message;
	        try
	        {
		        var group = await _dbContext.Pixels
			        .Join(_dbContext.PixelGroups, x => x.GroupId, y => y.Id,
				        (x, y) => new { y.Name, y.Massage, y.UserId, y.IsDefault, GroupId = y.Id, PixelId = x.Id })
			        .FirstOrDefaultAsync(r => r.PixelId == request.PixelId && r.IsDefault == false);

		       if (group != null)
		       {

			       await context.RespondAsync(new ResultWithError<GetGroupResponseDto>(
				       (int)HttpStatusCode.OK,
				       null,
				       new GetGroupResponseDto()
				       {
					       GroupId = group.GroupId,
					       UserId = group.UserId,
					       Massage = group.Massage,
					       Name = group.Name
				       }));
			       return;
		       }

		       await context.RespondAsync(new ResultWithError<GetGroupResponseDto>(
			       (int)HttpStatusCode.OK,
			       null,
			       null)
		       );

			}
	        catch (Exception ex)
	        {
		         await context.RespondAsync(new ResultWithError<GetGroupResponseDto>((int)HttpStatusCode.BadRequest, null,null));
	        }
        }
    }

    public class GetByPixelIdGroupDefinition : ConsumerDefinition<GetByPixelIdGroup>
    {
        public GetByPixelIdGroupDefinition()
        {
            EndpointName = "GetGroupByPixelIdRequest";
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<GetByPixelIdGroup> consumerConfigurator)
        {

        }
    }
}
