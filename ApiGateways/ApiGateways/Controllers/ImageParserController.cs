using ApiGateways.Dommain.Command.ImageParser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Common.Models.ImageParser;

namespace ApiGateways.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]/[action]")]
    public class ImageParserController : BaseController
    {
        [HttpPost]
        [AllowAnonymous]
        public async Task<ImageData> ParceImagetoBitmap([FromBody] ParceImagetoBitmapCommand command) => await Mediator.Send(command);
    }
}
