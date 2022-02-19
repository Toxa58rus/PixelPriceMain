using System;
using System.Collections.Generic;
using ApiGateways.Domain.Models.PixelsAndGroup.Response;
using Contracts.PixelContract.PixelResponse;

namespace ApiGateways.Domain.Models.PixelsAndGroup.Mapping;

public static class MappingChangePixelColorResponse
{
	public static  ChangePixelColorResponse ToModel(this ChangePixelColorResponseDto dto)
	{
		return new ChangePixelColorResponse()
		{
			Color = dto.Color,
			PixelId = new List<Guid> { Guid.Empty }
		};
	}
}