using System;

namespace Contracts.ImageParserContract.ImageParserResponse
{
    public class SetImageInGroupResponseDto
	{
		public string ImageBaseString { get; set; }
		public Guid GroupId { get; set; }
	}
}
