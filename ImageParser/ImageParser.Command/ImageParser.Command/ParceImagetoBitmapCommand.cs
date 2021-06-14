using Common.Rcp;
using System.Threading.Tasks;

namespace ImageParser.Command
{
    public class ParceImagetoBitmapCommand : ServiceCommand
    {
        public override string Name => "ParceImageToBitmap";

        public override async Task<string> Execute(object jsonValue)
        {
            return string.Empty;
        }
    }
}
