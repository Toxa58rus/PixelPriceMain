using Common.Extensions;
using Common.Models.Pixels;
using Common.Rcp;
using Microsoft.EntityFrameworkCore;
using Pixel.Context;
using System.Linq;
using System.Threading.Tasks;

namespace Pixel.Command
{
    public class RemovePixelGroupCommand : ServiceCommand
    {
        public override string Name => "RemovePixelGroup";

        public override async Task<string> Execute(object jsonValue)
        {
            await using var context = new PixelDbContext();

            var value = jsonValue.ToString().DeserializeToObject<RemovePixelGroupResponseModel>();
            var group = await context.PixelGroup.FirstOrDefaultAsync(s => s.Id == value.GroupId);

            if (!group.IsDefault)
            {
                var defaultGroup =
                    await context.PixelGroup.FirstOrDefaultAsync(s => s.IsDefault && s.UserId == value.UserId);
                var pixels = context.Pixels.Where(s => s.GroupId == value.GroupId);

                foreach (var item in pixels)
                {
                    item.GroupId = defaultGroup.Id;
                }

                context.PixelGroup.Remove(group);
                await context.SaveChangesAsync();
            }

            return value.ToJson();
        }
    }
}
