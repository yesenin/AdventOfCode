using Common;
using Serilog;

namespace AdventOfCode.Year2015;

public class Day02Part2 : IProblemWithInput
{
    public long GetAnswer()
    {
        if (string.IsNullOrEmpty(Input))
        {
            throw new ArgumentException(nameof(Input), $"{nameof(Input)} is null");
        }
        var total = 0;
        var lines = Input.Split('\n', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        
        foreach (var line in lines)
        {
            Logger?.Debug("Parsing input: {Input}.", line);

            (int l, int w, int h) = ParseInput(line);
            var ribbon = GetRibbonLength(l, w, h);
            var bow = GetBowLength(l, w, h);
            total += ribbon + bow;
        }

        return total;
    }

    public string Url => "https://adventofcode.com/2020/day02/part2/";
    public string Title => "Day 2 Part 2";
    public string? Input { get; set; }
    public ILogger? Logger { get; set; }

    private static (int l, int w, int h) ParseInput(string input)
    {
        var parts = input.Split('x');
        return (int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]));
    }

    private int GetRibbonLength(int l, int w, int h)
    {
        var perimeters = new [] { 2 * (l + w), 2 * (w + h), 2 * (h + l)};
        return perimeters.Min();
    }

    private int GetBowLength(int l, int w, int h)
    {
        return l * w * h;
    }
}
