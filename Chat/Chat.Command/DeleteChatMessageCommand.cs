using ChatService.Context;
using Common.Extensions;
using Common.Models.Chat;
using Common.Rcp;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ChatService.Command
{
    public class DeleteChatMessageCommand : ServiceCommand
    {
        public override string Name => "DeleteMessage";

        public override async Task<string> Execute(object jsonValue)
        {
            await using var context = new ChatDbContext();

            var value = jsonValue.ToString().DeserializeToObject<DeleteMessageResponseModel>();
            var message = context.ChatMessages.AsNoTracking().FirstOrDefault(t => t.Id.Equals(value.MessageId) && t.UserId.Equals(value.UserId));
            if (message != null)
            {
                context.ChatMessages.Remove(message);
                await context.SaveChangesAsync();
            }
            return true.ToJson();
        }
    }
}
