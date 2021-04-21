using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Models.Pixels;
using Microsoft.AspNetCore.Mvc;
using Pixel.Dommain.Command;

namespace Pixel.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PixelController : BaseController
    {
        [HttpGet]
        public async Task<List<Pixels>> GetAllPixels() => await Mediator.Send(new GetAllPixelsCommand());
    }
}
