using Common.Extensions;
using Common.Models.Pixels;
using Common.Rcp;
using Pixel.Context;
using System.Threading.Tasks;

namespace Pixel.Command
{
    public class CreatePixelGroup : ServiceCommand
    {
        public override string Name => "CreatePixelGroup";

        public override async Task<string> Execute(object jsonValue)
        {
            await using var context = new PixelDbContext();

            var value = jsonValue.ToString().DeserializeToObject<PixelGroup>();
            await context.PixelGroup.AddAsync(value);
            await context.SaveChangesAsync();

            return value.ToJson();
        }
    }
}
