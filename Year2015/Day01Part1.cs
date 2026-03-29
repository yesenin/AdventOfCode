using Common;

namespace AdventOfCode.Year2015;

public class Day01Part1 : IProblemWithInput
{
    public string GetAnswer()
    {
        if (string.IsNullOrEmpty(Input))
        {
            throw new ArgumentException(nameof(Input), $"{nameof(Input)} is null");
        }
        int floor = Input.Aggregate(0, Day01Common.ChangeFloor);
        return floor.ToString();
    }

    

    public string Url => "https://adventofcode.com/2015/day/1";
    public string Title => "Day 1: Not Quite Lisp. Part 1";
    
    public string? Input { get; set; }
}
