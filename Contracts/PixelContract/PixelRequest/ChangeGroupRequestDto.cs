using System;
using System.Collections.Generic;

namespace Contracts.PixelContract.PixelRequest
{
    public class ChangeGroupRequestDto
    {
	    public Guid GroupId { get; set; }
		public string Name { get; set; }
	    public Guid UserId { get; set; }
	    public string Massage { get; set; }
    }
}
