using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Routing;

namespace ChatService.Services.SignalR.CommonChat
{
	public static class Entry
	{
		public static IEndpointRouteBuilder AddCommonChat(this IEndpointRouteBuilder endpointRouteBuilder)
		{
			endpointRouteBuilder.MapHub<CommonChatHub>("/Chat", conf =>
			{
				conf.Transports = HttpTransportType.WebSockets;
			});
			return endpointRouteBuilder;
		}
	}
}
