using Common;

namespace AdventOfCode.Year2020;

public class Day01Part2 : IProblemWithInput
{
    public string GetAnswer()
    {
        ulong answer = 0;
        var lines = Input.Split('\n', StringSplitOptions.TrimEntries);

        var numbers = new List<int>();
        var sub = new List<int>();
        
        foreach (var line in lines)
        {
            numbers.Add(int.Parse(line));
        }

        foreach (var number in numbers)
        {
            var d = 2020 - number;
            if (numbers.IndexOf(d) >= 0)
            {
                sub.Add(2020 - number);
            }
        }
        
        return $"{answer}";
    }

    public string Url { get; } = "https://adventofcode.com/2020/day/1";
    public string Title { get; } = "Report Repair. Part 2";
    public string? Input { get; set; }
} 
