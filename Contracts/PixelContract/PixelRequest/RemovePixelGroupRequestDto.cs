using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.PixelContract.PixelRequest
{
	public class RemovePixelGroupRequestDto
	{
		public Guid UserId { get; set; }
		public Guid GroupId { get; set; }
	}
}
