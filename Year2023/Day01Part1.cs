using Common;

namespace AdventOfCode.Year2023;

public class Day01Part1 : IProblemWithInput
{
    public long GetAnswer()
    {
        var lines = Input.Split('\n');
        var answer = 0;

        foreach (var line in lines)
        {
            var i = 0;
            while (!"0123456789".Contains(line[i]))
            {
                i++;
            }
            var j = line.Length - 1;
            while (!"0123456789".Contains(line[j]))
            {
                j--;
            }
            answer += int.Parse($"{line[i]}{line[j]}");
        }
        
        return answer;
    }

    public string Url => "https://adventofcode.com/2023/day/1";
    public string Title => "Day 1: Report Repair. Part 1";
    public string? Input { get; set; }
}
