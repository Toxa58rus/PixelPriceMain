using System;

namespace Contracts.ImageParserContract.ImageParserRequest
{
	public class SetImageInGroupRequestDto
    {
	    public string ImageBaseString { get; set; }
	    public Guid GroupId { get; set; }
    }
}
