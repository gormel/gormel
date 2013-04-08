using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineRoomClient
{
	public class Filed<TCell, TSymbol> where TCell : ICell<TSymbol>
	{
		public int Width { get; private set; }
		public int Height { get; private set; }

		protected TCell[] data;
		public Filed(int width, int height)
		{
			Width = width;
			Height = height;
			data = new TCell[width * height];
		}

		public TCell this[int x, int y]
		{
			set { data[x + y * Width] = value; }
			get { return data[x + y * Width]; }
		}

		public TCell this[Point p]
		{
			set { this[p.X, p.Y] = value; }
			get { return this[p.X, p.Y]; }
		}

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

		public int Score(TSymbol symbol)
		{
			return data.Count(c => !c.IsWall && EqualityComparer<TSymbol>.Default.Equals(c.Symbol, symbol));
		}
	}
}
