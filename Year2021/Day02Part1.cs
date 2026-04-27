using System.Text.RegularExpressions;
using Common;

namespace AdventOfCode.Year2021;

public class Day02Part1 : IProblemWithInput
{
    public long GetAnswer()
    {
        var answer = 0L;
        var lines = Input.Split(Environment.NewLine, StringSplitOptions.TrimEntries);

        var regex = new Regex(@"(.+) (\d+)");

        var x = 0;
        var y = 0;
        
        foreach (var line in lines)
        {
            var match = regex.Match(line);
            if (match.Success)
            {
                var value = int.Parse(match.Groups[2].Value);
                switch (match.Groups[1].Value)
                {
                    case "forward":
                        x += value;
                        break;
                    case "down":
                        y += value;
                        break;
                    case "up":
                        y -= value;
                        break;
                }
            }
        }

        answer = x * y;
        
        return answer;
    }

    public string Url => "!TBD";
    public string Title => "!TBD";
    public string? Input { get; set; }
}
