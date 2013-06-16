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

		public float GetHeight(RowItem row)
		{
			float exceptedHeight = -1;
			if (row.ProcentHeight != -1)
				exceptedHeight = Height * row.ProcentHeight;
			else if (row.ValueHeight != -1)
				exceptedHeight = row.ValueHeight;
			else
			{
				var setted = from r in Rows where r.ProcentHeight != -1 || r.ValueHeight != -1 select r;
				exceptedHeight = (Height - setted.Sum(r => r.Height)) / (Rows.Count - setted.Count());
			}
			return Math.Max(0, Math.Min(exceptedHeight, Y + Height - row.Y));
		}

		public float GetY(RowItem row)
		{
			var before = from c in Rows
						 where c.Index < row.Index
						 select c;
			return Y + before.Sum(c => c.Height);
		}
	}
}
