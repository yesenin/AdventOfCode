using System.Text;
using Common;

namespace AdventOfCode.Year2016;

public class Day03Part2 : IProblemWithInput
{
    public string GetAnswer()
    {
        var answer = 0L;
        
        var lines = Input.Split(Environment.NewLine, StringSplitOptions.TrimEntries);

        var parsedSides = new List<int[]>()
        {
            new int[3],
            new int[3],
            new int[3],
        };

        var i = 0;
        
        foreach (var line in lines)
        {
            var sides =  line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            for (var c = 0; c < sides.Length; c++)
            {
                parsedSides[c][i]  = sides[c];
            }

            i++;
            if (i == 3)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (IsTriangle(parsedSides[j]))
                    {
                        answer++;
                    }
                }
                parsedSides = new List<int[]>()
                {
                    new int[3],
                    new int[3],
                    new int[3],
                };
                i = 0;
            }
        }
        
        return answer.ToString();
    }

    private bool IsTriangle(int[] sides)
    {
        for (var i = 0; i < sides.Length; i++)
        {
            var sum = 0;
            for (var j = 0; j < sides.Length; j++)
            {
                if (i != j)
                {
                    sum += sides[j];
                }
            }

            if (sum <= sides[i])
            {
                return false;
            }
        }
        return true;
    }
    
    public string Url => "!TBD";
    public string Title => "!TBD";
    public string? Input { get; set; }
}
