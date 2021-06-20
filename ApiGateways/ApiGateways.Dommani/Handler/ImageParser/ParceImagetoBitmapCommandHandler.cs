using ApiGateways.Dommain.Command.ImageParser;
using ApiGateways.Service.CommandService.ImageParser;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Common.Models.ImageParser;

namespace ApiGateways.Dommain.Handler.ImageParser
{
    public class ParceImagetoBitmapCommandHandler : IRequestHandler<ParceImagetoBitmapCommand, ImageData>
    {
        private readonly IImageParserServiceCommand _imageParserServiceCommand;

        public ParceImagetoBitmapCommandHandler(IImageParserServiceCommand imageParserServiceCommand)
        {
            _imageParserServiceCommand = imageParserServiceCommand;
        }

        public async Task<ImageData> Handle(ParceImagetoBitmapCommand request, CancellationToken cancellationToken)
        {
            var result = 
                await _imageParserServiceCommand
                    .ParceImagetoBitmap(new ImageData(request.ImageBaseString, request.XCount, request.YCount));
            
            return result;
        }
    }
}
