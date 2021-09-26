using Common.Rcp;
using System.Collections.Generic;

namespace ImageParserService.Command
{
    public class ImageParserCommandGroup : CommandGroup
    {
        public ImageParserCommandGroup()
        {
            var command = new List<ServiceCommand>
            {
                new ParseImageCommand()
            };

            SetDefaultCommands(command);
        }
    }
}
