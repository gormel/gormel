using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TraceMe
{
    public class RenderDevice
    {
        public Graphics Graphics { get; private set; }
        public List<BaseObject> Objects { get; private set; }
        public Color FillColor { get; set; }
        public double Fov { get; set; }
        private int depth = 3;
        private Bitmap buffer;
        private Object locking = new object();
        private List<Point> points = new List<Point>();
        private Vector3 eye;

        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }

        public RenderDevice(Graphics g, int sWidth, int sHeight, double fov)
        {
            ScreenHeight = sHeight;
            ScreenWidth = sWidth;
            Fov = fov;
            Graphics = g;
            Objects = new List<BaseObject>();
            buffer = new Bitmap(ScreenWidth, ScreenHeight);

            double cos = Math.Cos(Fov);
            double h = ScreenWidth / 2 * Math.Sqrt((1 + cos) / (1 - cos));
            eye = new Vector3(0, 0, -h);

            points = (from x in Enumerable.Range(0, ScreenWidth)
                      from y in Enumerable.Range(0, ScreenHeight)
                      select new Point(x, y)).ToList();
        }

        public void RenderScene()
        {
            BitmapData bmpData = null;
            lock (locking)
            {
                bmpData = buffer.LockBits(new Rectangle(new Point(), buffer.Size), ImageLockMode.WriteOnly, buffer.PixelFormat);
            }
            int multiplyer = Bitmap.GetPixelFormatSize(bmpData.PixelFormat) / 8;
            var dataArray = new byte[bmpData.Width * bmpData.Height * multiplyer];

            var brush = new SolidBrush(Color.White);

            Parallel.ForEach(points, p =>
                             {
                                 int x = p.X;
                                 int y = p.Y;
                                 Vector3 point = new Vector3(x - ScreenWidth / 2, ScreenHeight / 2 - y, 0);
                                 Vector3 direction = (point - eye).Normalize();
                                 Lay lay = new Lay(point, direction);

                                 Color c = Render(lay);
                                 lock (locking)
                                 {
                                     dataArray[(x + y * buffer.Width) * multiplyer + 0] = c.B;
                                     dataArray[(x + y * buffer.Width) * multiplyer + 1] = c.G;
                                     dataArray[(x + y * buffer.Width) * multiplyer + 2] = c.R;
                                     dataArray[(x + y * buffer.Width) * multiplyer + 3] = c.A;
                                 }

                                 //lock (locking)
                                 //{
                                 //    buffer.SetPixel(x, y, c);
                                 //}
            });

            Marshal.Copy(dataArray, 0, bmpData.Scan0, dataArray.Length);
            lock (locking)
            {
                buffer.UnlockBits(bmpData);
                Graphics.DrawImage(buffer, 0, 0, Graphics.VisibleClipBounds.Width, Graphics.VisibleClipBounds.Height);
            }
        }

        private Color Render(Lay lay)
        {
            Color result = FillColor;
            var hits = Objects.Select(o => o.Intersections(lay));

            var min = double.PositiveInfinity;
            Hit hit = null;
            foreach (var hit_ in hits)
            {
                if (hit_ == null)
                    continue;
                if (hit_.Distance < min && hit_.Distance >= 0)
                {
                    min = hit_.Distance;
                    hit = hit_;
                }
            }


            if (hit == null)
                return result;

            result = hit.Color;

            Vector3 hitPoint = lay.Point + lay.Direction.Normalize() * hit.Distance;

            if (hit.Reflection > 0)
            {
                double angle = Math.PI - 2 * Math.Acos(lay.Direction.DotProduct(hit.Normal) / lay.Direction.Lenght() / hit.Normal.Lenght());
                Vector3 newDirection = Matrix4.RorationAround(angle, lay.Direction.CrossProduct(hit.Normal)).Transform(lay.Direction);
                Lay reflected = new Lay(hitPoint, newDirection);
                Color reflectedColor = Render(reflected);
                double r = result.R + (reflectedColor.R * hit.Reflection);
                double g = result.G + (reflectedColor.G * hit.Reflection);
                double b = result.B + (reflectedColor.B * hit.Reflection);

                var max = Math.Max(r, Math.Max(g, b));
                r /= max;
                g /= max;
                b /= max;

                result = Color.FromArgb((byte)(255 * r), (byte)(255 * g), (byte)(255 * b));
            }

            return result;
        }

        public void Render(int x, int y)
        {
            Vector3 point = new Vector3(x - ScreenWidth / 2, ScreenHeight / 2 - y, 0);
            double cos = Math.Cos(Fov);
            double h = ScreenWidth / 2 * Math.Sqrt((1 + cos) / (1 - cos));
            Vector3 eye = new Vector3(0, 0, -h);
            Vector3 direction = (point - eye).Normalize();
            //double angleX = (0.25 - x / 2 / ScreenWidth) * Fov;
            //double angleY = (y / 2 / ScreenHeight - 0.25) * Fov;
            //Matrix4 rotation = Matrix4.RotationY(angleX) * Matrix4.RotationX(-angleY);
            //Vector3 direction = rotation.Transform(Vector3.Forward);
            Lay lay = new Lay(point, direction);

            Color c = Render(lay);

            lock (locking)
            {
                Graphics.FillRectangle(new SolidBrush(c), x, y, 1, 1);
            }
        }
    }
}
