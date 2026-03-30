using System.Text.RegularExpressions;
using Common;

namespace AdventOfCode.Year2015;

public class Day06Part2 : BaseProblemWithInput
{
    private readonly int[,] _grid = new int[1000, 1000];
    
    protected override string GetAnswerInner()
    {
        var lines = Input!.Split('\n').Select(x => x.Trim()).ToArray();

        var answer = 0;

        foreach (var line in lines)
        {
            var command = ParseLine(line);
            if (command == null)
            {
                continue;
            }
            ExecuteCommand(command.Value.from, command.Value.to, command.Value.action);
        }
        
        answer = _grid.Cast<int>().Sum();
        
        return answer.ToString();
    }

    public (Point from, Point to, string action)? ParseLine(string parse)
    {
        var regex = new Regex(@"(.+)\s(\d+,\d+)\sthrough\s(\d+,\d+)");
        var match = regex.Match(parse);
        if (match.Success)
        {
            var from = match.Groups[2].Value.Split(',').Select(int.Parse).ToArray();
            var to = match.Groups[3].Value.Split(',').Select(int.Parse).ToArray();
            var action = match.Groups[1].Value;
            return (new Point(from[0], from[1]), new Point(to[0], to[1]), action);
        }

        return null;
    }

    public void ExecuteCommand(Point from, Point to, string action)
    {
        for (var y = from.Y; y <= to.Y; y++)
        {
            for (var x = from.X; x <= to.X; x++)
            {
                _grid[x, y] = action switch
                {
                    "turn on" => _grid[x, y] + 1,
                    "turn off" => Math.Max(_grid[x, y] - 1, 0),
                    "toggle" => _grid[x, y] + 2,
                    _ => _grid[x, y]
                };
            }
        }
    }

    public override string Url => "https://adventofcode.com/2015/day/6";
    public override string Title => "Probably a Fire Hazard";
}
