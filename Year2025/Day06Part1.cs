using System.Text;
using Common;

namespace AdventOfCode.Year2025;

public sealed class Day06Part1 : IProblemWithInput
{
    public long GetAnswer()
    {
        var lines = Input.Split('\n').ToArray();

        var columns = new List<StringBuilder>();

        long answer = 0;

        foreach (var line in lines)
        {
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (columns.Count == 0)
            {
                for (var i = 0; i < parts.Length; i++)
                {
                    columns.Add(new StringBuilder(parts[i]));
                }
            }
            else
            {
                for (var i = 0; i < parts.Length; i++)
                {
                    columns[i].Append($" {parts[i]}");
                }
            }
        }

        foreach (var column in columns)
        {
            var columnStr = column.ToString();
            var parts = columnStr.Split(' ');
            var op = parts.Last();
            var items = parts.Take(parts.Length - 1).Select(x => long.Parse(x)).ToArray();
            if (op == "*")
            {
                answer += Mul(items);
            }

            if (op == "+")
            {
                answer += Add(items);
            }
        }
        
        return answer;

        long Add(params long[] values)
        {
            long result = 0;
            foreach (var value in values)
            {
                result += value;
            }
            return result;
        }
        
        long Mul(params long[] values)
        {
            long result = 1;
            foreach (var value in values)
            {
                result *= value;
            }
            return result;
        }
    }

    public string Url => "!TBD";
    public string Title => "!TBD";
    public string? Input { get; set; }

    record Range(ulong Start, ulong End)
    {
        public bool InRange(ulong n) => n >= Start && n <= End;
    }
}
