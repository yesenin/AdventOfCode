using Common;

namespace AdventOfCode.Year2025;

public sealed class Day05Part1 : IProblemWithInput
{
    public long GetAnswer()
    {
        var lines = Input.Split('\n');
        var parseRanges = true;
        
        var ranges = new List<Range>();
        var ingridients = new List<long>();
        
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
                ranges.Add(new Range(long.Parse(parts[0]), long.Parse(parts[1])));
            }
            else
            {
                ingridients.Add(long.Parse(line.Trim()));
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

        return freshCount;
    }

    public string Url => "!TBD";
    public string Title => "!TBD";
    public string? Input { get; set; }

    record Range(long Start, long End)
    {
        public bool InRange(long n) => n >= Start && n <= End;
    }
}
