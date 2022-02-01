using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ApiGateways.Domain.Command.User;
using ApiGateways.Domain.Models.User;

namespace ApiGateways.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthorizationController : BaseController
    {
        [HttpPost]
        public async Task<string> SingUp([FromBody] SingUpCommand command) => await Mediator.Send(command);

        [HttpPost]
        public async Task<UserToken> SingIn([FromBody] SingInCommand command) => await Mediator.Send(command);
    }
}
