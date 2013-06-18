using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using UILib.Base;

namespace UILib.Controls
{
	public class TableItem : Table
	{
		public int Index { get; set; }
		protected Table Container { get; private set; }

		public TableItem(Table baseControl, GraphicsDevice device)
			: base(baseControl, device)
		{
			Container = baseControl;
			Index = -1;
		}
	}
}
