using System.Text.RegularExpressions;
using Common;

namespace AdventOfCode.Year2023;

public class Day03Part1 : IProblemWithInput
{
    public long GetAnswer()
    {
        var lines = Input.Split(Environment.NewLine, StringSplitOptions.TrimEntries);
        var answer = 0L;

        var height = lines.Length;
        var width = lines[0].Length;

        var j = 0;
        
        var nonDidgits = new List<(int x, int y)>();
        var digits = new Dictionary<(int x, int y), int>();
        var numbers = new Dictionary<int, string>();
        
        var digitRegex = new Regex(@"\d{1}");

        var numberIsGoing = false;
        var numberId = 0;
        
        foreach (var line in lines)
        {
            for (var i = 0; i < line.Length; i++)
            {
                var symbol = line[i].ToString();
                if (!digitRegex.IsMatch(symbol))
                {
                    numberIsGoing = false;
                    if (symbol != ".")
                    {
                        nonDidgits.Add((i, j));
                    }
                }
                else
                {
                    if (!numberIsGoing)
                    {
                        numberIsGoing = true;
                        numberId++;
                        numbers.Add(numberId, "");
                    }
                    digits.Add((i, j), numberId);
                    numbers[numberId] += symbol;
                }
            }
            j++;
        }

        var connectedNumbers = new HashSet<int>();

        foreach (var nonDidgit in nonDidgits)
        {
            var surround = new List<(int x, int y)>
            {
                (nonDidgit.x, nonDidgit.y - 1),
                (nonDidgit.x + 1, nonDidgit.y - 1),
                (nonDidgit.x + 1, nonDidgit.y),
                (nonDidgit.x + 1, nonDidgit.y + 1),
                (nonDidgit.x, nonDidgit.y + 1),
                (nonDidgit.x - 1, nonDidgit.y + 1),
                (nonDidgit.x - 1, nonDidgit.y),
                (nonDidgit.x - 1, nonDidgit.y - 1),
            };
            var goodSurround = surround
                .Where(p => p.x >= 0 && p.y >= 0 && p.x < width && p.y < height)
                .ToList();
            var numberIds = digits.Where(p => goodSurround.Contains(p.Key))
                .Select(p => p.Value)
                .Distinct();
            connectedNumbers.UnionWith(numberIds);
        }

        answer = connectedNumbers.Select(x => int.Parse(numbers[x])).Sum();
        
        return answer;
    }

    public string Url => "!TBD";
    public string Title => "!TBD";
    public string? Input { get; set; }
}
