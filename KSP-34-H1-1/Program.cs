using System;
using System.Collections.Generic;
using System.Linq;

namespace KSP_34_H1_1
{
    class Program
    {
        private const ulong UNLIMITED_TIME_VALUE = uint.MaxValue;
        static void Main(string[] args)
        {
            var firstLineStrs = Console.ReadLine()?.Split(' ');
            var n = uint.Parse(firstLineStrs[0]);
            var m = uint.Parse(firstLineStrs[1]);
            var startId = uint.Parse(firstLineStrs[2]);
            var targetId = uint.Parse(firstLineStrs[3]);
            var timeSkiCost = uint.Parse(firstLineStrs[4]);

            var points = new Point[n * 2];
            Point start;
            Point target;

            for (uint i = 0; i < n; i++)
            {
                SetSkiPoint(points, i, new Point(i, Point.PointType.Ski));
                SetWalkPoint(points, i, new Point(i, Point.PointType.Walk), n);
            }

            for (var i = 0; i < m; i++)
            {
                var strs = Console.ReadLine().Split(' ');

                var a = uint.Parse(strs[0]);    // first id
                var b = uint.Parse(strs[1]);    // second id
                var x = uint.Parse(strs[2]);    // ski time
                var y = uint.Parse(strs[3]);    // walk time

                var aSP = GetSkiPoint(points, a);       // SKI
                var aWP = GetWalkPoint(points, a, n);   // WALK

                var bSP = GetSkiPoint(points, b);       // SKI
                var bWP = GetWalkPoint(points, b, n);   // WALK

                //  VERTICAL
                AddNeighborhood(aSP, aWP, timeSkiCost);
                AddNeighborhood(bSP, bWP, timeSkiCost);

                //  HORIZONTAL
                AddNeighborhood(aSP, bSP, x);
                AddNeighborhood(aWP, bWP, y);
            }

            start = GetSkiPoint(points, startId);
            target = GetWalkPoint(points, targetId, n);

            var bestTimes = CalculateBestTimes(start, points, timeSkiCost);

            var bestWayTime = bestTimes[target];
            
            Console.WriteLine(bestWayTime);
        }

        static IDictionary<Point, ulong> CalculateBestTimes(Point start, Point[] allPoints, uint skiTimeCost)
        {
            IDictionary<Point, ulong> bestTimes = new Dictionary<Point, ulong>();

            bestTimes.Add(start, 0);

            var points = new List<Point>(allPoints);

            foreach (var p in points)
            {
                p.Time = UNLIMITED_TIME_VALUE;
            }

            start.Time = 0;

            BubbleUpPoint(points, start);

            while (0 < points.Count)
            {
                var p = points.First();

                if (p.Time == UNLIMITED_TIME_VALUE)
                {
                    return bestTimes;
                }
                
                foreach (var neighbor in p.Neighbors)
                {
                    var p2 = neighbor.GetTheSecondPoint(p);
                    
                    var t = p.Time + neighbor.Weight;

                    if (t < p2.Time)
                    {
                        p2.Time = t;
                    }

                    BubbleUpPoint(points, p2);
                }
                
                bestTimes[p] = p.Time;
                points.Remove(p);
            }

            return bestTimes;
        }

        static void BubbleUpPoint(IList<Point> points, Point p)
        {
            var index = points.IndexOf(p);
            
            index--;

            while (0 < index)
            {
                var p2 = points[index];

                if (p.Time >= p2.Time)
                {
                    return;
                }

                points[index] = p;
                points[index + 1] = p2;

                index--;
            }
        }

        static Point GetSkiPoint(Point[] points, uint id) => points[id];

        static void SetSkiPoint(Point[] points, uint id, Point point) => points[id] = point;

        static Point GetWalkPoint(Point[] points, uint id, uint n) => points[id + n];

        static void SetWalkPoint(Point[] points, uint id, Point point, uint n) => points[id + n] = point;

        static void AddNeighborhood(Point a, Point b, ulong weight)
        {
            var neighborhood = new Neighborhood(a, b, weight);

            a.AddNeighbor(neighborhood);
            b.AddNeighbor(neighborhood);
        }
    }
}
