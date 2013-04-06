using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineRoomClient
{
	public interface ICell<TSymbol>
	{
		bool Right { get; set; }
		bool Down { get; set; }
		bool IsWall { get; set; }
		TSymbol Symbol { get; set; }
	}
}
