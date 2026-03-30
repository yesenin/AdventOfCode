using System.Text;
using Common;

namespace AdventOfCode.Year2025;

public sealed class Day09Part2 : IProblemWithInput
{
    public string GetAnswer()
    {
        var lines = Input.Split('\n');
        int answer = 0;

        var points = new List<(int x, int y)>();

        foreach (var line in lines)
        {
            var parts = line.Split(',').Select(int.Parse).ToArray();
            points.Add((parts[0], parts[1]));
        }
        
        var hallWidth = points.Max(x => x.x) + 3;
        var hallHeight = points.Max(x => x.y) + 2;
        
        var greenPoints = new List<(int x, int y)>();

        int from = -1;
        int to = -1;
        // draw horizontal borders
        for (int y = 0; y < hallHeight; y++)
        {
            var rowPoints = points.Where(p => p.y == y);
            if (rowPoints.Any())
            {
                from = rowPoints.Min(p => p.x);
                to = rowPoints.Max(p => p.x);
                for (var x = from; x <= to; x++)
                {
                    greenPoints.Add((x, y));
                }
            }
        }
        
        // draw vertical borders
        for (int x = 0; x < hallWidth; x++)
        {
            var colPoints = points.Where(p => p.x == x);
            if (colPoints.Any())
            {
                from = colPoints.Min(p => p.y);
                to = colPoints.Max(p => p.y);
                for (var y = from; y <= to; y++)
                {
                    greenPoints.Add((x, y));
                }
            }
        }
        
        // fill

        for (var y = 0; y < hallHeight; y++)
        {
            var rowGreenPoints = greenPoints.Where(x => x.y == y).ToList();
            if (rowGreenPoints.Any())
            {
                var minX = rowGreenPoints.Min(p => p.x);
                var maxX = rowGreenPoints.Max(p => p.x);
                for (var x = minX; x <= maxX; x++)
                {
                    if (!greenPoints.Any(p => p.y == y && p.x == x))
                    {
                        greenPoints.Add((x, y));
                    }
                }
            }
        }
        
        var squares = new Dictionary<int, List<int>>();

        for (var i = 0; i < points.Count; i++)
        {
            squares.Add(i, new List<int>());
            for (var e = 0; e < i + 1; e++)
            {
                squares[i].Add(0);
            }
            for (var j = i + 1; j < points.Count; j++)
            {
                var allCorners = new List<(int x, int y)>
                {
                    points[i],
                    points[j]
                };
                allCorners.Add((points[i].x, points[j].y));
                allCorners.Add((points[j].x, points[i].y));
                if (allCorners.All(c => greenPoints.Any(p => p.x == c.x && p.y == c.y)))
                {
                    squares[i].Add(GetSquare(points[i], points[j]));
                }
            }
        }
        
        int maxValue = 0;

        foreach (var squareKey in squares.Keys)
        {
            foreach (var square in squares[squareKey])
            {
                if (square > maxValue)
                {
                    maxValue = square;
                }
            }
        }
        
        answer = maxValue;
        
        return $"{answer}";
        
        int GetSquare((int x, int y) p1, (int x, int y) p2)
        {
            int width = Math.Max(p1.x, p2.x) -  Math.Min(p1.x, p2.x) + 1;
            int height = Math.Max(p1.y, p2.y) -  Math.Min(p1.y, p2.y) + 1;
            return width * height;
        }
    }

    public string Url => "!TBD";
    public string Title => "!TBD";
    public string? Input { get; set; }
}
