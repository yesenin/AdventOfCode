using System.Text;
using Common;

namespace AdventOfCode.Year2017;

public class Day04Part1 : IProblemWithInput
{
    public string GetAnswer()
    {
        var answer = 0;
        var lines = Input.Split('\n', StringSplitOptions.TrimEntries);

        foreach (var line in lines)
        {
            var d = line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .GroupBy(c => c)
                .ToDictionary(g => g.Key, g => g.Count());
                
            var a = d.Any(d => d.Value > 1);
            if (!a)
            {
                answer++;
            }
        }
        
        return answer.ToString();
    }

    private int FindSum(string line)
    {
        var digits = line.Trim().Select(c => int.Parse(c.ToString())).ToList();
        var current = digits[0];
        var sum = 0;
        for (var i = 1; i <= digits.Count; i++)
        {
            var j = i == digits.Count ? 0 : i;
            if (current == digits[j])
            {
                sum += digits[j];
            }
            else
            {
                current = digits[j];
            }
        }

        return sum;
    }

    public string Url => "!TBD";
    public string Title => "!TBD";
    public string? Input { get; set; }
}
