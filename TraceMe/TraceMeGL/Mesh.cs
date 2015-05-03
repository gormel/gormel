using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraceMeGL
{
	public class Mesh
	{
		private Vector3[] vertices;
		private Vector4[] colors;
		private int[] indices;
		public Mesh(Vector3[] vertices, int[] indices, Vector4[] colors)
		{

			this.vertices = new Vector3[vertices.Length];
			this.indices = new int[indices.Length];
			this.colors = new Vector4[colors.Length];
			//this.reflections = new double[reflections.Length];

			//Array.Copy(reflections, this.reflections, reflections.Length);
			Array.Copy(vertices, this.vertices, vertices.Length);
			Array.Copy(indices, this.indices, indices.Length);
			Array.Copy(colors, colors, colors.Length);
		}
	}
}
