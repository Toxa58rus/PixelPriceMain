using System;

namespace ApiGateways.Domain.Models.PixelsAndGroup.Response
{
    public class GetGroupResponse
	{
	    public Guid GroupId { get; set; }
	    public Guid UserId { get; set; }
		public string Name { get; set; }
	    public string Message { get; set; }
	}
}
