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
    public class ChangePixelGroupOwnerCommand : ServiceCommand
    {
        public override string Name => "ChangerPixelGroupOwner";

        public override async Task<string> Execute(object jsonValue)
        {
            await using var context = new PixelDbContext();

            var value = jsonValue.ToString().DeserializeToObject<ChangePixelGroupOwnerResponseModel>();
            var result = new List<PixelGroup>();


            foreach (var item in value.Groups)
            {
                if (item.IsDefault == false)
                {
                    var group = await context.PixelGroup.FirstOrDefaultAsync(s => s.Id == item.Id);
                    var pixels = await context.Pixels.Where(s => s.GroupId == item.Id).ToListAsync();

                    foreach (var pixelItem in pixels)
                    {
                        pixelItem.UserId = value.UserId;
                        await context.SaveChangesAsync();
                    }

                    
                    group.UserId = value.UserId;

                    await context.SaveChangesAsync();
                    result.Add(group);
                }
            }
           
            return result.ToJson();
        }
    }
}
