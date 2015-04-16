
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TraceMe
{
    public struct Vector3
    {
        public static Vector3 Zero { get { return new Vector3(0); } }
        public static Vector3 Forward { get { return new Vector3(0, 0, 1); } }
        public static Vector3 Up { get { return new Vector3(0, 1, 0); } }
        public static Vector3 Right { get { return new Vector3(1, 0, 0); } }

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public double LenghtSq()
        {
            return X * X + Y * Y + Z * Z;
        }

        public double Lenght()
        {
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        public static Vector3 operator *(double a, Vector3 b)
        {
            return new Vector3(a * b.X, a * b.Y, a * b.Z);
        }

        public static Vector3 operator *(Vector3 a, double b)
        {
            return new Vector3(b * a.X, b * a.Y, b * a.Z);
        }

        public static Vector3 operator /(Vector3 a, double b)
        {
            return new Vector3(a.X / b, a.Y / b, a.Z / b);
        }

        //public Vector3 Normalize()
        //{
        //    return this / Lenght();
        //}

        public void Normalize()
        {
            var l = Lenght();
            X /= l;
            Y /= l;
            Z /= l;
        }

        public Vector3 CrossProduct(Vector3 v)
        {
            return new Vector3(Y * v.Z - Z * v.Y,
                               Z * v.X - X * v.Z,
                               X * v.Y - Y * v.X);
        }

        public static void Cross(ref Vector3 v1, ref Vector3 v2, out Vector3 r)
        {
            r = new Vector3(v1.Y * v2.Z - v1.Z * v2.Y,
                            v1.Z * v2.X - v1.X * v2.Z,
                            v1.X * v2.Y - v1.Y * v2.X);
        }

        public double Dot(Vector3 v)
        {
            return X * v.X + Y * v.Y + Z * v.Z;
        }
    }
}
