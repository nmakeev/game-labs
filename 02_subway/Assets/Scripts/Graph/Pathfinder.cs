using System.Collections.Generic;

namespace Graph
{
    public static class Pathfinder
    {
        private class AStarPoint
        {
            public double F;
            public double G;
            public Point CameFrom;
        }
        
        public static (List<Point> points, int colorChanges) FindPath(IGraph graph, Point source, Point destination)
        {
            var openNodes = new List<Point> { source };
            var aStarPoints = new Dictionary<Point, AStarPoint>
            {
                { source, new AStarPoint() }
            };

            while (openNodes.Count > 0)
            {
                var current = FindPointWithMinFScore(openNodes, aStarPoints);
                openNodes.Remove(current);
                if (current == destination)
                {
                    var path = new List<Point>();
                    while (current != source)
                    {
                        path.Add(current);
                        current = aStarPoints[current].CameFrom;
                    }

                    path.Add(source);
                    path.Reverse();

                    var colorChanges = CalculateColorChanges(path);
                    return (path, colorChanges);
                }

                foreach (var next in graph.GetNeighbors(current))
                {
                    var newCost = aStarPoints[current].G + graph.GetEdgeWeight(current, next);
                    if (!aStarPoints.ContainsKey(next) || newCost < aStarPoints[next].G)
                    {
                        var aStarPoint = new AStarPoint
                        {
                            F = newCost,
                            G = newCost,
                            CameFrom = current
                        };
                        aStarPoints[next] = aStarPoint;
                        openNodes.Add(next);
                    }
                }
            }

            return default;
        }
        
        private static Point FindPointWithMinFScore(IReadOnlyList<Point> points, Dictionary<Point, AStarPoint> aStarPoints)
        {
            var result = points[0];
            var minScore = aStarPoints[result].F;

            for (var i = 1; i < points.Count; i++)
            {
                var point = points[i];
                var score = aStarPoints[point].F;
                if (minScore > score)
                {
                    result = point;
                    minScore = score;
                }
            }

            return result;
        }

        private static int CalculateColorChanges(IReadOnlyList<Point> path)
        {
            if (path.Count < 3)
            {
                return 0;
            }

            var firstPoint = path[0];
            var edge = firstPoint.FindEdgeTo(path[1]);
            var currentColor = edge.Color;
            var result = 0;

            for (var i = 2; i < path.Count; i++)
            {
                edge = path[i - 1].FindEdgeTo(path[i]);
                if (edge.Color == currentColor)
                {
                    continue;
                }
                
                currentColor = edge.Color;
                result++;
            }

            return result;
        }
    }
}