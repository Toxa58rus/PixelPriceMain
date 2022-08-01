using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ApiGateways.Domain.Command.ImageParser;

namespace ApiGateways.Controllers
{
    [ApiController]
    [UserAuthorize]
    [Route("[controller]/[action]")]
    public class ImageController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Image([FromBody] ParseImageCommand command) 
	        => await CreateResponse(async() => await Mediator.Send(command));
    }
}
