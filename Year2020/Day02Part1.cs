using System.Text.RegularExpressions;
using Common;

namespace AdventOfCode.Year2020;

public class Day02Part1 : IProblemWithInput
{
    public long GetAnswer()
    {
        long answer = 0;
        var lines = Input.Split('\n', StringSplitOptions.TrimEntries);

        var regex = new Regex(@"(\d+)-(\d+) (.{1}): (.+)");

        foreach (var line in lines)
        {
            var match = regex.Match(line);
            var from = int.Parse(match.Groups[1].Value);
            var to = int.Parse(match.Groups[2].Value);
            var letter = match.Groups[3].Value;
            var password = match.Groups[4].Value;
            
            var count = password.Count(x => x.ToString() == letter);

            if (count >= from && count <= to)
            {
                answer++;
            }
        }
        
        return answer;
    }

    public string Url => "!TBD";
    public string Title => "!TBD";
    public string? Input { get; set; }
} 
