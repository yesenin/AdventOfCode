using Common;

namespace AdventOfCode.Year2015;

public class Day03Part2 : BaseProblemWithInput
{
    protected override string GetAnswerInner()
    {
        var lines = Input!.Split('\n');

        var answer = 0;
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
                var point = Day03Common.ShiftPoint(new Point(x, y), line[i]);

                uniquePoints.Add(point);
                if (i % 2 > 0)
                {
                    currentSantaPoint = point;
                }
                else
                {
                    currentBotPoint = point;
                }
            }
            answer += uniquePoints.Count;
        }
        
        return answer.ToString();
    }
    
    public override string Url => "https://adventofcode.com/2015/day/3";
    public override string Title => "Day 3 Part 2: Santa and Bot";
}
