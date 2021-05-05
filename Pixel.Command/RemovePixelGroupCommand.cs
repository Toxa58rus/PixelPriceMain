using Common.Rcp;
using System;
using System.Threading.Tasks;

namespace Pixel.Command
{
    public class RemovePixelGroup : ServiceCommand
    {
        public override string Name => "RemovePixelGroup";

        public override Task<string> Execute(object jsonValue)
        {
            throw new NotImplementedException();
        }
    }
}
