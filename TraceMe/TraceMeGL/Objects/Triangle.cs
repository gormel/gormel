using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TraceMeGL
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	[Intersection(FileName = "Objects/triangle.txt")]
	public class Triangle
	{
		public Vector3 a;
		public Vector3 b;
		public Vector3 c;

		public Vector4 ca;
		public Vector4 cb;
		public Vector4 cc;
	}
}
