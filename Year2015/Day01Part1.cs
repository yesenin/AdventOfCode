using Common;

namespace AdventOfCode.Year2015;

public sealed class Day01Part1 : BaseProblemWithInput
{
    protected override string GetAnswerInner()
    {
        int floor = Input!.Aggregate(0, Day01Common.ChangeFloor);
        return floor.ToString();
    }

    public override string Url => "https://adventofcode.com/2015/day/1";
    public override string Title => "Day 1: Not Quite Lisp. Part 1";
}
