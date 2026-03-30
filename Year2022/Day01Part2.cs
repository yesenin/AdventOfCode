using Common;

namespace AdventOfCode.Year2022;

public class Day01Part2 : IProblemWithInput
{
    public string GetAnswer()
    {
        long answer = 0;
        
        var lines = Input.Split(Environment.NewLine, StringSplitOptions.TrimEntries);

        var callories = new List<long>();

        var current = 0L;
        foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line))
            {
                callories.Add(current);
                current = 0L;
                continue;
            }
            current += long.Parse(line);
        }

        answer = callories.OrderDescending().Take(3).Sum();
        
        return $"{answer}";
    }

    public string Url => "!TBD";
    public string Title => "!TBD";
    public string? Input { get; set; }
}
