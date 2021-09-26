using ChatService.Context;
using Common.Extensions;
using Common.Models.Chat;
using Common.Rcp;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatService.Command
{
    public class GetChatMessagesCommand : ServiceCommand
    {
        public override string Name => "GetMessages";

        public override async Task<string> Execute(object jsonValue)
        {
            await using var context = new ChatDbContext();

            var value = jsonValue.ToString().DeserializeToObject<GetChatMessagesResponseModel>();
            var chatMessages = context.ChatMessages.AsNoTracking().Where(t => t.ChatId.Equals(value.ChatId)).ToListAsync();
            return JsonConvert.SerializeObject(chatMessages);
        }
    }
}
