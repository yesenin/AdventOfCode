using Common;

namespace AdventOfCode.Year2015;

public class Day03Part1 : IProblemWithInput
{
    public string GetAnswer()
    {
        var lines = Input.Split('\n');

        var answer = new List<int>();
        foreach (var line in lines)
        {
            var currentPoint = new Point(0, 0);
            var uniquePoints = new HashSet<Point>()
            {
                currentPoint
            };
            foreach (var t in line)
            {
                var x = currentPoint.X;
                var y = currentPoint.Y;
                switch (t)
                {
                    case '^':
                        y++;
                        break;
                    case 'v':
                        y--;
                        break;
                    case '>':
                        x++;
                        break;
                    case '<':
                        x--;
                        break;
                }
                uniquePoints.Add(new Point(x, y));
                currentPoint = new Point(x, y);
            }
            answer.Add(uniquePoints.Count);
        }
        
        return string.Join("\n", answer);
    }
    
    private record Point(int X, int Y);

    public string Url => "https://adventofcode.com/2015/day/3";
    public string Title => "Perfectly Spherical Houses in a Vacuum. Part 1";
    public string? Input { get; set; }
}
