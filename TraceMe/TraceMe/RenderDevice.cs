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
        private const int depth = 3;
        private Bitmap buffer;
        private Object locking = new object();
        private List<Point> points = new List<Point>();
        private Vector3 eye;

        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }

        Random rnd = new Random();

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
            bmpData = buffer.LockBits(new Rectangle(new Point(), buffer.Size), ImageLockMode.WriteOnly, buffer.PixelFormat);
            int multiplyer = Bitmap.GetPixelFormatSize(bmpData.PixelFormat) / 8;
            var dataArray = new byte[bmpData.Width * bmpData.Height * multiplyer];
            var bWidth = buffer.Width;

            var brush = new SolidBrush(Color.White);

            Parallel.ForEach(points, p =>
            {
                if (rnd.Next(100) != 0)
                    return;

                int x = p.X;
                int y = p.Y;
                Vector3 point = new Vector3(x - ScreenWidth / 2, ScreenHeight / 2 - y, 0);
                Vector3 direction = point - eye;
                direction.Normalize();
                Lay lay = new Lay(point, direction);

                Color c = Render(lay, 0);
                dataArray[(x + y * bWidth) * multiplyer + 0] = c.B;
                dataArray[(x + y * bWidth) * multiplyer + 1] = c.G;
                dataArray[(x + y * bWidth) * multiplyer + 2] = c.R;
                dataArray[(x + y * bWidth) * multiplyer + 3] = c.A;
            });

            Marshal.Copy(dataArray, 0, bmpData.Scan0, dataArray.Length);
            buffer.UnlockBits(bmpData);
            lock (locking)
            {
                Graphics.DrawImage(buffer, 0, 0, Graphics.VisibleClipBounds.Width, Graphics.VisibleClipBounds.Height);
            }
        }

        private Color Render(Lay lay, int d)
        {
            Color result = FillColor;

            if (d >= depth)
                return result;

            var min = double.PositiveInfinity;
            Hit hit = null;
            for (int i = 0; i < Objects.Count; i++)
            {
                var hit_ = Objects[i].Intersections(lay);
                if (hit_ == null)
                    continue;
                if (hit_.Distance < min && hit_.Distance > 0)
                {
                    min = hit_.Distance;
                    hit = hit_;
                }
            }


            if (hit == null)
                return result;

            result = hit.Color;

            Vector3 hitPoint = lay.Point + lay.Direction * hit.Distance;

            if (hit.Reflection > 0)
            {
                Vector3 newDirection = lay.Direction - 2 * lay.Direction.DotProduct(hit.Normal) / hit.Normal.LenghtSq() * hit.Normal;
                Lay reflected = new Lay(hitPoint, newDirection);
                Color reflectedColor = Render(reflected, d + 1);
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
            Vector3 direction = point - eye;
            direction.Normalize();
            Lay lay = new Lay(point, direction);

            Color c = Render(lay, 0);

            lock (locking)
            {
                Graphics.FillRectangle(new SolidBrush(c), x, y, 1, 1);
            }
        }
    }
}
