using laba1.Geometry;
using System;
using System.Drawing;

namespace laba1.Drawing
{
    public static class Bresenham
    {
        public static void DrawLine(Bitmap bitmap, Color color, Segment segment)
        {  
            var x1 = (int)Math.Truncate(segment.Start.X);
            var y1 = (int)Math.Truncate(segment.Start.Y);
            var x2 = (int)Math.Truncate(segment.End.X);
            var y2 = (int)Math.Truncate(segment.End.Y);

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
    }
}
