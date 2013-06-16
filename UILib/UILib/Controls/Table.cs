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
			return GetSize(row, r => r.Height, r => r.ValueHeight, r => r.ProcentHeight, Height, Y, Rows);
		}

		public float GetY(RowItem row)
		{
			return GetOffset(row, r => r.Height, Y, Rows);
		}

		public float GetWidth(ColItem col)
		{
			return GetSize(col, c => c.Width, c => c.ValueWidth, c => c.ProcentWidth, Width, X, Cols);
		}

		public float GetX(ColItem col)
		{
			return GetOffset(col, c => c.Width, X, Cols);
		}

		private float GetSize<T>(T item, Func<T, float> itemSizeSelector, Func<T, float> itemValueSizeSelector
			, Func<T, float> itemProcentSizeSelector, float size, float offset, IEnumerable<T> items) where T : TableItem
		{
			float exceptedSize = -1;
			if (itemProcentSizeSelector(item) != -1)
				exceptedSize = size * itemProcentSizeSelector(item);
			else if (itemValueSizeSelector(item) != -1)
				exceptedSize = itemValueSizeSelector(item);
			else
			{
				var setted = from r in items where itemProcentSizeSelector(r) != -1 || itemValueSizeSelector(r) != -1 select r;
				exceptedSize = (size - setted.Sum(r => itemSizeSelector(r))) / (items.Count() - setted.Count());
				return Math.Max(0, exceptedSize);
			}
			return Math.Max(0, Math.Min(exceptedSize, Y + Height - GetOffset(item, itemSizeSelector, offset, items)));
		}

		private float GetOffset<T>(T item, Func<T, float> itemSizeSelector, float offset, IEnumerable<T> items) where T : TableItem
		{
			var before = from c in items
						 where c.Index < item.Index
						 select c;
			return offset + before.Sum(c => itemSizeSelector(c));
		}
	}
}
