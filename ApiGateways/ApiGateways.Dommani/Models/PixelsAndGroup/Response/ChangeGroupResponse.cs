using System;

namespace ApiGateways.Domain.Models.PixelsAndGroup.Response
{
    public class ChangeGroupResponse
	{
	    public Guid GroupId { get; set; }
	    public string Name { get; set; }
	    public Guid UserId { get; set; }
	    public string Massage { get; set; }
	}
}
