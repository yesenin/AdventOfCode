using System.Text;
using Common;

namespace AdventOfCode.Year2025;

public sealed class Day08Part1 : IProblemWithInput
{
    public string GetAnswer()
    {
        ulong answer = 0;
        var lines = Input.Split('\n');

        var points = new List<Point3d>();
        
        foreach (var line in lines)
        {
            var parts = line.Split(",").Select(x => int.Parse(x)).ToArray();
            points.Add(new Point3d(parts[0], parts[1], parts[2]));
        }
        
        var dict = new Dictionary<int, List<Distance>>();
        
        for (var i = 0; i < points.Count; i++)
        {
            dict.Add(i, new List<Distance>());
            for (var j = i + 1; j < points.Count; j++)
            {
                dict[i].Add(new Distance(i, j, points[j].GetDistance(points[i])));
            }

            dict[i] = dict[i].OrderBy(x => x.dist).ToList();
        }

        var distances = dict.SelectMany(x => x.Value).OrderBy(x => x.dist).ToList();
        
        var workingPoints = Enumerable.Range(0, points.Count).ToList();
        
        var k = 0;
        while (k < 1000)
        {
            var minDistance = distances[k];
            Union(minDistance.from, minDistance.to);
            k++;
        }

        answer = (ulong)CalculateAnswer();

        return $"{answer}";

        int Find(int x)
        {
            if (workingPoints[x] == x)
            {
                return x;
            }
            return Find(workingPoints[x]);
        }

        void Union(int x, int y)
        {
            if (Find(x) == Find(y))
            {
                return;
            }
            workingPoints[Find(x)] = Find(y);
        }

        int CalculateAnswer()
        {
            var counts = Enumerable.Range(0, points.Count).Select(_ => 0).ToList();
            for (var i = 0; i < counts.Count; i++)
            {
                var root = Find(i);
                counts[root]++;
            }
        
            var topThree = counts.OrderByDescending(x => x).Take(3);
            return topThree.Aggregate((x, y) => x * y);
        }
    }

    public string? Url { get; }
    public string? Title { get; }
    public string? Input { get; set; }
    
    record Point3d(int X, int Y, int Z)
    {
        public double GetDistance(Point3d toPoint)
        {
            ulong dX =  (ulong)toPoint.X - (ulong)X;
            ulong dY =  (ulong)toPoint.Y - (ulong)Y;
            ulong dZ =  (ulong)toPoint.Z - (ulong)Z;
            
            ulong sum = dX * dX + dY * dY + dZ * dZ;
            
            return Math.Sqrt(sum);
        }

        public override string ToString()
        {
            return $"({X}; {Y}; {Z})";
        }
    }

    record Distance(int from, int to, double dist);
}
