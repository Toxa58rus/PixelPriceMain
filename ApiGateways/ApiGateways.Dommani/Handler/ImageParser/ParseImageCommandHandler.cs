using ApiGateways.Dommain.Command.ImageParser;
using ApiGateways.Service.CommandService.ImageParser;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Common.Models.ImageParser;
using System;

namespace ApiGateways.Dommain.Handler.ImageParser
{
    public class ParceImagetoBitmapCommandHandler : IRequestHandler<ParseImageCommand, ImageData>
    {
        private readonly IImageParserServiceCommand _imageParserServiceCommand;

        public ParceImagetoBitmapCommandHandler(IImageParserServiceCommand imageParserServiceCommand)
        {
            _imageParserServiceCommand = imageParserServiceCommand;
        }

        public async Task<ImageData> Handle(ParseImageCommand request, CancellationToken cancellationToken)
        {
            var result = 
                await _imageParserServiceCommand
                    .ParseImage(new ImageData(request.ImageBaseString, request.XCount, request.YCount));
            
            return result;
        }
    }
}
