using Common;

namespace AdventOfCode.Year2021;

public class Day01Part1 : IProblemWithInput
{
    public string GetAnswer()
    {
        var lines = Input.Split('\n');
        var numbers = lines.Select(int.Parse).ToArray();
        var increases = 0;

        var prevNumber = numbers[0];
        foreach (var number in numbers.Skip(1))
        {
            if (prevNumber < number)
            {
                increases++;
            }
            prevNumber = number;
        }
        
        return $"{increases}";
    }

    public string? Url { get; }
    public string? Title { get; }
    public string? Input { get; set; }
}