using System.Drawing;

namespace Graph
{
    public class Edge
    {
        public Color Color;
        
        public Point Destination { get; }

        public Edge(Point destination, Color color)
        {
            Destination = destination;
            Color = color;
        }

        public override string ToString()
        {
            return $"Edge to {Destination}";
        }
    }
}