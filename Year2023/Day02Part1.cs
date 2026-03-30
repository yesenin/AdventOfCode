using System.Text.RegularExpressions;
using Common;

namespace AdventOfCode.Year2023;

public class Day02Part1 : IProblemWithInput
{
    public string GetAnswer()
    {
        var lines = Input.Split(Environment.NewLine, StringSplitOptions.TrimEntries);
        var answer = 0L;

        var firstRegex = new Regex(@"Game\s(\d+):(.+)");

        var limits = new Dictionary<string, int>
        {
            { "red", 12 },
            { "green", 13 },
            { "blue", 14 },
        };

        foreach (var line in lines)
        {
            var match = firstRegex.Match(line);
            
            var gameId = int.Parse(match.Groups[1].Value);
            
            var cubes = match.Groups[2].Value;
            var secondRegex = new Regex(@"(\d+\s+[^;]+)");
            var secondMatches = secondRegex.Matches(cubes.Trim());
            
            foreach (var cubeMatch in  secondMatches.Cast<Match>())
            {
                var set = cubeMatch.Groups[1].Value;
                
                var thirdRegex = new Regex(@"(\d+)\s+([^,]+)");
                var thirdMatches = thirdRegex.Matches(set.Trim());

                foreach (var thirdMatch in thirdMatches.Cast<Match>())
                {
                    var count = int.Parse(thirdMatch.Groups[1].Value);
                    var color = thirdMatch.Groups[2].Value;
                    if (limits[color] < count)
                    {
                        gameId = 0;
                    }
                }
            }
            
            answer += gameId;
        }
        
        return answer.ToString();
    }

    public string Url => "!TBD";
    public string Title => "!TBD";
    public string? Input { get; set; }
}
