using MediatR;

namespace ApiGateways.Dommain.Command.Chat
{
    public class CreateChatCommand : IRequest<string>
    {
        public string UsetId { get; set; }
    }
}
