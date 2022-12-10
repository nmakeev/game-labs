using System.Collections.Generic;

namespace Model
{
    public class Point
    {
        public int Id { get; private set; }
        
        public string Label { get; private set; }

        public List<Edge> Edges { get; private set; }

        public Point(int id, string label)
        {
            Id = id;
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
            return $"Point, Id = {Id.ToString()}, Label = {Label}";
        }
    }
}