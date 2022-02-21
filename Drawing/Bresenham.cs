using System;
using System.Drawing;

namespace laba1.Drawing
{
    public static class Bresenham
    {
        public static void DrawLine(Bitmap bitmap, Color color, Point from, Point to)
        {  
            var x1 = from.X;
            var y1 = from.Y;
            var x2 = to.X;
            var y2 = to.Y;

            int dx = Math.Abs(x2 - x1);
            int dy = Math.Abs(y2 - y1);
            int sx = x1 < x2 ? 1 : -1;
            int sy = y1 < y2 ? 1 : -1;
            int e = dx - dy;

            bitmap.SetPixel(x2, y2, color);
            
            while (x1 != x2 || y1 != y2)
            {
                bitmap.SetPixel(x1, y1, color);
                int e2 = e * 2;
                if (e2 > -dy)
                {
                    e -= dy;
                    x1 += sx;
                }
                if (e2 < dx)
                {
                    e += dx;
                    y1 += sy;
                }
            }
        }

        public static void DrawCircle(Bitmap bitmap, Color color, Point center, int radius)
        {
            var x = center.X;
            var y = center.Y;

            int dx = 0;
            int dy = radius;
            int d = 1 - 2 * radius;

            while (dy >= dx)
            {
                bitmap.SetPixel(x + dx, y + dy, color);
                bitmap.SetPixel(x + dx, y - dy, color);
                bitmap.SetPixel(x - dx, y + dy, color);
                bitmap.SetPixel(x - dx, y - dy, color);
                bitmap.SetPixel(x + dy, y + dx, color);
                bitmap.SetPixel(x + dy, y - dx, color);
                bitmap.SetPixel(x - dy, y + dx, color);
                bitmap.SetPixel(x - dy, y - dx, color);

                int e = 2 * (d + dy) - 1;
                if (d < 0 && e <= 0)
                    d += 2 * ++dx + 1;
                else if (d > 0 && e > 0)
                    d -= 2 * --dy + 1;
                else
                    d += 2 * (++dx - --dy);
            }
        }
    }
}
