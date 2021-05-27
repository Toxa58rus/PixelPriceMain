using MediatR;

namespace ApiGateways.Dommain.Command.Pixels
{
    public class RemovePixeGrpupCommand : IRequest<bool>
    {
        public string Id { get; set; }
        public string GroupId { get; set; }
    }
}
