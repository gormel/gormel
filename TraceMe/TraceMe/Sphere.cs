using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraceMe
{
    public class Sphere : BaseObject
    {
        public Vector3 Center { get; set; }
        public double Radius { get; set; }
        public Color Color { get; set; }
        public double Reflection { get; set; }
        public double Refraction { get; set; }
        public double LightDensity { get; set; }

        private Random rnd = new Random();

        public Sphere(Vector3 c, double r)
        {
            Center = c;
            Radius = r;
            Color = Color.Black;
        }

        public override Hit Intersections(Lay lay)
        {
            double a = lay.Direction.LenghtSq();
            double b = 2 * (lay.Point - Center).DotProduct(lay.Direction);
            double c = (lay.Point - Center).LenghtSq() - Radius * Radius;
            double d = b * b - 4 * a * c;
            if (d < 0)
                return null;

            double t1 = (-b + Math.Sqrt(d)) / 2 / a;
            double t2 = (-b - Math.Sqrt(d)) / 2 / a;
            
            Hit hit = new Hit();
            hit.Color = Color;
            hit.Distance = Math.Min(t1, t2);
            Vector3 hitPoint = lay.Point + lay.Direction * hit.Distance;

            double dx = (rnd.NextDouble() - 0.5) / 10;
            double dz = (rnd.NextDouble() - 0.5) / 10;
            double dy = (rnd.NextDouble() - 0.5) / 10;

            hit.Normal = hitPoint - Center + new Vector3(dx, dy, dz);
            hit.Normal.Normalize();
            hit.Reflection = Reflection;

            return hit;
        }
    }
}
