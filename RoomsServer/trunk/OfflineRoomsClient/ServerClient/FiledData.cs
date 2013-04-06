using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace OfflineRoomClient
{
	public class FiledData<TCell, TSymbol> where TCell : ICell<TSymbol>
	{
		public IEnumerable<Point> Points
		{
			get { return data.Keys; }
		}
		private Dictionary<Point, TCell> data = new Dictionary<Point, TCell>();

		public void Add(Point p, TCell cell)
		{
			if (!data.ContainsKey(p))
				data.Add(p, cell);
		}

		public TCell Get(Point p)
		{
			return data[p];
		}

		public TCell this[Point key]
		{
			get
			{
				return data[key];
			}
		}
	}
}
