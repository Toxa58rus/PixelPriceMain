using ApiGateways.Common.Models.User;
using ApiGateways.Domman.Command;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiGateways.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthorizationController : BaseController
    {
        [HttpPost]
        public async Task<Users> SingUp([FromBody] SingUpCommand command) => await Mediator.Send(command);

        [HttpPost]
        public async Task<UserToken> SingIn([FromBody] SingInCommand command) => await Mediator.Send(command);
    }
}
