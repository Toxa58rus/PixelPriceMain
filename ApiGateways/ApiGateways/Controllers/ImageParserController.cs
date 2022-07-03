using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ApiGateways.Domain.Command.ImageParser;

namespace ApiGateways.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ImageParserController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> ParseImage([FromBody] ParseImageCommand command) 
	        => await CreateResponse(async() => await Mediator.Send(command));
    }
}
