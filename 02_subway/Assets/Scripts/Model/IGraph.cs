using System.Collections.Generic;

namespace Model
{
    public interface IGraph
    {
        IEnumerable<Point> GetNeighbors(Point source);

        double GetEdgeWeight(Point source, Point destination);

    }
}