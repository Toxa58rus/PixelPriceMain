using System;
using System.Threading.Tasks;
using MassTransit;

namespace ChatService.Command.Consumers.Requests
{
    public class GetChatRoom : IConsumer<string>
    {
/*
        public override string Name => "GetChat";

        public override async Task<string> Execute(object jsonValue)
        {
            await using var context = new ChatDbContext();
            var value = jsonValue.ToString().DeserializeToObject<GetChatResponseModel>();
            var roomId = context.ChatRooms.FirstOrDefault(t => t.Id.Equals(value.RoomId))?.Id ?? null;
            return roomId.ToJson();
        }*/

        public Task Consume(ConsumeContext<string> context)
        {
	        throw new NotImplementedException();
        }
    }
}
