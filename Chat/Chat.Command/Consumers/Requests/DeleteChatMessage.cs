using System;
using System.Threading.Tasks;
using MassTransit;

namespace ChatService.Command.Consumers.Requests
{
    public class DeleteChatMessage : IConsumer<string>
    {
/*
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
        }*/

        public Task Consume(ConsumeContext<string> context)
        {
	        throw new NotImplementedException();
        }
    }
}
