using Chat.Context;
using Common.Extensions;
using Common.Models.Chat;
using Common.Rcp;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Command
{
    public class GetChatMessagesCommand : ServiceCommand
    {
        public override string Name => "GetMessages";

        public override async Task<string> Execute(object jsonValue)
        {
            await using var context = new ChatDbContext();

            var value = jsonValue.ToString().DeserializeToObject<GetChatMessagesResponseModel>();
            var chatMessages = context.ChatMessages.Where(t => t.ChatId.Equals(value.ChatId)).ToList();
            return JsonConvert.SerializeObject(chatMessages);
        }
    }
}
