using System.Text;
using Common;

namespace AdventOfCode.Year2016;

public class Day04Part1 : IProblemWithInput
{
    public string GetAnswer()
    {
        var answer = 0L;
        
        var lines = Input.Split(Environment.NewLine, StringSplitOptions.TrimEntries);

        foreach (var line in lines)
        {
            var parts = line.Split("[");
            parts[1] = parts[1].Substring(0, parts[1].Length - 1);
            var lastIndexes =  new Dictionary<string, (int lastIndex, int count)>();
            for (var i = 0; i < parts[1].Length; i++)
            {
                var lastIndex = -1;
                var count = 0;
                var letter = parts[1][i];
                for (var j = parts[0].Length - 1; j >= 0; j--)
                {
                    if (parts[0][j] == letter)
                    {
                        count++;
                        lastIndex = j;
                    }
                }
                lastIndexes.Add(letter.ToString(), (lastIndex, count));
            }

            var sortedCounts = lastIndexes
                .Where(x => x.Value.count > 0)
                .OrderByDescending(y => y.Value.count)
                .ThenBy(x => x.Key)
                .ThenBy(y => y.Value.lastIndex)
                .Select(y => y.Key);
            
            var res = string.Join("", sortedCounts);
            if (res == parts[1])
            {
                Console.WriteLine(line);
                foreach (var k in lastIndexes.Keys)
                {
                    Console.Write($"{k}: {lastIndexes[k].count}, {lastIndexes[k].lastIndex}");
                    Console.WriteLine();
                }
                var lastPart = long.Parse(parts[0].Split('-').Last());
                answer += lastPart;
            }
        }
        
        return answer.ToString();
    }

    public string Url => "!TBD";
    public string Title => "!TBD";
    public string? Input { get; set; }
}
