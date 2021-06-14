using System.Collections.Generic;
using System.Linq;

namespace Common.Rcp
{
    public class CommandGroup
    {
        private List<ServiceCommand> _commands;

        public CommandGroup()
        {
        }

        public CommandGroup(List<ServiceCommand> commands)
        {
            _commands = commands;
        }

        public void AddCommand(ServiceCommand command) => _commands.Add(command);

        public ServiceCommand FindCommand(string commandName) => _commands.FirstOrDefault(s => s.Name == commandName);

        public List<ServiceCommand> GetCommands() => _commands;

        public void RemoveCommand(string commandName) => _commands.RemoveAll(s => s.Name == commandName);

        public void SetDefaultCommands(List<ServiceCommand> commands) 
        {
            _commands = commands;
        }
    }
}
