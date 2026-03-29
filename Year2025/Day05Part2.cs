using Common;

namespace AdventOfCode.Year2025;

public sealed class Day05Part2 : IProblemWithInput
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
                ulong start = ulong.Parse(parts[0]);
                ulong end = ulong.Parse(parts[1]);
                if (start >= end)
                {
                    (start, end) = (end, start);
                }
                ranges.Add(new Range
                    {
                        Start = start,
                        End = end
                    }
                );
            }
            else
            {
                ingridients.Add(ulong.Parse(line.Trim()));
            }
        }

        var allFresh = new HashSet<ulong>();
        
        ranges.Sort((a, b) => a.Start.CompareTo(b.Start));
        //ingridients.Sort((a, b) => a.CompareTo(b));

        var normalizedRanges = new List<Range>()
        {
            ranges[0],
        };
        
        for (var i = 1; i < ranges.Count; i++)
        {
            var range_i =  ranges[i];
            // tree options
            // A
            // X---Y
            //   M---N
            // X-----N
            // B
            //    X---Y
            //  M---N
            //  M-----Y
            // C
            //   X--Y
            // M------N
            // M------N
            // D
            // X------Y
            //   M--N
            // X------Y

            var option = normalizedRanges
                .Select(r => (r, GetOption(r,  range_i)))
                .Where(o => o.Item2 != "0");

            if (option.Count() == 0)
            {
                normalizedRanges.Add(range_i);
                continue;
            }

            if (option.Count() > 1)
            {
                throw new Exception("Too many options");
            }
            
            var first = option.First();
            
            switch (first.Item2)
            {
                case "A":
                    first.r.End = range_i.End;
                    break;
                case "B":
                    first.r.Start = range_i.Start;
                    break;
                case "C":
                    first.r.Start = range_i.Start;
                    first.r.End = range_i.End;
                    break;
            }
        }

        string GetOption(Range r1, Range r2)
        {
            if (r1.InRange(r2.Start) && r1.InRange(r2.End))
            {
                return "D";
            }

            if (r2.InRange(r1.Start) && r2.InRange(r1.End))
            {
                return "C";
            }

            if (r1.InRange(r2.Start) && r2.End > r1.End)
            {
                return "A";
            }

            if (r1.InRange(r2.End) && r2.Start < r1.Start)
            {
                return "B";
            }

            return "0";
        }

        ulong totalCount = 0;
        
        foreach (var normalizedRange in normalizedRanges)
        {
            totalCount += normalizedRange.Length;
        }
        
        return totalCount.ToString();
    }

    public string? Url { get; }
    public string? Title { get; }
    public string? Input { get; set; }

    record Range
    {
        public ulong Start { get; set; }
        public ulong End { get; set; }
        public bool InRange(ulong n) => n >= Start && n <= End;
        public ulong Length => End - Start + 1;

        public IEnumerable<ulong> Expand()
        {
            for (ulong i = Start; i <= End; i++)
            {
                yield return i;
            }
        }
    }
}
