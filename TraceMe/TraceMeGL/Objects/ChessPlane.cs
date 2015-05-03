using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TraceMeGL.Objects
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	[Intersection(FileName = "Objects/chessplane.txt")]
	public class ChessPlane
	{
		public float reflection;
		public float d;
		public float sqWidth;
		public float sqHeight;
		public Vector4 color1;
		public Vector4 color2;
	}
}
