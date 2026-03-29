using System.Text;
using Common;

namespace AdventOfCode.Year2017;

public class Day02Part1 : IProblemWithInput
{
    public string GetAnswer()
    {
        var answer = 0;
        var lines = Input.Split('\n');
        foreach (var line in lines)
        {
            var parts = line.Split('\t', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            answer += (parts.Max() - parts.Min());
        }
        return answer.ToString();
    }

    public string? Url { get; }
    public string? Title { get; }
    public string? Input { get; set; }
}