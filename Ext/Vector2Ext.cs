using System;
using System.Drawing;
using System.Numerics;

namespace laba1.Geometry
{
    public static class Vector2Ext
    {
        public static float Cross(this Vector2 a, Vector2 b)
        {
            return a.X * b.Y - a.Y * b.X;
        }

        public static Point ToPoint(this Vector2 v)
        {
            return new Point((int)Math.Round(v.X), (int)Math.Round(v.Y));
        }
    }
}
