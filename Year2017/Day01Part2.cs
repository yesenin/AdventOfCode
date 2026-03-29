using System.Text;
using Common;

namespace AdventOfCode.Year2017;

public class Day01Part2 : IProblemWithInput
{
    public string GetAnswer()
    {
        var answer = 0;
        var lines = Input.Split('\n');
        foreach (var line in lines)
        {
            var half = line.Length / 2;
            var sum = 0;
            for (var i = 0; i < half; i++)
            {
                if (line[i] == line[i + half])
                {
                    sum += int.Parse(line[i].ToString()) * 2;
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