using System.Collections.Generic;

namespace Graph
{
    public static class Pathfinder
    {
        public static (List<Point> points, int colorChanges) FindPath(IGraph graph, Point source, Point destination)
        {
            var cameFrom = new Dictionary<Point, Point>();
            var fScores = new Dictionary<Point, double>();
            var gScores = new Dictionary<Point, double>();

            var openNodes = new List<Point>();

            cameFrom[source] = source;
            fScores.Add(source, 0);
            gScores.Add(source, 0);

            openNodes.Add(source);
            
            while (openNodes.Count > 0)
            {
                var current = FindPointWithMinFScore(openNodes, fScores);
                openNodes.Remove(current);
                if (current.Id == destination.Id)
                {
                    var path = new List<Point>();
                    while (current != source)
                    {
                        path.Add(current);
                        current = cameFrom[current];
                    }

                    path.Add(source);
                    path.Reverse();

                    var colorChanges = CalculateColorChanges(path);
                    return (path, colorChanges);
                }

                foreach (var next in graph.GetNeighbors(current))
                {
                    var newCost = gScores[current] + graph.GetEdgeWeight(current, next);
                    if (!gScores.ContainsKey(next) || newCost < gScores[next])
                    {
                        gScores[next] = newCost;
                        fScores[next] = newCost;
                        openNodes.Add(next);
                        cameFrom[next] = current;
                    }
                }
            }

            return default;
        }
        
        private static Point FindPointWithMinFScore(IReadOnlyList<Point> points, Dictionary<Point, double> fScores)
        {
            var result = points[0];
            var minScore = fScores[result];

            for (var i = 1; i < points.Count; i++)
            {
                var point = points[i];
                var score = fScores[point];
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