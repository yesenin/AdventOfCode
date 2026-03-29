using Common;
using Serilog;

namespace AdventOfCode.Year2015;

public sealed class Day02Part1 : BaseProblemWithInput, IProblemWithLogger
{
    protected override string GetAnswerInner()
    {
        Logger?.Information("Solving {Title}.", Title);

        var total = 0;
        var lines = Input!.Split('\n', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        foreach (var line in lines)
        {
            Logger?.Debug("Parsing input: {Input}.", line);
            (int l, int w, int h) = ParseInput(line);
            Logger?.Debug("Parsed box dimensions: {Length}x{Width}x{Height}.", l, w, h);

            var area = SurfaceHelper.GetSurfaceArea(l, w, h);
            var min = SurfaceHelper.GetMinimalSideSquare(l, w, h);
            Logger?.Debug("Computed surface area {SurfaceArea} and slack {Slack}.", area, min);
            total += area + min;
        }

        return total.ToString();
    }

    public override string Url => "https://adventofcode.com/2015/day/2";
    public override string Title => "Day 2: I Was Told There Would Be No Math. Part 1";
    public ILogger? Logger { get; set; }

    private static (int l, int w, int h) ParseInput(string input)
    {
        var parts = input.Split('x');
        return (int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]));
    }
}
