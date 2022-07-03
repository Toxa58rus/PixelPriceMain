using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Common.Errors;
using Contracts.PixelContract;
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
    public class GetPixelByGroupId : IConsumer<GetPixelByGroupIdRequestDto>
    {
        private readonly PixelContext _dbContext;

        public GetPixelByGroupId(PixelContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Consume(ConsumeContext<GetPixelByGroupIdRequestDto> context)
        {
	        var request = context.Message;


	        try
	        {
		        var response = _dbContext.Pixels.AsNoTracking().Select(x => new PixelDto()
		        {
			        GroupId = x.GroupId,
			        Color = x.Color,
			        Id = x.Id,
			        UserId = x.UserId,
			        X = x.X.Value,
			        Y = x.Y.Value
		        }).Where(x => x.GroupId == request.GroupId).ToList();

		        await context.RespondAsync(new ResultWithError<GetPixelByGroupIdResponseDto>(
			        (int)HttpStatusCode.OK,
			        null,
			        new GetPixelByGroupIdResponseDto()
			        {
				        Pixels = response
			        })
		        );
	        }
	        catch (Exception e)
	        {
				await context.RespondAsync(new ResultWithError<GetPixelByGroupIdResponseDto>(
					(int)HttpStatusCode.BadRequest,
					"Ошибка получения пикселей",
					null)
				);
			}
        }
    }
    public class GetPixelByGroupIdDefinition : ConsumerDefinition<GetPixelByGroupId>
    {
        public GetPixelByGroupIdDefinition()
        {
            EndpointName = "GetPixelByGroupIdRequest";
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<GetPixelByGroupId> consumerConfigurator)
        {
        }
    }
}
