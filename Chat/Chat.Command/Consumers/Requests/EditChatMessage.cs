using System;
using System.Threading.Tasks;
using MassTransit;

namespace ChatService.Command.Consumers.Requests
{
    public class EditChatMessage : IConsumer<string>
    {
/*
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
                message.Edit = true;
                await context.SaveChangesAsync();
            }
            return message.ToJson();
        }*/

        public Task Consume(ConsumeContext<string> context)
        {
	        throw new NotImplementedException();
        }
    }
}
