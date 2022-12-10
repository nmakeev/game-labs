using System.Collections.Generic;

namespace Graph
{
    public class Point
    {
        public string Label { get; private set; }

        public List<Edge> Edges { get; private set; }

        public Point(string label)
        {
            Label = label;
            Edges = new List<Edge>();
        }

        public Edge FindEdgeTo(Point point)
        {
            foreach (var edge in Edges)
            {
                if (edge.Destination == point)
                {
                    return edge;
                }
            }

            return null;
        }

        public override string ToString()
        {
            return $"Point, Label = {Label}";
        }
    }
}