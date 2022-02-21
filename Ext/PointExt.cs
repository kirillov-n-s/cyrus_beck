using System.Drawing;
using System.Numerics;

namespace laba1.Ext
{
    public static class PointExt
    {
        public static Vector2 ToVector2(this Point p)
        {
            return new Vector2(p.X, p.Y);
        }
    }
}
