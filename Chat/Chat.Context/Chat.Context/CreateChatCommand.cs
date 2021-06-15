using Common.Rcp;
using System.Threading.Tasks;

namespace Chat.Context
{
    public class CreateChatCommand : ServiceCommand
    {
        public override string Name => "CreateChat";

        public override async Task<string> Execute(object jsonValue)
        {
            //TODO надо сделать обработку чата
            return "CreateChat";
        }
    }
}
