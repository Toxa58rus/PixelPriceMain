using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.PixelContract.PixelRequest
{
	public class GetPixelPartRequestDto
	{
		public int StartPositionX { get; set; }
		public int StartPositionY { get; set; }
		public int EndPositionX { get; set; }
		public int EndPositionY { get; set; }
	}
}
