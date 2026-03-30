using Common;

namespace AdventOfCode.Year2018;

public class Day01Part1 : IProblemWithInput
{
    public string GetAnswer()
    {
        long answer = 0;
        var lines = Input.Split('\n', StringSplitOptions.TrimEntries);

        var numbers = lines.Select(int.Parse).ToList();

        answer = numbers.Sum();
        
        return $"{answer}";
    }

    public string Url => "!TBD";
    public string Title => "!TBD";
    public string? Input { get; set; }
} 
