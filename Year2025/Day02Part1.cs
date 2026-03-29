using Common;

namespace AdventOfCode.Year2025;

public sealed class Day02Part1 : IProblemWithInput
{
    public string GetAnswer()
    {
        ulong answer = 0;
        var a = Input.Split(',');
        foreach (var range in a)
        {
            var parts = range.Split('-');
            var start = ulong.Parse(parts[0]);
            var end = ulong.Parse(parts[1]);
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
        return answer.ToString();
    }

    public string? Url { get; }
    public string? Title { get; }
    public string? Input { get; set; }
}
