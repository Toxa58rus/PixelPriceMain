using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.PixelContract
{
	public class PixelDto
	{
		public Guid Id { get; set; }
		public int Color { get; set; }
		public int X { get; set; }
		public int Y { get; set; }
		public Guid GroupId { get; set; }
		public Guid UserId { get; set; }
	}
}
