using Common.Rcp;
using System.Collections.Generic;

namespace Pixel.Command
{
    public class PixelCommandGroup : CommandGroup
    {
        public PixelCommandGroup()
        {
            var command = new List<ServiceCommand>
            {
                new GetAllPixelsCommand(),
                new CreatePixelGroupCommand(),
                new ChangePixelGroupCommand(),
                new RemovePixelGroupCommand(),
                new ChangePixelsOwnerCommand(),
                new ChangePixelGroupOwnerCommand(),
                new ChangePixelColorCommand()
            };

            SetDefaultCommands(command);
        }
    }
}
