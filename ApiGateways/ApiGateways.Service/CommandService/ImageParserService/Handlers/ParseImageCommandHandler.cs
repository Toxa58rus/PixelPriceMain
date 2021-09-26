using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Dommain.Command.ImageParser;
using ApiGateways.Dommain.Handler;
using ApiGateways.Dommain.Handler.ImageParser;
using Common.Models.ImageParser;
using MediatR;

namespace ApiGateways.Service.CommandService.ImageParserService.Handlers
{
    public class ParceImagetoBitmapHandler : HandlerBase<ParseImageCommand, ImageData>
    {
        private readonly IImageParserServiceCommand _imageParserServiceCommand;

        public ParceImagetoBitmapHandler(IImageParserServiceCommand imageParserServiceCommand)
        {
            _imageParserServiceCommand = imageParserServiceCommand;
        }

        protected override async Task<ImageData> Execute(ParseImageCommand request, CancellationToken cancellationToken)
        {
			var result =
				await _imageParserServiceCommand
					.ParseImage(new ImageData(request.ImageBaseString, request.XCount, request.YCount));

			return result;
		}
    }
}
