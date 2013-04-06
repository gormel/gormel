using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsServer
{
	public class LoginClientInfo : ClientInfo
	{
		public LoginClientInfo(Client c)
			: base(c)
		{
		}
	}
}
