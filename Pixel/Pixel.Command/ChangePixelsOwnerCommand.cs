using Common.Extensions;
using Common.Models.Pixels;
using Common.Rcp;
using Microsoft.EntityFrameworkCore;
using Pixel.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixel.Command
{
    public class ChangePixelsOwnerCommand : ServiceCommand
    {
        public override string Name => "ChangerPixelsOwner";

        public override async Task<string> Execute(object jsonValue)
        {
            await using var context = new PixelDbContext();

            var value = jsonValue.ToString().DeserializeToObject<ChangePixelsOwnerResponseModel>();
            var result = new List<Pixels>();


            foreach (var item in value.Pixels)
            {
                var pixel = await context.Pixels.FirstOrDefaultAsync(s => s.Id == item.Id);
                pixel.UserId = value.UserId;

                await context.SaveChangesAsync();
                result.Add(pixel);
            }
       
            return result.ToJson();
        }
    }
}
