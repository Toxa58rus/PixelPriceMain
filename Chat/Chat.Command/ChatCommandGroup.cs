using Chat.Context;
using Common.Rcp;
using System.Collections.Generic;

namespace Chat.Command
{
    public class ChatCommandGroup : CommandGroup
    {
        public ChatCommandGroup()
        {
            var commands = new List<ServiceCommand>()
            {
                new CreateChatCommand(),
                new GetChatRoomCommand(),
                new GetChatMessagesCommand(),
                new SendChatMessageCommand(),
                new EditChatMessageCommand(),
                new DeleteChatMessageCommand()
            };

            SetDefaultCommands(commands);
        }
    }
}
