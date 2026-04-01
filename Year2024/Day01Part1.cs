using Common;

namespace AdventOfCode.Year2024;

public class Day01Part1 : BaseProblemWithInput
{
    protected override long GetAnswerInner()
    {
        var leftCol = new List<int>();
        var rightCol = new List<int>();

        var lines = Input!.Split("\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        foreach (string line in lines)
        {
            var parts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            leftCol.Add(int.Parse(parts[0]));
            rightCol.Add(int.Parse(parts[1]));
        }
        
        leftCol.Sort();
        rightCol.Sort();
        
        var temp = leftCol.Zip(rightCol, (a, b) => Math.Abs(a - b));

        return temp.Sum();
    }
    
    public override string Title => "Historian Hysteria";
    public override string Url => "https://adventofcode.com/2024/day/1";
}

