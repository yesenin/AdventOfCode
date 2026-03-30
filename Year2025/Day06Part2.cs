using System.Text;
using Common;

namespace AdventOfCode.Year2025;

public sealed class Day06Part2 : IProblemWithInput
{
    public string GetAnswer()
    {
        var lines = Input.Split('\n').ToArray();
        ulong answer = 0;
        
        var length = lines.Max(x => x.Length);
        var height = lines.Length;

        var group = new List<string>();

        var fred = new List<string>();

        var needOp = true;
        var op = "";
        
        for (var i = 0; i < length; i++)
        {
            var col = new List<string>(height);
            
            for (var j = 0; j < height - 1; j++)
            {
                col.Add(lines[j][i].ToString());
            }

            if (needOp)
            {
                op = lines[height - 1][i].ToString();
                needOp = false;
            }

            if (col.All(x => x == " "))
            {
                group.Add(op);
                fred.Add(string.Join(";", group.Select(x => x.Trim())));
                group = new List<string>();
                needOp = true;
            }
            else
            {
                group.Add(string.Join("", col));
            }
        }
        
        group.Add(op);
        fred.Add(string.Join(";", group.Select(x => x.Trim())));

        foreach (var f in fred)
        {
            var parts = f.Split(';', StringSplitOptions.RemoveEmptyEntries);
            var items =  parts.Take(parts.Length - 1).Select(x => ulong.Parse(x)).ToArray();
            var o = parts.Last();
            if (o == "*")
            {
                answer += Mul(items);
            }

            if (o == "+")
            {
                answer += Add(items);
            } 
        }
        
        
        return answer.ToString();

        ulong Add(params ulong[] values)
        {
            ulong result = 0;
            foreach (var value in values)
            {
                result += value;
            }
            return result;
        }
        
        ulong Mul(params ulong[] values)
        {
            ulong result = 1;
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
