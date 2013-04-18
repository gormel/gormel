using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OfflineRoomClient
{
	public class Cell<TSymbol> : ICell
	{
		public TSymbol Symbol { get; set; }
		public bool Right { get; set; }
		public bool Down { get; set; }
		public bool IsWall { get; set; }
	}
}
