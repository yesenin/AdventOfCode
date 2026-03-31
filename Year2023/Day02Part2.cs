using System.Text.RegularExpressions;
using Common;

namespace AdventOfCode.Year2023;

public class Day02Part2 : IProblemWithInput
{
    public long GetAnswer()
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
            
            var state = new Dictionary<string, int>
            {
                { "red", 0 },
                { "green", 0 },
                { "blue", 0 },
            };
            
            foreach (var cubeMatch in  secondMatches.Cast<Match>())
            {
                
                var set = cubeMatch.Groups[1].Value;
                
                var thirdRegex = new Regex(@"(\d+)\s+([^,]+)");
                var thirdMatches = thirdRegex.Matches(set.Trim());

                foreach (var thirdMatch in thirdMatches.Cast<Match>())
                {
                    var count = int.Parse(thirdMatch.Groups[1].Value);
                    var color = thirdMatch.Groups[2].Value;
                    if (state[color] < count)
                    {
                        state[color] = count;
                    }
                }
            }
            
            answer += state.Select(x => x.Value).Aggregate((a, b) => a * b);
        }
        
        return answer;
    }

    public string Url => "https://adventofcode.com/2023/day/2";
    public string Title => "Calorie Counting";
    public string? Input { get; set; }
}
