using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using UILib.Base;

namespace UILib.Controls
{
	public class Table : UIControl
	{
		public List<RowItem> Rows { get; private set; }
		public List<ColItem> Cols { get; private set; }
		public Table(UIControl baseControl, GraphicsDevice device)
			: base(baseControl, device)
		{
			Rows = new List<RowItem>();
			Cols = new List<ColItem>();
		}
	}
}
