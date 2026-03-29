using Common;

namespace AdventOfCode.Year2020;

public class Day03Part2 : IProblemWithInput
{
    public string GetAnswer()
    {
        var lines = Input.Split('\n', StringSplitOptions.TrimEntries);

        var pattern = new List<string>();
        foreach (var line in lines)
        {
            pattern.Add(line);
        }

        long answer = 1L;
        answer *= Slope(1, 1);
        answer *= Slope(3, 1);
        answer *= Slope(5, 1);
        answer *= Slope(7, 1);
        answer *= Slope(1, 2);
        
        return $"{answer}";

        long Slope(int di, int dj)
        {
            var result = 0L;
            
            var i = 0;
            var j = 0;

            while (j < pattern.Count - 1)
            {
                j += dj;
                i += di;
                if (i >= pattern[j].Length)
                {
                    i -= pattern[j].Length;
                }

                if (pattern[j][i] == '#')
                {
                    result++;
                }
            }

            return result;
        }
    }

    public string Url => "https://adventofcode.com/2020/day/3";
    public string Title => "Toboggan Trajectory. Part 2";
    public string? Input { get; set; }
} 
