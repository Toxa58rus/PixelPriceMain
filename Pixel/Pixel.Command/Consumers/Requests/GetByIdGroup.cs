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
    public class GetByIdGroup : IConsumer<GetGroupByIdRequestDto>
    {
        private readonly PixelContext _dbContext;

        public GetByIdGroup(PixelContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Consume(ConsumeContext<GetGroupByIdRequestDto> context)
        {
	        var request = context.Message;
	        try
	        {
		        var group = await _dbContext.PixelGroups.Where(s => s.Id == request.GroupId).AsNoTracking().FirstOrDefaultAsync();

				if (group != null)
				{

					await context.RespondAsync(new ResultWithError<GetGroupResponseDto>(
						(int)HttpStatusCode.OK,
						null,
						new GetGroupResponseDto()
						{
							GroupId = group.Id,
							UserId = group.UserId,
							Massage = group.Massage,
							Name = group.Name
						}));
					return;
				}

				await context.RespondAsync(new ResultWithError(
					(int)HttpStatusCode.OK,
					null));
			
				return;
			}
	        catch (Exception ex)
	        {
		         await context.RespondAsync(new ResultWithError<GetGroupResponseDto>((int)HttpStatusCode.BadRequest, null,null));
		         return;
	        }
        }
    }

    public class GetGroupByIdDefinition : ConsumerDefinition<GetByIdGroup>
    {
        public GetGroupByIdDefinition()
        {
            EndpointName = "GetGroupByIdRequest";
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<GetByIdGroup> consumerConfigurator)
        {

        }
    }
}
