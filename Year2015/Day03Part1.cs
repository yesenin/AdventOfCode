using Common;

namespace AdventOfCode.Year2015;

public class Day03Part1 : BaseProblemWithInput
{
    protected override long GetAnswerInner()
    {
        var lines = Input!.Split('\n');

        var answer = 0;
        foreach (var line in lines)
        {
            var currentPoint = new Point(0, 0);
            var uniquePoints = new HashSet<Point>()
            {
                currentPoint
            };
            foreach (var move in line)
            {
                currentPoint = Day03Common.ShiftPoint(currentPoint, move);
                uniquePoints.Add(currentPoint);
            }
            answer += uniquePoints.Count;
        }
        
        return answer;
    }

    

    public override string Url => "https://adventofcode.com/2015/day/3";
    public override string Title => "Perfectly Spherical Houses in a Vacuum. Part 1";
}
