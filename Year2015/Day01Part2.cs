using Common;

namespace AdventOfCode.Year2015;

public sealed class Day01Part2 : BaseProblemWithInput
{
    protected override long GetAnswerInner()
    {
        var floor = 0;
        var position = 0;
        do
        {
            var c = Input![position];
            position++;
            floor = Day01Common.ChangeFloor(floor, c);
        } while (floor >= 0 && position < Input.Length);

        return floor < 0 ? position : -1;
    }

    public override string Url => "https://adventofcode.com/2015/day/1";
    public override string Title => "Day 1: Not Quite Lisp. Part 2";
}
