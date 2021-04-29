using Common.Rcp;
using System.Collections.Generic;
using System.Linq;

namespace Pixel.Command
{
    public class PixelCommandGroup : CommandGroup
    {
        private readonly List<ServiceCommand> _commands;

        public PixelCommandGroup()
        {
            _commands = CreateDefaultDommand();
        }

        public PixelCommandGroup(List<ServiceCommand> commands)
        {
            _commands = commands;
        }

        public override void AddCommand(ServiceCommand command) => _commands.Add(command);

        public override ServiceCommand FindCommand(string commandName) => _commands.FirstOrDefault(s => s.Name == commandName);

        public override List<ServiceCommand> GetCommands() => _commands;

        public override void RemoveCommand(string commandName) => _commands.RemoveAll(s => s.Name == commandName);

        private List<ServiceCommand> CreateDefaultDommand()
        {
            return new List<ServiceCommand>
            {
                new GetAllPixelsCommand()
            };
        }
    }
}
