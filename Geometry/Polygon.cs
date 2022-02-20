using System;
using System.Collections.Generic;
using System.Numerics;

namespace laba1.Geometry
{
    public class Polygon
    {
        #region private
        List<Vector2> _points = new List<Vector2>();

        bool CyrusBeck(Segment to_clip, out Segment clipped)
        {
            clipped = new Segment();
            var dir = to_clip.Direction;
            var ta = 0.0f;
            var tb = 1.0f;
            foreach (var segment in Segments)
            {
                float t;
                var dot = Vector2.Dot(segment.Normal, dir);
                switch (Math.Sign(dot))
                {
                    case -1:
                        t = to_clip.Intersect(segment);
                        ta = t > ta ? t : ta;
                        break;
                    case +1:
                        t = to_clip.Intersect(segment);
                        tb = t < tb ? t : tb;
                        break;
                    case 0:
                        if (!segment.OnLeft(to_clip.Start))
                            return false;
                        break;
                }
            }
            if (ta > tb)
                return false;
            clipped = to_clip.Clip(ta, tb);
            return true;
        }
        #endregion

        #region public
        public void Add(Vector2 point)
        {
            _points.Add(point);
        }

        public void RemoveLast()
        {
            _points.RemoveAt(Count - 1);
        }

        public void Clear()
        {
            _points.Clear();
        }

        public int Count => _points.Count;

        public bool IsConvex
        {
            get
            {
                if (Count > 3)
                    for (int a = Count - 2, b = Count - 1, p = 0; p < Count; a = b, b = p, ++p)
                        if (!new Segment(_points[a], _points[b]).OnLeft(_points[p]))
                            return false;
                return true;
            }
        }

        public IEnumerable<Segment> Segments
        {
            get
            {
                if (Count >= 2)
                    for (int a = Count - 1, b = 0; b < Count; a = b, ++b)
                        yield return new Segment(_points[a], _points[b]);
            }
        }

        public IEnumerable<Segment> CyrusBeck(IEnumerable<Segment> to_clip)
        {
            if (!IsConvex)
            {
                _points.Reverse();
                if (!IsConvex)
                    throw new ArgumentException("Многоугольник для отсечения Кируса-Бека должен быть выпуклым.");
            }

            var result = new List<Segment>();
            foreach (var segment in to_clip)
                if (CyrusBeck(segment, out Segment clipped))
                    result.Add(clipped);
            return result;
        }
        #endregion
    }
}
