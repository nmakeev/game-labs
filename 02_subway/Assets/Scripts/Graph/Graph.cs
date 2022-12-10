using System.Collections.Generic;
using System.Drawing;

namespace Graph
{
    public class Graph : IGraph
    {
        private readonly List<Point> _points;
        private readonly double _defaultWeight;

        public Graph(double defaultWeight)
        {
            _points = new List<Point>();
            _defaultWeight = defaultWeight;
        }

        public Point CreatePoint(string label)
        {
            var point = new Point(label);
            _points.Add(point);
            return point;
        }

        public double GetEdgeWeight(Point source, Point destination)
        {
            return _defaultWeight;
        }

        public IEnumerable<Point> GetNeighbors(Point source)
        {
            foreach (var edge in source.Edges)
            {
                yield return edge.Destination;
            }
        }

        public void AddEdge(Point source, Point destination, Color edgeColor)
        {
            source.Edges.Add(new Edge(destination, edgeColor));
            destination.Edges.Add(new Edge(source, edgeColor));
        }

        public void AddLine(Color lineColor, params Point[] points)
        {
            for (var i = 1; i < points.Length; i++)
            {
                var previousPoint = points[i - 1];
                var point = points[i];
                
                AddEdge(point, previousPoint, lineColor);
            }
        }

        public Point FindPointByLabel(string label)
        {
            foreach (var point in _points)
            {
                if (point.Label == label)
                {
                    return point;
                }
            }

            return null;
        }
    }
}