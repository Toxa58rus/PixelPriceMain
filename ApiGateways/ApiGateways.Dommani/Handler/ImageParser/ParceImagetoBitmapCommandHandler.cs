using ApiGateways.Dommain.Command.ImageParser;
using ApiGateways.Service.CommandService.ImageParser;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApiGateways.Dommain.Handler.ImageParser
{
    public class ParceImagetoBitmapCommandHandler : IRequestHandler<ParceImagetoBitmapCommand, string>
    {
        private readonly IImageParserServiceCommand _imageParserServiceCommand;

        public ParceImagetoBitmapCommandHandler(IImageParserServiceCommand imageParserServiceCommand)
        {
            _imageParserServiceCommand = imageParserServiceCommand;
        }

        public Task<string> Handle(ParceImagetoBitmapCommand request, CancellationToken cancellationToken)
        {
            var result = _imageParserServiceCommand.ParceImagetoBitmap(request.ImageBaseString);
            return result;
        }
    }
}
