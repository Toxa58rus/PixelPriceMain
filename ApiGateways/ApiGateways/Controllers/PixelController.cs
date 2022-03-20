using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ApiGateways.Domain.Command.PixelsAndGroup;

namespace ApiGateways.Controllers
{
    // TODO: Вынести работу с группами в другой контроллер 
    // Добавить погимнацию получения пикселей 
    [ApiController]
    [Route("[controller]/[action]")]
    public class PixelController : BaseController
    {

        [UserAuthorize]
        [HttpPost]
        public async Task<IActionResult> ChangerPixelColor([FromBody] ChangerPixelColorCommand command) =>
	        await CreateResponse(async () => await Mediator.Send(command));

    }
}
