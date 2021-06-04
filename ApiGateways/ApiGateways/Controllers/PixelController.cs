using ApiGateways.Dommain.Command.Pixels;
using Common.Models.Pixels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiGateways.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("[controller]/[action]")]
    public class PixelController : BaseController
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<List<Pixels>> GetAllPixels() => 
            await Mediator.Send(new GetAllPixelsCommand());

        [HttpPost]
        public async Task<PixelGroup> CreatePixelGroup([FromBody]CreatePixelGroupCommand command) => 
            await Mediator.Send(command);

        [HttpPost]
        public async Task<List<Pixels>> ChangerPixelGroup([FromBody] ChangerPixelGroupCommand command) =>
            await Mediator.Send(command);

        [HttpDelete]
        public async Task<bool> RemovePixeGrpup([FromBody] RemovePixeGrpupCommand command) =>
            await Mediator.Send(command);

        [HttpPost]
        public async Task<List<Pixels>> ChangerPixelsOwner([FromBody] ChangerPixelsOwnerCommand command) =>
            await Mediator.Send(command);

        [HttpPost]
        public async Task<List<PixelGroup>> ChangerPixelGroupOwner([FromBody] ChangerPixelGroupOwnerCommand command) =>
            await Mediator.Send(command);

        [HttpPost]
        public async Task<List<PixelColorReslutModel>> ChangerPixelColor([FromBody] ChangerPixelColorCommand command) =>
            await Mediator.Send(command);
    }
}
