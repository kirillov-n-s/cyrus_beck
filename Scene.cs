using laba1.Drawing;
using laba1.Ext;
using laba1.Geometry;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

namespace laba1
{
    public class Scene
    {
        readonly PictureBox _picturebox;
        readonly int _width, _height;
        readonly Color _colpoly, _colseg;
        //readonly int _ptrad;

        readonly Polygon _polygon = new Polygon();
        readonly List<Segment> _segments = new List<Segment>();

        bool _has_start;
        Vector2 _start;

        void RenderSegment(Bitmap bitmap, Color color, Segment segment)
        {
            Bresenham.DrawLine(bitmap, color, segment.Start.ToPoint(), segment.End.ToPoint());
        }

        /*void RenderVertex(Bitmap bitmap, Color color, Vector2 vertex)
        {
            Bresenham.DrawCircle(bitmap, color, vertex.ToPoint(), _ptrad);
        }*/

        void RenderSegments(Bitmap bitmap, Color color, IEnumerable<Segment> segments)
        {
            foreach (var segment in segments)
            {
                RenderSegment(bitmap, color, segment);
                /*RenderVertex(bitmap, color, segment.Start);
                RenderVertex(bitmap, color, segment.End);*/
            }
        }

        void Render(IEnumerable<Segment> visible = null)
        {
            var bitmap = new Bitmap(_width, _height);
            RenderSegments(bitmap, _colpoly, _polygon.Segments);
            RenderSegments(bitmap, _colseg, visible ?? _segments);
            _picturebox.Image?.Dispose();
            _picturebox.Image = bitmap;
            _picturebox.Refresh();
        }

        public Scene(PictureBox picturebox, Color poly, Color seg/*, int rad = 1*/)
        {
            _picturebox = picturebox;
            _width = picturebox.Width;
            _height = picturebox.Height;
            _picturebox.Image = new Bitmap(_width, _height);
            _colpoly = poly;
            _colseg = seg;
            //_ptrad = rad;
        }

        public void AddPolygonPoint(Point point)
        {
            _polygon.Add(point.ToVector2());
            Render();
        }

        public void AddSegmentPoint(Point point)
        {
            var vertex = point.ToVector2();
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

        /*public void RemoveLastSegment()
        {
            if (_has_start)
                _has_start = false;
            else
                _segments.RemoveAt(_segments.Count - 1);
            Render();
        }*/

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
