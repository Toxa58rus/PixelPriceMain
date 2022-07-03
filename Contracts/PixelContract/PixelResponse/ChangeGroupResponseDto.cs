using System;

namespace Contracts.PixelContract.PixelResponse
{
    public class ChangeGroupResponseDto
	{
	    public Guid GroupId { get; set; }
	    public string Name { get; set; }
	    public Guid UserId { get; set; }
	    public string Massage { get; set; }
    }
}
