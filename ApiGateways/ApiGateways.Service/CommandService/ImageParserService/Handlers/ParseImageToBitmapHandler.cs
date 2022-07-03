using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Domain;
using ApiGateways.Domain.Command.ImageParser;
using ApiGateways.Domain.Models.Image.Response;
using ApiGateways.Domain.Services.ImageParser;
using Common.Errors;

namespace ApiGateways.Service.CommandService.ImageParserService.Handlers
{
    public class ParseImageToBitmapHandler : HandlerBase<ParseImageCommand, IResultWithError<ImageDataResponse>>
    {
        private readonly IImageParserService _imageParserService;

        public ParseImageToBitmapHandler(IImageParserService imageParserService)
        {
            _imageParserService = imageParserService;
        }

        protected override async Task<IResultWithError<ImageDataResponse>> Execute(ParseImageCommand request, CancellationToken cancellationToken)
        {
			var result =
				await _imageParserService
                    .SetImageForGroup(request.ImageBaseString, request.GroupId);

			return result;
		}
    }
}
