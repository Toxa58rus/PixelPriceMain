using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Domain;
using ApiGateways.Domain.Command.ImageParser;
using ApiGateways.Domain.Models.ImageParser;
using ApiGateways.Domain.Services.ImageParser;

namespace ApiGateways.Service.CommandService.ImageParserService.Handlers
{
    public class ParceImagetoBitmapHandler : HandlerBase<ParseImageCommand, ImageData>
    {
        private readonly IImageParserService _imageParserService;

        public ParceImagetoBitmapHandler(IImageParserService imageParserService)
        {
            _imageParserService = imageParserService;
        }

        protected override async Task<ImageData> Execute(ParseImageCommand request, CancellationToken cancellationToken)
        {
			var result =
				await _imageParserService
                    .ParseImage(new ImageData(request.ImageBaseString, request.XCount, request.YCount));

			return result;
		}
    }
}
