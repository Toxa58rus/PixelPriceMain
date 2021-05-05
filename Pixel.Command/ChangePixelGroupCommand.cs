using Common.Extensions;
using Common.Models.Pixels;
using Common.Rcp;
using Microsoft.EntityFrameworkCore;
using Pixel.Context;
using System.Threading.Tasks;

namespace Pixel.Command
{
    public class ChangePixelGroupCommand : ServiceCommand
    {
        public override string Name => "ChangerPixelGroup";

        public override async Task<string> Execute(object jsonValue)
        {
            await using var context = new PixelDbContext();

            var value = jsonValue.ToString().DeserializeToObject<ChangePixelsResponceModel>();
            var hasGroup = await context.PixelGroup.AsNoTracking().AnyAsync(s => s.Id == value.GroupId);

            if (hasGroup)
            {
                foreach (var item in value.Pixels)
                {
                    var pixel = await context.Pixels.FirstOrDefaultAsync(s => s.Id == item.Id);
                    pixel.GroupId = value.GroupId;

                    await context.SaveChangesAsync();
                }
            }

            return value.ToJson();
        }
    }
}
