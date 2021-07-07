﻿using ApiGateways.Dommain.Command.ImageParser;
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
        public async Task<ImageData> ParseImage([FromBody] ParseImageCommand command) => await Mediator.Send(command);
    }
}
