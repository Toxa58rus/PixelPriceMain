using System;

namespace ChatService.Context.Models
{
	public class ChatRoom 
    {
        public ChatRoom()
        {
        }

        public ChatRoom(string createUserId, string joinUserId)
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
