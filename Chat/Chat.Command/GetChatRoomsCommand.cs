using ChatService.Context;
using Common.Extensions;
using Common.Models.Chat;
using Common.Rcp;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatService.Command
{
    public class GetChatRoomsCommand : ServiceCommand
    {
        public override string Name => "GetRooms";

        public override async Task<string> Execute(object jsonValue)
        {
            await using var context = new ChatDbContext();
            var value = jsonValue.ToString().DeserializeToObject<GetRoomsResponseModel>();
            var rooms = context.ChatRooms.Where(t => t.CreateUserId.Equals(value.UserId) || t.JoinUserId.Equals(value.UserId)).AsNoTracking().ToListAsync();
            return rooms.ToJson();
        }
    }
}
