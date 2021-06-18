using System;

namespace Common.Models.Chat
{

    public class ChatRooms
    {
        public ChatRooms()
        {
        }

        public ChatRooms(string createUserId, string joinUserId)
        {
            Id = Guid.NewGuid().ToString();
            CreateUserId = createUserId;
            JoinUserId = joinUserId;
        }

        public string Id { get; set; }
        public string CreateUserId { get; set; }
        public string JoinUserId { get; set; }
    }
}
