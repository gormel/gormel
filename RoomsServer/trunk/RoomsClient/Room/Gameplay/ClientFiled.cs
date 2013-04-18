using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Gameplay;

namespace RoomsClient
{
	public class ClientFiled : Filed<ClientPlayer>
	{
		public FiledCursor Cursor { get; private set; }
		public bool Locked { get; private set; }

		public ClientFiled(int width, int height)
			: base(width, height)
		{
			Cursor = new FiledCursor();

			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					this[i, j] = new ClientCell();
				}
			}
		}

		public bool Test(Point a, Point b, Point c, Point pos)
		{
			Point s1 = b.Subtract(a);
			Point s2 = c.Subtract(a);
			Point s3 = c.Subtract(b);
			Point p1 = pos.Subtract(a);
			Point p2 = pos.Subtract(b);

			return s2.CrossZValue(p1) * s2.CrossZValue(s1) > 0 &&
					s1.CrossZValue(p1) * s1.CrossZValue(s2) > 0 &&
					s3.CrossZValue(p2) * s3.CrossZValue(s1.Negate()) > 0;
		}

		public void Update(FiledData<ClientPlayer> data)
		{
			foreach (var p in data.Points)
			{
				this[p] = data[p];
			}
		}

		public void Lock()
		{
			Locked = true;
		}

		public void Unlock()
		{
			Locked = false;
		}
	}
}
