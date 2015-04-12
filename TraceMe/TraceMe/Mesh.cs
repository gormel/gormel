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
        public Matrix4 Transformation { get; set; }
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
        private bool SameSide(Vector3 p1, Vector3 p2, Vector3 a, Vector3 b)
        {
            var cp1 = (b - a).CrossProduct(p1 - a);
            var cp2 = (b - a).CrossProduct(p2 - a);
            return cp1.DotProduct(cp2) >= 0;
        }

        public override Hit Intersections(Lay lay)
        {
            Hit result = null;
            double mint = double.PositiveInfinity;
            for (int v = 0; v < indices.Length / 3; v += 3)
            {
                Vector3 a = Transformation.Transform(vertices[v + 0]);
                Vector3 b = Transformation.Transform(vertices[v + 1]);
                Vector3 c = Transformation.Transform(vertices[v + 2]);

                Vector3 n = (b - a).CrossProduct(c - a).Normalize();
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

                    if (!(SameSide(i, a, b, c) && SameSide(i, b, a, c) && SameSide(i, c, a, b)))
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
