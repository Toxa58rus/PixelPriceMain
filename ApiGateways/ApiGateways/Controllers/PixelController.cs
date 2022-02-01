using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateways.Domain.Command.PixelsAndGroup;
using ApiGateways.Domain.Models.PixelsAndGroup;
using ApiGateways.Domain.Models.PixelsAndGroup.Response;

namespace ApiGateways.Controllers
{
    // TODO: Вынести работу с группами в другой контроллер 
			// Добавить погимнацию получения пикселей 
    [ApiController]
    //[Authorize]
    [Route("[controller]/[action]")]
    [AllowAnonymous]
    public class PixelController : BaseController
    {
      //  [HttpGet]
      //  public async Task<List<Pixel>> GetById(Guid Id) => 
      //      await Mediator.Send();

       // [HttpPost]
       // public async Task<PixelGroup> CreatePixelGroup([FromBody]CreatePixelGroupCommand command) => 
       //     await Mediator.Send(command);

       // [HttpPost]
       // public async Task<List<Pixel>> ChangerPixelGroup([FromBody] ChangerPixelGroupCommand command) =>
       //     await Mediator.Send(command);

        [HttpPost]
        public async Task<List<Pixel>> ChangerPixelsOwner([FromBody] ChangerPixelsOwnerCommand command) =>
            await Mediator.Send(command);

        //[HttpPost]
        //public async Task<List<PixelGroup>> ChangerPixelGroupOwner([FromBody] ChangerPixelGroupOwnerCommand command) =>
        //    await Mediator.Send(command);

        [HttpPost]
        public async Task<List<ChangePixelColorResponseModel>> ChangerPixelColor([FromBody] ChangerPixelColorCommand command) =>
            await Mediator.Send(command);

        //[HttpDelete]
        //public async Task<bool> RemovePixelGroup([FromBody] RemovePixelGroupCommand command) =>
        //    await Mediator.Send(command);
    }
}
