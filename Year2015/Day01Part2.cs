using Common;

namespace AdventOfCode.Year2015;

public class Day01Part2 : IProblemWithInput
{
    public string GetAnswer()
    {
        if (string.IsNullOrEmpty(Input))
        {
            throw new ArgumentException(nameof(Input), $"{nameof(Input)} is null");
        }
        
        var floor = 0;
        var position = 0;
        do
        {
            var c = Input[position];
            position++;
            floor = Day01Common.ChangeFloor(floor, c);
        } while (floor >= 0 && position < Input.Length);

        if (floor < 0)
        {
            return position.ToString();
        }

        return "-1";
    }

    public string Url => "https://adventofcode.com/2015/day/1";
    public string Title => "Day 1: Not Quite Lisp. Part 2";
    public string? Input { get; set; }
}
