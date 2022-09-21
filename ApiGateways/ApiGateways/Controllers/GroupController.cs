using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using ApiGateways.Domain.Command.PixelsAndGroup;
using ApiGateways.Domain.Models.PixelsAndGroup;
using ApiGateways.Domain.Models.PixelsAndGroup.Response;

namespace ApiGateways.Controllers
{
	[ApiController]
	[UserAuthorize]
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

		[HttpGet("{PixelId:guid}")]
		public async Task<IActionResult> GetGroupByPixelId([FromRoute] GetGroupByPixelIdCommand сommand) =>
			await CreateResponse(async () => await Mediator.Send(сommand));

		[HttpGet("{GroupId:guid}")]
		public async Task<IActionResult> GetGroupById([FromRoute] GetGroupByIdCommand сommand) =>
			await CreateResponse(async () => await Mediator.Send(сommand));

		[HttpGet]
		[Description("Возможно не нужен, или переименовать надо и другой функционал дать ")]
		public async Task<IActionResult> GetGroupByUser() =>
			await CreateResponse(async () => await Mediator.Send(new GetGroupByUserIdCommand()));
		

		[HttpDelete]
		public async Task<IActionResult> RemovePixelGroup([FromBody] RemovePixelGroupCommand command) =>
			await CreateResponse(async () => await Mediator.Send(command));
    }
}
