using System.Collections.Generic;
using System.Drawing;

namespace Graph
{
    public class Graph : IGraph
    {
        private Dictionary<int, Point> _points;
        private readonly double _defaultWeight;
        
        private int _nextId;

        public Graph(double defaultWeight)
        {
            _points = new Dictionary<int, Point>();
            _defaultWeight = defaultWeight;
        }

        public Point CreatePoint(string label)
        {
            var point = new Point(_nextId, label);
            _points[point.Id] = point;
            _nextId++;
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
            _points[source.Id].Edges.Add(new Edge(destination, edgeColor));
            _points[destination.Id].Edges.Add(new Edge(source, edgeColor));
        }

        public Point FindPointByLabel(string label)
        {
            foreach (var value in _points.Values)
            {
                if (value.Label == label)
                {
                    return value;
                }
            }

            return null;
        }
    }
}