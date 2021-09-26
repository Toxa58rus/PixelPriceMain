

using Common.Models.Pixels;
using Contracts.PixelContract.PixelRequest;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;
using PixelService.Context.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PixelService.Command.Consumers.Command
{
    public class ChangePixelColorCommand : IConsumer<ChangePixelColorRequest>
    {
        private readonly PixelContext _dbContext;

        public ChangePixelColorCommand(PixelContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Consume(ConsumeContext<ChangePixelColorRequest> context)
        {
            var request = context.Message;
            var result = new List<PixelColorReslutModel>();

            foreach (var item in request.Pixels)
            {
                var pixelColor = new Context.Models.PixelColor
                {
                    PixelId = item.Id,
                    Color = request.Color,
                    Id = NewId.NextGuid()
                };
                await _dbContext.PixelColors.AddAsync(pixelColor);
                await _dbContext.SaveChangesAsync();

                var resultPixel = new PixelColorReslutModel
                {
                    PixelId = item.Id,
                    Color = _dbContext.PixelColors.Where(t => t.PixelId == item.Id).Select(t => t.Color).ToList()
                };
                result.Add(resultPixel);
            }

            return;
        }
    }

    public class ChangePixelColorCommandDefinition : ConsumerDefinition<ChangePixelColorCommand>
    {
        public ChangePixelColorCommandDefinition()
        {
            EndpointName = "ChangePixelColorCommand";
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<ChangePixelColorCommand> consumerConfigurator)
        {

        }
    }
}
