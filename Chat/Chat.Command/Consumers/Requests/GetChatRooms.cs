using System;
using System.Threading.Tasks;
using MassTransit;

namespace ChatService.Command.Consumers.Requests
{
    public class GetChatRooms : IConsumer<string>
    {
/*
        public override string Name => "GetRooms";

        public override async Task<string> Execute(object jsonValue)
        {
            await using var context = new ChatDbContext();
            var value = jsonValue.ToString().DeserializeToObject<GetRoomsResponseModel>();
            var rooms = context.ChatRooms.Where(t => t.CreateUserId.Equals(value.UserId) || t.JoinUserId.Equals(value.UserId)).AsNoTracking().ToListAsync();
            return rooms.ToJson();
        }*/

        public Task Consume(ConsumeContext<string> context)
        {
	        throw new NotImplementedException();
        }
    }
}
