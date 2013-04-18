using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace OfflineRoomClient
{
	public class FiledData<TSymbol>
	{
		public IEnumerable<Point> Points
		{
			get { return data.Keys; }
		}
		private Dictionary<Point, Cell<TSymbol>> data = new Dictionary<Point, Cell<TSymbol>>();

		public void Add(Point p, Cell<TSymbol> cell)
		{
			if (!data.ContainsKey(p))
				data.Add(p, cell);
		}

		public Cell<TSymbol> Get(Point p)
		{
			return data[p];
		}

		public Cell<TSymbol> this[Point key]
		{
			get
			{
				return data[key];
			}
		}
	}
}
