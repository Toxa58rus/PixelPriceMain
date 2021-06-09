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
    public class GetChatRoomCommand : ServiceCommand
    {
        public override string Name => "GetChat";

        public override async Task<string> Execute(object jsonValue)
        {
            await using var context = new ChatDbContext();

            var value = jsonValue.ToString().DeserializeToObject<ChatRooms>();
            
            var roomId = context.ChatRooms.FirstOrDefault(t => (t.CreateUserId.Equals(value.CreateUserId) && t.JoinUserId.Equals(value.JoinUserId)) 
            || (t.CreateUserId.Equals(value.JoinUserId) && t.JoinUserId.Equals(value.CreateUserId)))?.Id ?? null;

            if (String.IsNullOrEmpty(roomId))
            {
                var newRoom = new ChatRooms(value.CreateUserId, value.JoinUserId);
                await context.ChatRooms.AddAsync(newRoom);
                await context.SaveChangesAsync();
                return newRoom.Id.ToJson();
            }
            else return roomId.ToJson();
        }
    }
}
