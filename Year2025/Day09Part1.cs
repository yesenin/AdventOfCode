using Common;

namespace AdventOfCode.Year2025;

public sealed class Day09Part1 : IProblemWithInput
{
    public long GetAnswer()
    {
        var lines = Input.Split('\n');
        long answer = 0;

        var points = new List<(long x, long y)>();

        foreach (var line in lines)
        {
            var parts = line.Split(',').Select(long.Parse).ToArray();
            points.Add((parts[0], parts[1]));
        }
        
        var squares = new Dictionary<int, List<long>>();

        for (var i = 0; i < points.Count; i++)
        {
            squares.Add(i, new List<long>());
            for (var e = 0; e < i + 1; e++)
            {
                squares[i].Add(0);
            }
            for (var j = i + 1; j < points.Count; j++)
            {
                squares[i].Add(GetSquare(points[i], points[j]));
            }
        }
        
        long maxValue = 0;

        foreach (var distancesKey in squares.Keys)
        {
            foreach (var distance in squares[distancesKey])
            {
                if (distance > maxValue)
                {
                    maxValue = distance;
                }
            }
        }
        
        answer = maxValue;

        return answer;

        long GetSquare((long x, long y) p1, (long x, long y) p2)
        {
            long width = Math.Max(p1.x, p2.x) -  Math.Min(p1.x, p2.x) + 1;
            long height = Math.Max(p1.y, p2.y) -  Math.Min(p1.y, p2.y) + 1;
            return width * height;
        }
    }

    public string Url => "!TBD";
    public string Title => "!TBD";
    public string? Input { get; set; }
}
