using Common.Rcp;
using System.Collections.Generic;

namespace ImageParser.Command
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
