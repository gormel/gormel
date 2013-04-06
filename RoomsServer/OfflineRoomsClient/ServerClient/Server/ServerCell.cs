using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineRoomClient
{
	public class ServerCell : ICell<ServerPlayer>
	{
		public bool Right {	get; set; }
		public bool Down { get;	set; }
		public bool IsWall { get; set; }
		public ServerPlayer Symbol { get; set; }

		public ServerCell()
		{
			Symbol = null;
		}
	}
}
