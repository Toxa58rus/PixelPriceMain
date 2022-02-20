using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiGateways.Domain.Models.User.Response
{
	public class SignUpDataResponse
	{
		public string Token { get; set; }
		public Guid UserId { get; set; }
	}
}
