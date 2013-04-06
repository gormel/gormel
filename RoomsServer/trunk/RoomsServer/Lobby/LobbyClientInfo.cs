using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsServer
{
	public class LobbyClientInfo : ClientInfo
	{
		public string Name { get; private set; }
		public int Elo { get; set; }
		public bool InRoom { get; set; }
		public LobbyClientInfo(Client c, string name)
			: base(c)
		{
			InRoom = false;
			Name = name;
		}
	}
}
