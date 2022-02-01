using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiGateways.Context.Models
{
	public class Logs
	{
		public string Application { get; set; }
		public string Level { get; set; }
		public string Message { get; set; }
		public string Logger { get; set; }
		public string Callsite { get; set; }
		public string Exception { get; set; }
		public string Logged { get; set; }
	}
}
