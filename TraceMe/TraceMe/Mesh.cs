using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TraceMe
{
    public class Mesh : BaseObject
    {
        public Matrix4 Transformation;
        private Vector3[] vertices;
        private int[] indices;
        private Color[] colors;
        private double[] reflections;

        public Mesh(Vector3[] vertices, int[] indices, Color[] vertexColors, double[] reflections)
        {
            if (indices.Length % 3 != 0)
                throw new ArgumentException("Indices do not describes triangles.");
            if (indices.Any(i => i >= vertices.Length))
                throw new ArgumentException("Indices do not describes triangles.");

            this.vertices = new Vector3[vertices.Length];
            this.indices = new int[indices.Length];
            this.colors = new Color[vertexColors.Length];
            this.reflections = new double[reflections.Length];

            Array.Copy(reflections, this.reflections, reflections.Length);
            Array.Copy(vertices, this.vertices, vertices.Length);
            Array.Copy(indices, this.indices, indices.Length);
            Array.Copy(vertexColors, colors, vertexColors.Length);

            Transformation = Matrix4.Identity();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool SameSide(ref Vector3 p1, ref Vector3 p2, ref Vector3 a, ref Vector3 b)
        {
            Vector3 cp1 = new Vector3();
            Vector3 cp2 = new Vector3();
            Vector3 ba = b - a;
            Vector3 p1a = p1 - a;
            Vector3 p2a = p2 - a;
            Vector3.CrossProduct(ref ba, ref p1a, out cp1);
            Vector3.CrossProduct(ref ba, ref p2a, out cp2);
            return cp1.DotProduct(cp2) >= 0;
        }

        public override Hit Intersections(Lay lay)
        {
            Hit result = null;
            double mint = double.PositiveInfinity;
            for (int v = 0; v < indices.Length / 3; v += 3)
            {
                Vector3 a = new Vector3();
                Vector3 b = new Vector3();
                Vector3 c = new Vector3();
                Matrix4.Transform(ref Transformation, ref vertices[v + 0], out a);
                Matrix4.Transform(ref Transformation, ref vertices[v + 1], out b);
                Matrix4.Transform(ref Transformation, ref vertices[v + 2], out c);

                Vector3 n = (b - a).CrossProduct(c - a);
                n.Normalize();
                double d = -n.DotProduct(a);

                double t = -(d + lay.Point.DotProduct(n)) / lay.Direction.DotProduct(n);

                if (t < mint)
                {
                    mint = t;
                    Vector3 i = lay.Point + lay.Direction * t;
                    Vector3 ai = a - i;
                    Vector3 bi = b - i;
                    Vector3 ci = c - i;
                    Vector3 ac = a - c;
                    Vector3 bc = b - c;

                    //if ((ai.CrossProduct(bi) + bi.CrossProduct(ci) + ci.CrossProduct(ai)).LenghtSq() > ac.CrossProduct(bc).LenghtSq())
                    //    continue;

                    if (!(SameSide(ref i, ref a, ref b, ref c) && SameSide(ref i,ref b, ref a, ref c) && SameSide(ref i, ref c, ref a, ref b)))
                        continue;

                    Color ca = colors[v + 0];
                    Color cb = colors[v + 1];
                    Color cc = colors[v + 2];

                    double da = ai.LenghtSq();
                    double db = bi.LenghtSq();
                    double dc = ci.LenghtSq();

                    double max = Math.Max(da, Math.Max(db, dc));

                    da /= max;
                    db /= max;
                    dc /= max;

                    Color color = Color.FromArgb((byte)(ca.R * da + cb.R * db + cc.R * dc),
                                                  (byte)(ca.G * da + cb.G * db + cc.G * dc),
                                                  (byte)(ca.B * da + cb.B * db + cc.B * dc));

                    result = new Hit();
                    result.Color = color;
                    result.Distance = t;
                    result.Normal = n;
                    result.Reflection = reflections[v] * da + reflections[v + 1] * db + reflections[v + 2] * dc;
                }

                
            }

            return result;
        }
    }
}
