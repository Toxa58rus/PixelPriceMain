using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Common.Errors;
using Contracts.ImageParserContract.ImageParserRequest;
using Contracts.ImageParserContract.ImageParserResponse;
using Contracts.PixelContract.PixelRequest;
using Contracts.PixelContract.PixelResponse;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;
using Newtonsoft.Json;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ImageParserService.Command.Consumers.Requests
{
    public class SetImageGroup : IConsumer<SetImageInGroupRequestDto>
    {
	    private readonly IClientFactory _clientFactory;

	    public SetImageGroup(IClientFactory clientFactory)
	    {
		    _clientFactory = clientFactory;
	    }
	    public async Task Consume(ConsumeContext<SetImageInGroupRequestDto> context)
        {
	        var request = context.Message;

	        var requestClient = _clientFactory.CreateRequestClient<GetPixelByGroupIdRequestDto>();

	        var pixelsGroup =  requestClient.GetResponse<ResultWithError<GetPixelByGroupIdResponseDto>>(new GetPixelByGroupIdRequestDto()
	        {
		        GroupId = request.GroupId
	        }).GetAwaiter().GetResult().Message.Result.Pixels;

	        var result = pixelsGroup.OrderBy(x => x.X).GroupBy(y => y.X).ToList().SelectMany(e => e.OrderBy(s => s.Y))
		        .GroupBy(q => q.X).ToList();

			var minValueX = result[result.First().Key].First().X;
	        
	        var maxValueY = result[result.First().Key].Last().Y;

	        foreach (var resultItem in result)
	        {
		        var minValueY = resultItem.First().Y;

				foreach (var item in resultItem)
		        {
			        if (minValueX != item.X)
			        {
				        await context.RespondAsync(new ResultWithError<SetImageInGroupResponseDto>((int)HttpStatusCode.BadRequest,
					        "Группа не является прямоугольной областью",null));
				        return;
					}

			        if (maxValueY < item.Y)
			        {
				        await context.RespondAsync(new ResultWithError<SetImageInGroupResponseDto>((int)HttpStatusCode.BadRequest,
					        "Группа не является прямоугольной областью", null));
				        return;
			        }

			        if (minValueY != item.Y)
			        {
				        await context.RespondAsync(new ResultWithError<SetImageInGroupResponseDto>((int)HttpStatusCode.BadRequest,
					        "Группа не является прямоугольной областью", null));
				        return;
					}

			        minValueY++;
		        }

		        minValueX++;

	        }

	        try
	        {
		        var bytes = Convert.FromBase64String(request.ImageBaseString);

		        await using (var memory = new MemoryStream(bytes))
		        {
			        using (var image = Image.Load(memory, out var format))
			        {
				        var options = new ResizeOptions();

				        options.Size = new Size(minValueX, maxValueY);
				        options.Compand = false;
				        options.Mode = ResizeMode.BoxPad;

				        image.Mutate(x => x.Resize(options));
				        var base64 = image.ToBase64String(format);

				        await context.RespondAsync(new ResultWithError<SetImageInGroupResponseDto>(
					        (int)HttpStatusCode.OK, null, new SetImageInGroupResponseDto()
					        {
						        ImageBaseString = base64,
						        GroupId = request.GroupId,
					        }));
			        }
		        }
	        }
	        catch (Exception ex)
	        {
		         await context.RespondAsync(new ResultWithError<SetImageInGroupResponseDto>((int)HttpStatusCode.BadRequest, ex.Message, null));
		         return;
	        }

        }
    }

    public class SetImageGroupDefinition : ConsumerDefinition<SetImageGroup>
    {
        public SetImageGroupDefinition()
        {
            EndpointName = "SetImageGroupRequest";
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<SetImageGroup> consumerConfigurator)
        {

        }
    }
}
