using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraceMe
{
    public struct Matrix4
    {
        //private double[,] values;

        private double m00;
        private double m01;
        private double m02;
        private double m03;
        private double m10;
        private double m11;
        private double m12;
        private double m13;
        private double m20;
        private double m21;
        private double m22;
        private double m23;
        private double m30;
        private double m31;
        private double m32;
        private double m33;


        private const int rows = 4;
        private const int cols = 4;

        //public Matrix4()
        //{
            //values = new double[rows, cols];
            //for (int i = 0; i < rows; i++)
            //{
            //    for (int j = 0; j < cols; j++)
            //    {
            //        values[i, j] = 0;
            //    }
            //}
        //}

        public Matrix4(double m00, double m01, double m02, double m03, 
                       double m10, double m11, double m12, double m13, 
                       double m20, double m21, double m22, double m23, 
                       double m30, double m31, double m32, double m33)
        {
            this.m00 = m00;
            this.m01 = m01;
            this.m02 = m02;
            this.m03 = m03;

            this.m10 = m10;
            this.m11 = m11;
            this.m12 = m12;
            this.m13 = m13;

            this.m20 = m20;
            this.m21 = m21;
            this.m22 = m22;
            this.m23 = m23;

            this.m30 = m30;
            this.m31 = m31;
            this.m32 = m32;
            this.m33 = m33;
        }

        public static Matrix4 operator *(Matrix4 a, Matrix4 b)
        {
            Matrix4 r = new Matrix4();
            //for (int i = 0; i < 4; i++)
            //{
            //    for (int j = 0; j < 4; j++)
            //    {
            //        for (int k = 0; k < 4; k++)
            //        {
            //            result.values[i, j] += a.values[i, k] * b.values[k, j];
            //        }
            //    }
            //}

            r.m00 = a.m00 * b.m00 + a.m01 * b.m10 + a.m02 * b.m20 + a.m03 * b.m30;
            r.m01 = a.m00 * b.m01 + a.m01 * b.m11 + a.m02 * b.m21 + a.m03 * b.m31;
            r.m02 = a.m00 * b.m02 + a.m01 * b.m12 + a.m02 * b.m22 + a.m03 * b.m32;
            r.m03 = a.m00 * b.m03 + a.m01 * b.m13 + a.m02 * b.m23 + a.m03 * b.m33;

            r.m10 = a.m10 * b.m00 + a.m11 * b.m10 + a.m12 * b.m20 + a.m13 * b.m30;
            r.m11 = a.m10 * b.m01 + a.m11 * b.m11 + a.m12 * b.m21 + a.m13 * b.m31;
            r.m12 = a.m10 * b.m02 + a.m11 * b.m12 + a.m12 * b.m22 + a.m13 * b.m32;
            r.m13 = a.m10 * b.m03 + a.m11 * b.m13 + a.m12 * b.m23 + a.m13 * b.m33;

            r.m20 = a.m20 * b.m00 + a.m21 * b.m10 + a.m22 * b.m20 + a.m23 * b.m30;
            r.m21 = a.m20 * b.m01 + a.m21 * b.m11 + a.m22 * b.m21 + a.m23 * b.m31;
            r.m22 = a.m20 * b.m02 + a.m21 * b.m12 + a.m22 * b.m22 + a.m23 * b.m32;
            r.m23 = a.m20 * b.m03 + a.m21 * b.m13 + a.m22 * b.m23 + a.m23 * b.m33;

            r.m30 = a.m30 * b.m00 + a.m31 * b.m10 + a.m32 * b.m20 + a.m33 * b.m30;
            r.m31 = a.m30 * b.m01 + a.m31 * b.m11 + a.m32 * b.m21 + a.m33 * b.m31;
            r.m32 = a.m30 * b.m02 + a.m31 * b.m12 + a.m32 * b.m22 + a.m33 * b.m32;
            r.m33 = a.m30 * b.m03 + a.m31 * b.m13 + a.m32 * b.m23 + a.m33 * b.m33;
            return r;                                  
        }

        public static Matrix4 RotationX(double angle)
        {
            var cos = Math.Cos(angle);
            var sin = Math.Sin(angle);
            return new Matrix4(1,   0,    0, 0,
                               0, cos, -sin, 0,
                               0, sin,  cos, 0,
                               0,   0,    0, 1);
        }

        public static Matrix4 RotationY(double angle)
        {
            var cos = Math.Cos(angle);
            var sin = Math.Sin(angle);
            return new Matrix4(cos, 0, sin, 0,
                                  0, 1,   0, 0,
                               -sin, 0, cos, 0,
                                  0, 0,   0, 1);
        }

        public static Matrix4 RotationZ(double angle)
        {
            var cos = Math.Cos(angle);
            var sin = Math.Sin(angle);
            return new Matrix4( cos, sin, 0, 0,
                               -sin, cos, 0, 0,
                                  0,   0, 1, 0,
                                  0,   0, 0, 1);
        }

        public static Matrix4 RorationAround(double angle, Vector3 v)
        {
            double x = v.X;
            double y = v.Y;
            double z = v.Z;
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            return new Matrix4(    cos + x * x * (1 - cos), x * y * (1 - cos) + z * sin, x * z * (1 - cos) + y * sin, 0, 
                               x * y * (1 - cos) + z * sin,     cos + y * y * (1 - cos), y * x * (1 - cos) - x * sin, 0, 
                               x * z * (1 - cos) - y * sin, y * z * (1 - cos) + x * sin,     cos + z * z * (1 - cos), 0, 
                                                         0,                           0,                           0, 1);
        }

        public static Matrix4 Identity()
        {
            return new Matrix4(1, 0, 0, 0, 
                               0, 1, 0, 0, 
                               0, 0, 1, 0, 
                               0, 0, 0, 1);
        }

        public static Matrix4 Translation(Vector3 v)
        {
            return new Matrix4(  1,   0,   0, 0,
                                 0,   1,   0, 0,
                                 0,   0,   1, 0,
                               v.X, v.Y, v.Z, 1);
        }

        public Vector3 Transform(Vector3 v)
        {
            Vector3 result = new Vector3(v.X * m00 + v.Y * m01 + v.Z * m02 + m03,
                                         v.X * m10 + v.Y * m11 + v.Z * m12 + m13,
                                         v.X * m20 + v.Y * m21 + v.Z * m22 + m23);
            return result;
        }

        public Vector3 GetTransform()
        {
            return new Vector3(m30, m31, m32);
        }
    }
}
