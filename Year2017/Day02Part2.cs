using System.Text;
using Common;

namespace AdventOfCode.Year2017;

public class Day02Part2 : IProblemWithInput
{
    public string GetAnswer()
    {
        var answer = 0;
        var lines = Input.Split('\n');
        foreach (var line in lines)
        {
            var parts = line.Split('\t', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse).OrderByDescending(x => x).ToList();
            var sum = 0;
            for (var i = 0; i < parts.Count() - 1; i++)
            {
                for (var j = i + 1; j < parts.Count(); j++)
                {
                    if (parts[i] % parts[j] == 0)
                    {
                        sum += parts[i] /  parts[j];
                    }
                }
            }
            answer += sum;
        }
        return answer.ToString();
    }

    public string? Url { get; }
    public string? Title { get; }
    public string? Input { get; set; }
}