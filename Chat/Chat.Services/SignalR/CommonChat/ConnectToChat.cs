using ChatService.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ChatService.Services.SignalR.CommonChat
{
	[Authorize("test")]
	public class CommonChatHub : Hub
	{
		private readonly ChatDbContext _dbContext;

		public class Message
		{
			public string Text { get; set; }
		}
		public CommonChatHub(ChatDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public override Task OnConnectedAsync()
		{
			Groups.AddToGroupAsync(Context.ConnectionId, "CommonChat");
			return base.OnConnectedAsync();
		}

		public async Task SendMessageInChat(Message message)
		{
			await Clients.Group("CommonChat").SendAsync("ReceiveMessage", message.Text);
		}
	}
}
