using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineRoomClient
{
	public enum Direction
	{
		Up = 0,
		Left = 1,
		Down = 2,
		Right = 3,
	}
	public class Cursor : ICloneable
	{
		public int X { get; set; }
		public int Y { get; set; }
		public Direction Direction { get; set; }
		public Point Position
		{
			get { return new Point(X, Y); }
		}


		public object Clone()
		{
			var rv = new Cursor();
			rv.X = X;
			rv.Y = Y;
			rv.Direction = Direction;

			return rv;
		}
	}
}
