using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsServer
{
	public class LobbyClientInfo : ClientInfo
	{
		public bool InRoom { get; set; }
		public LobbyClientInfo(Client c, string name)
			: base(name, c)
		{
			InRoom = false;
		}
	}
}
