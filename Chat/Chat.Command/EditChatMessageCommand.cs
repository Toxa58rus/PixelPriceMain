using Chat.Context;
using Common.Extensions;
using Common.Models.Chat;
using Common.Rcp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Command
{
    public class EditChatMessageCommand : ServiceCommand
    {
        public override string Name => "EditMessage";

        public override async Task<string> Execute(object jsonValue)
        {
            await using var context = new ChatDbContext();

            var value = jsonValue.ToString().DeserializeToObject<EditMessageResponseModel>();
            var message = context.ChatMessages.FirstOrDefault(t => t.Id.Equals(value.MessageId) && t.UserId.Equals(value.UserId));
            if (message != null)
            {
                message.Message = value.Text;
                message.EditDate = DateTime.Now.ToLocalTime();
                await context.SaveChangesAsync();
            }
            return message.ToJson();
        }
    }
}
