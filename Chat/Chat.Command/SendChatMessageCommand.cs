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
    public class SendChatMessageCommand : ServiceCommand
    {
        public override string Name => "SendMessage";

        public override async Task<string> Execute(object jsonValue)
        {
            await using var context = new ChatDbContext();

            var value = jsonValue.ToString().DeserializeToObject<ChatMessages>();
            await context.ChatMessages.AddAsync(value);
            await context.SaveChangesAsync();
            return value.ToJson();
        }
    }
}
