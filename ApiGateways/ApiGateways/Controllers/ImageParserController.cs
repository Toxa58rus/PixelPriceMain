using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ApiGateways.Domain.Command.ImageParser;
using ApiGateways.Domain.Models.ImageParser;

namespace ApiGateways.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ImageParserController : BaseController
    {
        [HttpPost]
        public async Task<ImageData> ParseImage([FromBody] ParseImageCommand command) => await Mediator.Send(command);
    }
}
