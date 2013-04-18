using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace OfflineRoomClient
{
	public class Filed<TSymbol> : IFiled
	{
		public int Width { get; set; }
		public int Height { get; set; }
		public bool Complete
		{
			get
			{
				foreach (var cell in from c in data where !c.IsWall select c)
				{
					if (EqualityComparer<TSymbol>.Default.Equals(cell.Symbol, default(TSymbol)))
						return false;
				}
				return true;
			}
		}

		protected Cell<TSymbol>[] data;
		public Filed(int width, int height)
		{
			Width = width;
			Height = height;
			data = new Cell<TSymbol>[width * height];
		}

		public Cell<TSymbol> this[int x, int y]
		{
			set { data[x + y * Width] = value; }
			get { return data[x + y * Width]; }
		}

		public Cell<TSymbol> this[Point p]
		{
			set { this[p.X, p.Y] = value; }
			get { return this[p.X, p.Y]; }
		}


		public int Score(TSymbol symbol)
		{
			return data.Count(c => !c.IsWall && EqualityComparer<TSymbol>.Default.Equals(c.Symbol, symbol));
		}


		ICell IFiled.this[int x, int y]
		{
			get
			{
				return this[x, y];
			}
		}

		ICell IFiled.this[Point p]
		{
			get
			{
				return this[p];
			}
		}
	}
}
