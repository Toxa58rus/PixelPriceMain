using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateways.Dommain.Handler.Chat;
using Common.Models.Chat;

namespace ApiGateways.Service.CommandService.Chat
{
	public class ChatService : IChatServiceCommand
	{
		public Task<ChatRooms> CreateChatCommand(string createUserId, string joinUserId)
		{
			throw new NotImplementedException();
		}

		public Task<ChatRooms> GetChat(string roomId)
		{
			throw new NotImplementedException();
		}

		public Task<List<ChatMessages>> GetMessages(string chatId)
		{
			throw new NotImplementedException();
		}

		public Task<ChatMessages> SendMessage(string chatId, string userId, string message)
		{
			throw new NotImplementedException();
		}

		public Task<ChatMessages> EditMessage(string messageId, string text, string userId)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteMessage(string messageId, string userId)
		{
			throw new NotImplementedException();
		}

		public Task<List<ChatRooms>> GetChatRooms(string userId)
		{
			throw new NotImplementedException();
		}
	}
}
