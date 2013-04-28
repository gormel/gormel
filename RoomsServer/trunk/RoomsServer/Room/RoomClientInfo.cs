using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsServer
{
	public class RoomClientInfo : ClientInfo
	{
		public Team Team { get; private set; }
		public ServerPlayer Player { get; set; }
		public RoomClientInfo(Client c, string name, Team team)
			: base(name, c)
		{
			Team = team;
		}
	}
}
