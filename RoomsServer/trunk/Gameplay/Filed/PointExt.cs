using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
	public static class PointExt
	{
		public static float Dot(this Point a, Point b)
		{
			return (float)a.X * b.X + (float)a.Y * b.Y;
		}

		public static float CrossZValue(this Point a, Point b)
		{
			return (float)a.X * b.Y - (float)b.X * a.Y;
		}

		public static Point Add(this Point a, Point b)
		{
			return new Point(a.X + b.X, a.Y + b.Y);
		}

		public static Point Negate(this Point p)
		{
			return new Point(-p.X, -p.Y);
		}

		public static Point Subtract(this Point a, Point b)
		{
			return a.Add(b.Negate());
		}

		public static double Distance(this Point a, Point b)
		{
			Point sub = b.Subtract(a);
			return Math.Sqrt(sub.X * sub.X + sub.Y * sub.Y);
		}
	}
}
