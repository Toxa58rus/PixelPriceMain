using System.Threading.Tasks;
using MassTransit;

namespace ChatService.Command.Consumers.Requests
{
    public class GetChatMessages : IConsumer<string>
    {
/*
        public override string Name => "GetMessages";

        public override async Task<string> Execute(object jsonValue)
        {
            await using var context = new ChatDbContext();

            var value = jsonValue.ToString().DeserializeToObject<GetChatMessagesResponseModel>();
            var chatMessages = context.ChatMessages.AsNoTracking().Where(t => t.ChatId.Equals(value.ChatId)).ToListAsync();
            return JsonConvert.SerializeObject(chatMessages);
        }*/

        public Task Consume(ConsumeContext<string> context)
        {
	        throw new System.NotImplementedException();
        }
    }
}
