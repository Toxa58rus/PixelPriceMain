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
            var value = jsonValue.ToString().DeserializeToObject<GetChatResponseModel>();
            var roomId = context.ChatRooms.FirstOrDefault(t => t.Id.Equals(value.RoomId))?.Id ?? null;
            return roomId.ToJson();
        }
    }
}
