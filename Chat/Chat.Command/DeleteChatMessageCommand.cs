using Chat.Context;
using Common.Extensions;
using Common.Models.Chat;
using Common.Rcp;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Command
{
    public class DeleteChatMessageCommand : ServiceCommand
    {
        public override string Name => "DeleteMessage";

        public override async Task<string> Execute(object jsonValue)
        {
            await using var context = new ChatDbContext();

            var value = jsonValue.ToString().DeserializeToObject<DeleteMessageResponseModel>();
            var message = context.ChatMessages.FirstOrDefault(t => t.Id.Equals(value.MessageId));
            context.ChatMessages.Remove(message);
            await context.SaveChangesAsync();
            return true.ToJson();
        }
    }
}
