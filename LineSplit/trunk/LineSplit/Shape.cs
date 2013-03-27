using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace LineSplit
{
    public enum ClipType
    {
        Coin,
        Beck
    }

    public class Shape
    {
        public Point[] Points { get; private set; }
        public Shape(int count)
        {
            Points = new Point[count];
        }

        private byte PointCode(Point p, RectangleF clipRegion)
        {
            byte b1 = 0;
            byte b2 = 0;
            byte b3 = 0;
            byte b4 = 0;

            if (p.X < clipRegion.Left)
                b1 = 1;
            if (p.X > clipRegion.Right)
                b2 = 1;
            if (p.Y < clipRegion.Top)
                b4 = 1;
            if (p.Y > clipRegion.Bottom)
                b3 = 1;

            return (byte)((b1 << 3) + (b2 << 2) + (b3 << 1) + (b4));
        }

        private void CoinClip(Graphics g)
        {
            int l = (int)g.ClipBounds.Left;
            int r = (int)g.ClipBounds.Right;
            int t = (int)g.ClipBounds.Top;
            int b = (int)g.ClipBounds.Bottom;

            for (int i = 0; i < Points.Length; i++)
            {
                Point p1 = Points[i];
                Point p2 = Points[(i + 1) % Points.Length];

                byte code1 = PointCode(p1, g.ClipBounds);
                byte code2 = PointCode(p2, g.ClipBounds);

                double dxdy = 0;
                if (p2.Y - p1.Y != 0)
                    dxdy = (p2.X - p1.X) / (p2.Y - p1.Y);

                double dydx = 0;
                if (p2.X - p1.X != 0)
                        dydx = (p2.Y - p1.Y) / (p2.X - p1.X);


                for (int j = 0; j < 4; j++)
                {
                    if (code1 == 0 && code2 == 0 || code1 == code2)
                        break;

                    if (code1 == 0)
                    {
                        var ttt = p1;
                        p1 = p2;
                        p2 = ttt;

                        var tt = code1;
                        code1 = code2;
                        code2 = tt;
                    }

                    if ((code1 & 1) == 1)
                    {
                        p1.X += (int)(dxdy * (t - p1.Y));
                        p1.Y = t;
                    } 
                    else if ((code1 & 2) == 2)
                    {
                        p1.X += (int)(dxdy * (b - p1.Y));
                        p1.Y = b;
                    }
                    else if ((code1 & 4) == 4)
                    {
                        p1.Y += (int)(dydx * (r - p1.X));
                        p1.X = r;
                    }
                    else if ((code1 & 8) == 8)
                    {
                        p1.Y += (int)(dydx * (l - p1.X));
                        p1.X = l;
                    }

                    code1 = PointCode(p1, g.ClipBounds);
                }

                g.DrawLine(new Pen(Color.Orange), p1, p2);
            }
        }

        private void BeckClip(Graphics g)
        {
        }

        public void Draw(Graphics g, ClipType clipType)
        {
            g.FillRectangle(new SolidBrush(Color.White), g.ClipBounds);
            switch (clipType)
            {
                case ClipType.Coin:
                    CoinClip(g);
                    break;
                case ClipType.Beck:
                    BeckClip(g);
                    break;
                default:
                    break;
            }
        }

        public void Translate(int dx, int dy)
        {
            for (int i = 0; i < Points.Length; i++)
            {
                Points[i].X += dx;
                Points[i].Y += dy;
            }
        }
    }
}
