using ApiGateways.Dommain.Command.ImageParser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiGateways.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]/[action]")]
    public class ImageParserController : BaseController
    {
        [HttpPost]
        [AllowAnonymous]
        public async Task<string> ParceImagetoBitmap([FromBody] ParceImagetoBitmapCommand command) => await Mediator.Send(command);
    }
}
