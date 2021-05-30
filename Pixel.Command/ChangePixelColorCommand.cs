using Common.Extensions;
using Common.Models.Pixels;
using Common.Rcp;
using Pixel.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixel.Command
{
    public class ChangePixelColorCommand : ServiceCommand
    {
        public override string Name => "ChangerPixelColor";

        public override async Task<string> Execute(object jsonValue)
        {
            await using var context = new PixelDbContext();

            var value = jsonValue.ToString().DeserializeToObject<ChangePixelColorResponceModel>();
            var result = new List<PixelColorReslutModel>();

            foreach (var item in value.Pixels)
            {
                var pixelColor = new PixelColor
                {
                    PixelId = item.Id,
                    Color = value.Color,
                    Id = Guid.NewGuid().ToString()
                };
                await context.PixelColor.AddAsync(pixelColor);
                await context.SaveChangesAsync();

                var resultPixel = new PixelColorReslutModel 
                {
                    PixelId = item.Id,
                    Color = context.PixelColor.Where(t => t.PixelId == item.Id).Select(t => t.Color).ToList()
                };
                result.Add(resultPixel);
            }

           

            return result.ToJson();
        }
    }
}
