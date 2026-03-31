using Common;

namespace AdventOfCode.Year2025;

public sealed class Day01Part1 : IProblemWithInput
{
    public long GetAnswer()
    {
        var a = Input.Split('\n');
        var pos = 50;
        var prev_pos = 0;
        var answer = 0;
        for (var i = 0; i < a.Length; i++)
        {
            var direction = a[i][0];
            var value = int.Parse(a[i].Substring(1));
            prev_pos = pos;
            if (direction == 'R')
            {
                pos += value;
            }

            if (direction == 'L')
            {
                pos -= value;
            }

            if (pos >= 100)
            {
                pos %= 100;
            }

            if (pos < 0)
            {
                var d = Math.Abs(pos % 100);
                if (d == 0)
                {
                    pos = 0;
                }
                else
                {
                    pos = 100 - d;
                }
            }
            Console.WriteLine($"{a[i]}\t{pos}\t{prev_pos - pos}");
            if (pos == 0)
            {
                answer++;
            }
        }
        return answer;
    }

    private int ModAdd(int a, int b, int mod)
    {
        var result = a + b;
        if (result > mod)
        {
            return result % mod;
        }

        return result;
    }
    
    private int ModSub(int a, int b, int mod)
    {
        var result = a - b;
        if (result > 0)
        {
            return mod - (result % mod);
        }

        return result;
    }

    public string Url => "https://adventofcode.com/2025/day/1";
    public string Title => "Day 1: Chronal Calibration";
    public string? Input { get; set; }
}
