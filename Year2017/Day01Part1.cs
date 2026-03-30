using System.Text;
using Common;

namespace AdventOfCode.Year2017;

public class Day01Part1 : IProblemWithInput
{
    public string GetAnswer()
    {
        var answer = new StringBuilder();
        var lines = Input.Split('\n');
        foreach (var line in lines)
        {
            answer.Append(FindSum(line));
            answer.AppendLine();
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
