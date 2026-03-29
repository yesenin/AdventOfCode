using Common;

namespace AdventOfCode.Year2025;

public sealed class Day09Part1 : IProblemWithInput
{
    public string GetAnswer()
    {
        var lines = Input.Split('\n');
        ulong answer = 0;

        var points = new List<(ulong x, ulong y)>();

        foreach (var line in lines)
        {
            var parts = line.Split(',').Select(ulong.Parse).ToArray();
            points.Add((parts[0], parts[1]));
        }
        
        var squares = new Dictionary<int, List<ulong>>();

        for (var i = 0; i < points.Count; i++)
        {
            squares.Add(i, new List<ulong>());
            for (var e = 0; e < i + 1; e++)
            {
                squares[i].Add(0);
            }
            for (var j = i + 1; j < points.Count; j++)
            {
                squares[i].Add(GetSquare(points[i], points[j]));
            }
        }
        
        ulong maxValue = 0;

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

        return $"{answer}";

        ulong GetSquare((ulong x, ulong y) p1, (ulong x, ulong y) p2)
        {
            ulong width = Math.Max(p1.x, p2.x) -  Math.Min(p1.x, p2.x) + 1;
            ulong height = Math.Max(p1.y, p2.y) -  Math.Min(p1.y, p2.y) + 1;
            return width * height;
        }
    }

    public string? Url { get; }
    public string? Title { get; }
    public string? Input { get; set; }
}
