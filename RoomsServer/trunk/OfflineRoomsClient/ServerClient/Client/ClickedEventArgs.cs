using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OfflineRoomClient
{
	public class ClickedEventArgs : EventArgs
	{
		public Cursor Cursor { get; private set; }
		public ClickedEventArgs(Cursor cursor)
		{
			Cursor = cursor;
		}
	}
}
