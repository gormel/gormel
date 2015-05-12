using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraceMeGL
{
	public class Mesh
	{
		private List<Triangle> triangles = new List<Triangle>();

		public Matrix4 Transformations { get; set; }

		public Mesh(Vector3[] vertices, int[] indices, Vector4[] colors, float[] reflections)
		{
			Transformations = Matrix4.Identity;

			for (int i = 0; i < indices.Length; i += 3)
			{
				Vector3 a = vertices[indices[i] + 0];
				Vector3 b = vertices[indices[i] + 1];
				Vector3 c = vertices[indices[i] + 2];

				Vector4 ca = colors[indices[i] + 0];
				Vector4 cb = colors[indices[i] + 1];
				Vector4 cc = colors[indices[i] + 2];

				float ra = reflections[indices[i] + 0];
				float rb = reflections[indices[i] + 1];
				float rc = reflections[indices[i] + 2];

				Triangle t = ObjectManager.Instance.Value.CreateObject<Triangle>();
				t.a = a;
				t.b = b;
				t.c = c;

				t.ca = ca;
				t.cb = cb;
				t.cc = cc;

				t.ra = ra;
				t.rb = rb;
				t.rc = rc;

				triangles.Add(t);
			}
		}

		public void FlushTransforms()
		{
			foreach (var t in triangles)
			{
				t.a = Vector3.Transform(t.a, Transformations);
				t.b = Vector3.Transform(t.b, Transformations);
				t.c = Vector3.Transform(t.c, Transformations);
			}
		}

		public static Mesh LoadFromObj(Stream fileStream)
		{
			using (var reader = new StreamReader(fileStream))
			{
				while (!reader.EndOfStream)
				{
					string s = reader.ReadLine();
					if (s.StartsWith("#"))
						continue;
					if (s.StartsWith("v "))
					{
						//vertex
						var splitted = s.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

						var v1 = Convert.ToDouble(splitted[1]);
						var v2 = Convert.ToDouble(splitted[2]);
						var v3 = Convert.ToDouble(splitted[2]);

						Vector3 vertex = new Vector3((float)v1, (float)v2, (float)v3);
					}

					if (s.StartsWith("vt "))
					{
						//tex coord
					}

					if (s.StartsWith("f "))
					{
						//index
					}
				}
			}
			return null;
		}
	}
}
