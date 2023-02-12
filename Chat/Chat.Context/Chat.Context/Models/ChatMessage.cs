using System;

namespace ChatService.Context.Models
{

    public class ChatMessage
    {
        public ChatMessage()
        {
        }

        public ChatMessage(string userId, string message)
        {
	        UserId = userId;
            Message = message;
            WriteDate = DateTimeOffset.UtcNow;
        }

        public string UserId { get; set; }
        public string Message { get; set; }
        public DateTimeOffset WriteDate { get; set; }

    }
}
