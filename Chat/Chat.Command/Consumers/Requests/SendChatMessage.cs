using System;
using System.Threading.Tasks;
using MassTransit;

namespace ChatService.Command.Consumers.Requests
{
    public class SendChatMessage : IConsumer<string>
    {
/*
        public override string Name => "SendMessage";

        public override async Task<string> Execute(object jsonValue)
        {
            await using var context = new ChatDbContext();

            var value = jsonValue.ToString().DeserializeToObject<ChatMessage>();
            await context.ChatMessages.AddAsync(value);
            await context.SaveChangesAsync();
            return value.ToJson();
        }*/

        public Task Consume(ConsumeContext<string> context)
        {
	        throw new NotImplementedException();
        }
    }
}
