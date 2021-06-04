using Common.Rcp;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pixel.Context;
using System.Linq;
using System.Threading.Tasks;

namespace Pixel.Command
{
    public class GetAllPixelsCommand : ServiceCommand
    {
        public override string Name => "GetAllPixelsCommand";

        public override async Task<string> Execute(object value)
        {
            await using var context = new PixelDbContext();

            var result = context.Pixels.AsNoTracking().ToList();
            return JsonConvert.SerializeObject(result);
        }
    }
}
