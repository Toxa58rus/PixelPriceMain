using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Common.Errors;
using Contracts.PixelContract;
using Contracts.PixelContract.PixelRequest;
using Contracts.PixelContract.PixelResponse;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Courier;
using MassTransit.Courier.Contracts;
using MassTransit.Definition;
using PixelService.Command.Consumers.Requests;
using PixelService.Context;
using PixelService.Context.Models;

namespace PixelService.Command.Consumers.Requests
{
	public class GetPixelPart : IConsumer<GetPixelPartRequestDto>
	{
		private readonly PixelContext _dbContext;

		public GetPixelPart(PixelContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task Consume(ConsumeContext<GetPixelPartRequestDto> context)
		{

			var request = context.Message;
			try
			{
				var resultQuery = _dbContext.Pixels.Select(x => new PixelDto()
				{
					Color = x.Color,
					GroupId = x.GroupId,
					Id = x.Id,
					UserId = x.UserId,
					X = x.X.Value,
					Y = x.Y.Value

				}).Where(x =>
					(x.X >= request.StartPositionX && x.Y >= request.StartPositionY) &&
					(x.X <= request.EndPositionX && x.Y <= request.EndPositionY)).ToList();

				await context.RespondAsync(new ResultWithError<GetPixelPartResponseDto>(
					(int)HttpStatusCode.OK,
					null,
					new GetPixelPartResponseDto() { Pixels = resultQuery }));

			}

			catch
			{
				await context.RespondAsync(new ResultWithError<GetPixelPartResponseDto>(
					(int)HttpStatusCode.BadRequest,
					"Ошибка получения пикселей",
					null));


			}
		}
	}
}

public class GetPixelPartDefinition : ConsumerDefinition<GetPixelPart>
{
	public GetPixelPartDefinition()
	{
		EndpointName = "GetPixelPartRequest";
	}

	protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
		IConsumerConfigurator<GetPixelPart> consumerConfigurator)
	{

	}
}

