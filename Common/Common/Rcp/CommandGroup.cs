using System.Collections.Generic;

namespace Common.Rcp
{
    public abstract class CommandGroup
    {
        public abstract List<ServiceCommand> GetCommands();
        public abstract void AddCommand(ServiceCommand command);
        public abstract void RemoveCommand(string commandName);
        public abstract ServiceCommand FindCommand(string commandName);
    }
}
