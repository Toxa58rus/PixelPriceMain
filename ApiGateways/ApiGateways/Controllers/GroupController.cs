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
	[ApiController]
	[Route("[controller]/[action]")]
	public class GroupController : BaseController
    {
	    [HttpPost]
		public async Task<IActionResult> CreatePixelGroup([FromBody] CreatePixelGroupCommand command) =>
			await CreateResponse(async () => await Mediator.Send(command));

		[HttpPut]
		public async Task<IActionResult> ChangePixelGroup([FromBody] ChangerPixelGroupCommand command) =>
			await CreateResponse(async () => await Mediator.Send(command));

		[HttpPut]
		public async Task<IActionResult> ChangeGroup([FromBody] ChangerGroupCommand command) =>
			await CreateResponse(async () => await Mediator.Send(command));

		[HttpGet("{GroupId:guid}")]
		public async Task<IActionResult> GetGroupById([FromRoute] GetGroupByIdCommand сommand) =>
			await CreateResponse(async () => await Mediator.Send(сommand));

		[HttpGet("{UserId:guid}")]
		public async Task<IActionResult> GetGroupByUserId([FromRoute] GetGroupByUserIdCommand command) =>
			await CreateResponse(async () => await Mediator.Send(command));


		[HttpDelete]
		public async Task<IActionResult> RemovePixelGroup([FromBody] RemovePixelGroupCommand command) =>
			await CreateResponse(async () => await Mediator.Send(command));
    }
}
