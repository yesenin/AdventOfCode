using System.Text;
using Common;

namespace AdventOfCode.Year2016;

public class Day03Part1 : IProblemWithInput
{
    public string GetAnswer()
    {
        var answer = 0L;
        
        var lines = Input.Split(Environment.NewLine, StringSplitOptions.TrimEntries);

        foreach (var line in lines)
        {
            var sides =  line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            var isTriangle = true;
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
                    isTriangle = false;
                    break;
                }
            }

            if (isTriangle)
            {
                answer++;
            }
        }
        
        return answer.ToString();
    }

    public string Url => "!TBD";
    public string Title => "!TBD";
    public string? Input { get; set; }
}
