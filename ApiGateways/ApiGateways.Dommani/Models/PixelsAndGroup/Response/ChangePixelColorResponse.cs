using System;
using System.Collections.Generic;
using Contracts.PixelContract.PixelResponse;

namespace ApiGateways.Domain.Models.PixelsAndGroup.Response
{
	public class ChangePixelColorResponse
	{
		public List<Guid> PixelId { get; set; }
		public int Color { get; set; }

	}


}
