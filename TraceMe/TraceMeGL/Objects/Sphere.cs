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
	[Intersection(FileName = "Objects/sphere.txt")]
	public struct Sphere
	{
		public Vector3 position;
		public Vector4 color;
		public float radius;
		public float reflection;
	}
}
