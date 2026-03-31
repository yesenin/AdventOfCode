using Common;

namespace AdventOfCode.Year2025;

public sealed class Day02Part1 : IProblemWithInput
{
    public long GetAnswer()
    {
        long answer = 0;
        var a = Input.Split(',');
        foreach (var range in a)
        {
            var parts = range.Split('-');
            var start = long.Parse(parts[0]);
            var end = long.Parse(parts[1]);
            for (var i = start; i <= end; i++)
            {
                var iString = i.ToString();
                if (iString.Length % 2 != 0)
                {
                    continue;
                }
                var half = i.ToString().Length / 2;
                var head = Convert.ToInt32(iString.Substring(0, half));
                var tail = Convert.ToInt32(iString.Substring(half));
                if (head == tail)
                {
                    answer += i;
                }
            }
        }
        return answer;
    }

    public string Url => "!TBD";
    public string Title => "!TBD";
    public string? Input { get; set; }
}
