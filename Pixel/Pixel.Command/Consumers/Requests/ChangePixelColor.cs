using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.PixelContract.PixelRequest;
using Contracts.PixelContract.PixelResponse;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;
using PixelService.Context;

namespace PixelService.Command.Consumers.Requests
{
    public class ChangePixelColor : IConsumer<ChangePixelColorRequest>
    {
        private readonly PixelContext _dbContext;

        public ChangePixelColor(PixelContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Consume(ConsumeContext<ChangePixelColorRequest> context)
        {
            var request = context.Message;
            //var result = new List<PixelColorReslutModel>();

            //foreach (var item in request.PixelIds)
            //{
            //    var pixelColor = new Context.Models.PixelColor
            //    {
            //        PixelId = item,
            //        Color = request.Color,
            //        Id = NewId.NextGuid()
            //    };
            //    await _dbContext.PixelColors.AddAsync(pixelColor);
            //    await _dbContext.SaveChangesAsync();
            //
            //    var resultPixel = new PixelColorReslutModel
            //    {
            //        PixelId = item,
            //        Color = _dbContext.PixelColors.Where(t => t.PixelId == item).Select(t => t.Color).ToList()
            //    };
            //    result.Add(resultPixel);
            //}

            await context.RespondAsync(new ChangePixelColorResponse() {Color = 1});
        }
    }

    public class ChangePixelColorCommandDefinition : ConsumerDefinition<ChangePixelColor>
    {
        public ChangePixelColorCommandDefinition()
        {
            EndpointName = "ChangePixelColorRequest";
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<ChangePixelColor> consumerConfigurator)
        {

        }
    }
}
