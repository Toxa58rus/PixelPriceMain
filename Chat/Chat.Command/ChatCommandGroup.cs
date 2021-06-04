using Chat.Context;
using Common.Rcp;
using System.Collections.Generic;
using System.Linq;

namespace Chat.Command
{
    public class ChatCommandGroup : CommandGroup
    {
        private readonly List<ServiceCommand> _commands;

        public ChatCommandGroup()
        {
            _commands = CreateDefaultDommand();
        }

        public ChatCommandGroup(List<ServiceCommand> commands)
        {
            _commands = commands;
        }

        public override void AddCommand(ServiceCommand command) => _commands.Add(command);

        public override ServiceCommand FindCommand(string commandName) => _commands.FirstOrDefault(s => s.Name == commandName);

        public override List<ServiceCommand> GetCommands() => _commands;

        public override void RemoveCommand(string commandName) => _commands.RemoveAll(s => s.Name == commandName);

        private List<ServiceCommand> CreateDefaultDommand()
        {
            return new()
            {
                new CreateChatCommand()
            };
        }
    }
}
