using Common;

namespace AdventOfCode.Year2025;

public sealed class Day05Part1 : IProblemWithInput
{
    public string GetAnswer()
    {
        var lines = Input.Split('\n');
        var parseRanges = true;
        
        var ranges = new List<Range>();
        var ingridients = new List<ulong>();
        
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                parseRanges = false;
                continue;
            }

            if (parseRanges)
            {
                var parts = line.Trim().Split('-');
                ranges.Add(new Range(ulong.Parse(parts[0]), ulong.Parse(parts[1])));
            }
            else
            {
                ingridients.Add(ulong.Parse(line.Trim()));
            }
        }

        var freshCount = 0;
        
        ranges.Sort((a, b) => a.Start.CompareTo(b.Start));
        ingridients.Sort((a, b) => a.CompareTo(b));

        foreach (var ingridient in ingridients)
        {
            var firstGreaterEnd = ranges.FirstOrDefault(r => r.End >= ingridient);
            if (firstGreaterEnd != null && firstGreaterEnd.InRange(ingridient))
            {
                freshCount++;
            }
        }

        return freshCount.ToString();
    }

    public string? Url { get; }
    public string? Title { get; }
    public string? Input { get; set; }

    record Range(ulong Start, ulong End)
    {
        public bool InRange(ulong n) => n >= Start && n <= End;
    }
}
