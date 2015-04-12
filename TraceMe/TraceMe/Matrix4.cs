using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraceMe
{
    public class Matrix4
    {
        private double[,] values;

        private const int rows = 4;
        private const int cols = 4;

        public Matrix4()
        {
            values = new double[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    values[i, j] = 0;
                }
            }
        }

        public static Matrix4 operator *(Matrix4 a, Matrix4 b)
        {
            Matrix4 result = new Matrix4();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        result.values[i, j] += a.values[i, k] * b.values[k, j];
                    }
                }
            }

            return result;
        }

        public static Matrix4 RotationX(double angle)
        {
            var cos = Math.Cos(angle);
            var sin = Math.Sin(angle);
            Matrix4 result = new Matrix4();
            result.values = new double[,]{{1,   0,    0, 0},
                                          {0, cos, -sin, 0},
                                          {0, sin,  cos, 0},
                                          {0,   0,    0, 1}};
            return result;
        }

        public static Matrix4 RotationY(double angle)
        {
            var cos = Math.Cos(angle);
            var sin = Math.Sin(angle);
            Matrix4 result = new Matrix4();
            result.values = new double[,]{{ cos, 0, sin, 0},
                                          {   0, 1,   0, 0},
                                          {-sin, 0, cos, 0},
                                          {   0, 0,   0, 1}};
            return result;
        }

        public static Matrix4 RotationZ(double angle)
        {
            var cos = Math.Cos(angle);
            var sin = Math.Sin(angle);
            Matrix4 result = new Matrix4();
            result.values = new double[,]{{cos, sin, 0, 0},
                                         {-sin, cos, 0, 0},
                                         {   0,   0, 1, 0},
                                         {   0,   0, 0, 1}};
            return result;
        }

        public static Matrix4 RorationAround(double angle, Vector3 v)
        {
            double x = v.X;
            double y = v.Y;
            double z = v.Z;
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            return new Matrix4()
            {
                values = new double[,]{{    cos + x * x * (1 - cos), x * y * (1 - cos) + z * sin, x * z * (1 - cos) + y * sin, 0}, 
                                       {x * y * (1 - cos) + z * sin,     cos + y * y * (1 - cos), y * x * (1 - cos) - x * sin, 0}, 
                                       {x * z * (1 - cos) - y * sin, y * z * (1 - cos) + x * sin,     cos + z * z * (1 - cos), 0}, 
                                       {                          0,                           0,                           0, 1}}
            };
        }

        public static Matrix4 Identity()
        {
            return new Matrix4() {
                values = new double[,] {{1, 0, 0, 0}, 
                                        {0, 1, 0, 0}, 
                                        {0, 0, 1, 0}, 
                                        {0, 0, 0, 1}}
            };
        }

        public static Matrix4 Translation(Vector3 v)
        {
            return new Matrix4()
            {
                values = new double[,] {{  1,   0,   0, 0},
                                        {  0,   1,   0, 0},
                                        {  0,   0,   1, 0},
                                        {v.X, v.Y, v.Z, 1} }
            };
        }

        public Vector3 Transform(Vector3 v)
        {
            Vector3 result = new Vector3(v.X * values[0, 0] + v.Y * values[0, 1] + v.Z * values[0, 2] + values[0, 3],
                                        v.X * values[1, 0] + v.Y * values[1, 1] + v.Z * values[1, 2] + values[1, 3],
                                        v.X * values[2, 0] + v.Y * values[2, 1] + v.Z * values[2, 2] + values[2, 3]);
            return result;
        }
    }
}
