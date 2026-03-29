using Common;

namespace AdventOfCode.Year2020;

public class Day03Part1 : IProblemWithInput
{
    public string GetAnswer()
    {
        ulong answer = 0;
        var lines = Input.Split('\n', StringSplitOptions.TrimEntries);

        var pattern = new List<string>();
        foreach (var line in lines)
        {
            pattern.Add(line);
        }

        var i = 0;
        var j = 0;

        while (j < pattern.Count - 1)
        {
            j += 1;
            i += 3;
            if (i >= pattern[j].Length)
            {
                i -= pattern[j].Length;
            }

            if (pattern[j][i] == '#')
            {
                answer++;
            }
        }
        
        return $"{answer}";
    }

    public string Url { get; } = "https://adventofcode.com/2020/day/3";
    public string Title { get; } = "Toboggan Trajectory";
    public string? Input { get; set; }
} 
