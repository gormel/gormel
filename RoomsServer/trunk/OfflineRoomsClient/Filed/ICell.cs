using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OfflineRoomClient
{
	public interface ICell
	{
		bool Right { get; set; }
		bool Down { get; set; }
		bool IsWall { get; set; }
	}
}
