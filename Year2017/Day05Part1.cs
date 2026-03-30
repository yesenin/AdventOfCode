using System.Text;
using Common;

namespace AdventOfCode.Year2017;

public class Day05Part1 : IProblemWithInput
{
    public string GetAnswer()
    {
        var answer = 0;
        var lines = Input.Split('\n');

        var maze = new List<int>();
        
        foreach (var line in lines)
        {
            maze.Add(int.Parse(line));
        }

        var i = 0;
        var step = 0;
        while (i >= 0 &&i < maze.Count)
        {
            step++;
            
            var next_i =  i + maze[i];
            maze[i]++;

            i = next_i;
        }

        answer = step;
        
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
