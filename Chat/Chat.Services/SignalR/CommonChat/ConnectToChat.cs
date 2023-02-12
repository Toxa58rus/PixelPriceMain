using ChatService.Context;
using ChatService.Context.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ChatService.Services.SignalR.CommonChat
{
	[Authorize("test")]
	public class CommonChatHub : Hub
	{
		private readonly StorageMessage _storageMessage;

		public class Message
		{
			public string UserId { get; set; }
			public string Text { get; set; }
		}

		public CommonChatHub(StorageMessage storageMessage)
		{
			_storageMessage = storageMessage;
		}
		public override Task OnConnectedAsync()
		{
			Groups.AddToGroupAsync(Context.ConnectionId, "CommonChat");
			return base.OnConnectedAsync();
		}

		public async Task SignalGetAllMessage(Message message)
		{
			var allMessage = _storageMessage.GetAllMessage();
			
			await Clients.Caller.SendAsync("ReceiveAllMessage", allMessage);
		}

		public async Task SignalSendMessageInChat(Message message)
		{
			var temp = new ChatMessage()
			{
				Message = message.Text,
				UserId = message.UserId,
				WriteDate = DateTimeOffset.UtcNow,
			};

			_storageMessage.Add(temp);

			await Clients.Group("CommonChat").SendAsync("ReceiveMessage", temp);
		}
	}
}
