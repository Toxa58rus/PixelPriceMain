using ChatService.Context;
using Common.Extensions;
using Common.Models.Chat;
using Common.Rcp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatService.Command
{
    public class CreateChatCommand : ServiceCommand
    {
        public override string Name => "CreateChat";

        public override async Task<string> Execute(object jsonValue)
        {
            await using var context = new ChatDbContext();
            var value = jsonValue.ToString().DeserializeToObject<ChatRooms>();
            await context.ChatRooms.AddAsync(value);
            await context.SaveChangesAsync();
            return value.ToJson();
        }
    }
}
