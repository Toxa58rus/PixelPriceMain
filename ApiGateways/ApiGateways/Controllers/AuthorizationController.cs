using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ApiGateways.Domain.Command.User;

namespace ApiGateways.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthorizationController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> SingUp([FromBody] SingUpCommand command) => 
	        await CreateResponse(async () => await Mediator.Send(command));

        [HttpPost]
        public async Task<IActionResult> SingIn([FromBody] SingInCommand command) => 
	        await CreateResponse(async () => await Mediator.Send(command));
    }
}
