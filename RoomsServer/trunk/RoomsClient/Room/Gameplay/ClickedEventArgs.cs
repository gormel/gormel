using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gameplay;

namespace RoomsClient
{
	public class ClickedEventArgs : EventArgs
	{
		public FiledCursor Cursor { get; private set; }
		public ClickedEventArgs(FiledCursor cursor)
		{
			Cursor = cursor;
		}
	}
}
