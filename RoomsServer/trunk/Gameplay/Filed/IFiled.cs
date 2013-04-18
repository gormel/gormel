using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Gameplay
{
	public interface IFiled
	{
		int Width { get; set; }
		int Height { get; set; }
		bool Complete { get; }
		ICell this[int x, int y] { get; }
		ICell this[Point p] { get; }
	}
}
