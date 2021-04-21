using ApiGateways.Dommain.Command.Pixels;
using Common.Models.Pixels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiGateways.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]/[action]")]
    public class PixelController : BaseController
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<List<Pixels>> GetAllPixels() => await Mediator.Send(new GetAllPixelsCommand());
    }
}
