using Common;

namespace AdventOfCode.Year2018;

public class Day01Part2 : IProblemWithInput
{
    public long GetAnswer()
    {
        long answer = 0;
        var lines = Input.Split('\n', StringSplitOptions.TrimEntries);

        var numbers = lines.Select(int.Parse).ToList();

        var dict = new Dictionary<int, int>();

        var i = 0;
        var sum = 0;
        while (true)
        {
            sum += numbers[i];
            if (!dict.TryAdd(sum, 1))
            {
                break;
            }

            i++;
            if (i == numbers.Count)
            {
                i = 0;
            }
        }
        
        answer = sum;
        
        return answer;
    }

    public string Url => "!TBD";
    public string Title => "!TBD";
    public string? Input { get; set; }
} 
