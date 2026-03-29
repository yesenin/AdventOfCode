using Common;

namespace AdventOfCode.Year2025;

public sealed class Day02Part2 : IProblemWithInput
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
                // Console.WriteLine($"Check number {i}");
                var iString = i.ToString();
                if (iString.Length < 2)
                {
                    continue;
                }
                var isRepeating = true;
                for (var j = 1; j <= iString.Length / 2; j++)
                {
                    isRepeating = IsRepeating(iString, j);
                    if (isRepeating)
                    {
                        break;
                    }
                }
                if (isRepeating)
                {
                    Console.WriteLine(iString);
                    answer += i;
                    //Console.WriteLine("Stop!");
                    //break;
                }
            }
        }
        return answer.ToString();
    }

    private bool IsRepeating(string line, int subStringToFindLength)
    {
        var subString = line.Substring(0, subStringToFindLength);
        var k = subStringToFindLength;
        //Console.WriteLine($"Substring to check: {subString}");
        while (k + subStringToFindLength <=  line.Length)
        {
            var nextSubstring = line.Substring(k, subStringToFindLength);
            //Console.WriteLine($"Check substring: {nextSubstring}");
            if (nextSubstring != subString)
            {
                //Console.WriteLine("False");
                return false;
            }
            k += subStringToFindLength;
        }

        if (k < line.Length)
        {
            return false;
        }
        //Console.WriteLine("True");
        return true;
    }

    public string? Url { get; }
    public string? Title { get; }
    public string? Input { get; set; }
}
