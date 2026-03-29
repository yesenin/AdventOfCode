using Common;

namespace AdventOfCode.Year2015;

public class Day03Part2 : IProblemWithInput
{
    public string GetAnswer()
    {
        var lines = Input.Split('\n');

        var answer = new List<int>();
        foreach (var line in lines)
        {
            var currentSantaPoint = new Point(0, 0);
            var currentBotPoint = new Point(0, 0);
            var uniquePoints = new HashSet<Point>()
            {
                currentBotPoint,
                currentSantaPoint
            };
            for (var i = 0; i < line.Length; i++)
            {
                var x = i % 2 > 0 ? currentSantaPoint.X :  currentBotPoint.X;
                var y = i % 2 > 0 ? currentSantaPoint.Y : currentBotPoint.Y;
                switch (line[i])
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
                if (i % 2 > 0)
                {
                    currentSantaPoint = new Point(x, y);
                }
                else
                {
                    currentBotPoint = new Point(x, y);
                }
            }
            answer.Add( uniquePoints.Count );
        }
        
        return string.Join("\n", answer);
    }
    
    record Point(int X, int Y);

    public string? Url { get; }
    public string? Title { get; }
    public string? Input { get; set; }
}