using System.Numerics;

namespace laba1.Geometry
{
    public struct Segment
    {
        public readonly Vector2 Start, End, Direction, Normal;

        public Segment(Vector2 start, Vector2 end)
        {
            Start = start;
            End = end;
            Direction = End - Start;
            Normal = new Vector2(Direction.Y, -Direction.X);
        }

        /*
        Точка находится слева от отрезка,
        если 2d вект. произв. неотрицательно
        */
        public bool OnLeft(Vector2 point)
        {
            return Direction.Cross(point - Start) >= 0;
        }

        /*
        Пусть:
            p = Start
            q = other.Start
            r = Direction
            s = other.Direction

        Параметрическое представление отрезков:
            this  = p + tr
            other = q + us,
        где t, u — параметры

        Пересечение при:
            p + tr = q + us

        Решим для t:
        2d вект. произв. (обозн. &) обеих сторон с s:
            (p + tr) & s = (q + us) & s
        s & s = 0:
            t(r & s) = (q - p) & s
            t = ((q - p) & s) / (r & s)
        */
        public float Intersect(Segment other)
        {
            var p = Start;
            var q = other.Start;
            var r = Direction;
            var s = other.Direction;
            var t = (q - p).Cross(s) / r.Cross(s);
            return !float.IsNaN(t) ? t : 0.0f;
        }

        public Segment Clip(float ta, float tb)
        {
            var a = Start;
            var d = Direction;
            return new Segment(a + ta * d, a + tb * d);
        }
    }
}
