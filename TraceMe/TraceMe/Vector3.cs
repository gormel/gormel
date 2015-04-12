
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraceMe
{
    public struct Vector3
    {
        public static Vector3 Zero { get { return new Vector3(0); } }
        public static Vector3 Forward { get { return new Vector3(0, 0, 1); } }

        public double X;
        public double Y;
        public double Z;

        public Vector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3(float xyz)
            : this(xyz, xyz, xyz)
        {
        }

        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Vector3 operator -(Vector3 v) 
        {
            return new Vector3(-v.X, -v.Y, -v.Z);
        }

        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return a + (-b);
        }

        public double LenghtSq()
        {
            return X * X + Y * Y + Z * Z;
        }

        public double Lenght()
        {
            return Math.Sqrt(LenghtSq());
        }

        public static Vector3 operator *(double a, Vector3 b)
        {
            return new Vector3(a * b.X, a * b.Y, a * b.Z);
        }

        public static Vector3 operator *(Vector3 a, double b)
        {
            return b * a;
        }

        public static Vector3 operator /(Vector3 a, double b)
        {
            return 1 / b * a;
        }

        public Vector3 Normalize()
        {
            return this / Lenght();
        }

        public Vector3 CrossProduct(Vector3 v)
        {
            return new Vector3(Y * v.Z - Z * v.Y,
                               Z * v.X - X * v.Z,
                               X * v.Y - Y * v.X);
        }

        public double DotProduct(Vector3 v)
        {
            return X * v.X + Y * v.Y + Z * v.Z;
        }
    }
}
