using Common;

namespace AdventOfCode.Year2021;

public class Day01Part2 : IProblemWithInput
{
    public string GetAnswer()
    {
        var lines = Input.Split('\n');
        var numbers = lines.Select(int.Parse).ToArray();
        var increases = 0;
        var trippleSums = new List<int>();

        var i = 0;
        while (i < numbers.Length - 2)
        {
            var localSum = 0;
            for (var j = i; j < i + 3; j++)
            {
                localSum += numbers[j];
            }
            trippleSums.Add(localSum);
            i += 1;
        }

        var prevNumber = trippleSums[0];
        foreach (var number in trippleSums.Skip(1))
        {
            if (prevNumber < number)
            {
                increases++;
            }
            prevNumber = number;
        }
        
        return $"{increases}";
    }

    public string Url => "!TBD";
    public string Title => "!TBD";
    public string? Input { get; set; }
}
