using System;

namespace ChatService.Context.Models
{

    public class ChatMessage
    {
        public ChatMessage()
        {
        }

        public ChatMessage(string chatId, string userId, string message)
        {
            Id = Guid.NewGuid().ToString();
            ChatId = chatId;
            UserId = userId;
            Message = message;
            WriteDate = DateTime.Now.ToLocalTime();
            Edit = false;
        }

        public string Id { get; set; }
        public string ChatId { get; set; }
        public string UserId { get; set; }
        public string Message { get; set; }
        public DateTime WriteDate { get; set; }
        public DateTime EditDate { get; set; }
        public bool Edit { get; set; }

    }
}
