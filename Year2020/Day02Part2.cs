using System.Text.RegularExpressions;
using Common;

namespace AdventOfCode.Year2020;

public class Day02Part2 : IProblemWithInput
{
    public long GetAnswer()
    {
        long answer = 0;
        var lines = Input.Split('\n', StringSplitOptions.TrimEntries);

        var regex = new Regex(@"(\d+)-(\d+) (.{1}): (.+)");

        foreach (var line in lines)
        {
            var match = regex.Match(line);
            var a = int.Parse(match.Groups[1].Value) - 1;
            var b = int.Parse(match.Groups[2].Value) - 1;
            var letter = match.Groups[3].Value;
            var password = match.Groups[4].Value;
           
            if ((password[a].ToString() == letter && password[b].ToString() != letter) 
                || (password[b].ToString() == letter && password[a].ToString() != letter))
            {
                answer++;
            }
        }
        
        return answer;
    }

    public string Url { get; } = "https://adventofcode.com/2020/day/2";
    public string Title { get; } = "Password Philosophy. Part 2";
    public string? Input { get; set; }
} 
