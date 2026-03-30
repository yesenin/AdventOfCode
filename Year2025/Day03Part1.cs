using Common;

namespace AdventOfCode.Year2025;

public class Day03Part1 : IProblemWithInput
{
    public string GetAnswer()
    {
        var answer = 0;
        var lines = Input.Split('\n');
        foreach (var line in lines)
        {
            answer += FindMax(line);
        }
        return answer.ToString();
    }

    private int FindMax(string line)
    {
        var result = 0;
        for (var i = 0; i < line.Length; i++)
        {
            for (var j = i + 1; j < line.Length; j++)
            {
                var candidate = int.Parse($"{line[i]}{line[j]}");
                if (candidate > result)
                {
                    result = candidate;
                }
            }
        }

        return result;
    }

    public string Url => "!TBD";
    public string Title => "!TBD";
    public string? Input { get; set; }
}
