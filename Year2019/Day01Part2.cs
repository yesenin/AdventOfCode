using Common;

namespace AdventOfCode.Year2019;

public class Day01Part2 : IProblemWithInput
{
    public long GetAnswer()
    {
        long answer = 0;
        var lines = Input!.Split('\n', StringSplitOptions.TrimEntries);

        var numbers = new List<int>();
        
        foreach (var line in lines)
        {
            numbers.Add(int.Parse(line));
        }

        var temp = new List<int>();
        foreach (var number in numbers)
        {
            var a = new List<int>();
            var d = number / 3 - 2;
            while (d > 0)
            {
                a.Add(d);
                d = d / 3 - 2;
            }
            temp.Add(a.Sum());
        }
        
        answer = temp.Sum();

        return answer;
    }

    public string Url => "!TBD";
    public string Title => "!TBD";
    public string? Input { get; set; }
} 
