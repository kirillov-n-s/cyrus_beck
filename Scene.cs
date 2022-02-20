using laba1.Drawing;
using laba1.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba1
{
    public class Scene
    {
        readonly PictureBox _picturebox;
        readonly int _width, _height;
        readonly Color _colpoly, _colseg;

        readonly Polygon _polygon = new Polygon();
        readonly List<Segment> _segments = new List<Segment>();

        bool _has_start;
        Vector2 _start;

        void Render(IEnumerable<Segment> visible = null)
        {
            var bitmap = new Bitmap(_width, _height);
            foreach (var segment in _polygon.Segments)
                Bresenham.DrawLine(bitmap, _colpoly, segment);
            foreach (var segment in visible ?? _segments)
                Bresenham.DrawLine(bitmap, _colseg, segment);
            _picturebox.Image?.Dispose();
            _picturebox.Image = bitmap;
            _picturebox.Refresh();
        }

        public Scene(PictureBox picturebox, Color poly, Color seg)
        {
            _picturebox = picturebox;
            _width = picturebox.Width;
            _height = picturebox.Height;
            _colpoly = poly;
            _colseg = seg;
        }

        public void AddPolygonPoint(Point point)
        {
            _polygon.Add(new Vector2(point.X, point.Y));
            Render();
        }

        public void AddSegmentPoint(Point point)
        {
            var vertex = new Vector2(point.X, point.Y);
            if (!_has_start)
            {
                _start = vertex;
                _has_start = true;
                return;
            }

            _has_start = false;
            _segments.Add(new Segment(_start, vertex));
            Render();
        }

        public void RemoveLastPolygonPoint()
        {
            _polygon.RemoveLast();
            Render();
        }

        public void ApplyCyrusBeck(bool apply = true)
        {
            var visible = apply ? _polygon.CyrusBeck(_segments) : null;
            Render(visible);
        }

        public void Clear()
        {
            _has_start = false;
            _polygon.Clear();
            _segments.Clear();
            _picturebox.Image.Dispose();
            _picturebox.Image = new Bitmap(_width, _height);
            _picturebox.Refresh();
        }
    }
}
