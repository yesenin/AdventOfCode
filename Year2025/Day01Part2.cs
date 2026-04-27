using Common;

namespace AdventOfCode.Year2025;

public sealed class Day01Part2 : IProblemWithInput
{
    public long GetAnswer()
    {
        var a = Input.Split('\n');
        var pos = 50;
        var answer = 0;
            Console.WriteLine($"INIT\t{pos}");
        
        foreach (var t in a)
        {
            var direction = t[0];
            var value = int.Parse(t[1..]);
            int prevPos = pos;
            if (direction == 'R')
            {
                pos += value;
            }

            if (direction == 'L')
            {
                pos -= value;
            }
            var posAbs = Math.Abs(pos);

            var zero = "";

            if (pos >= 100)
            {
                var c =  posAbs / 100;
                for (var j = 0; j < c; j++)
                {
                    zero = ">> 0";
                    answer += 1;
                }
                pos %= 100;

            }
            else if (pos <= 0)
            {
                
                var d = posAbs % 100;
                var c = posAbs / 100;
                var add = prevPos == 0 ? 0 : 1;
                for (var j = 0; j < c + add; j++)
                {
                    zero = "<< 0";
                    answer += 1;
                }
                if (d == 0)
                {
                    pos = 0;
                }
                else
                {
                    pos = 100 - d;
                }
            }

            if (pos == 0)
            {
                zero = "> 0 <";
                // answer += 1;
            }

            var sign = direction == 'L' ? '-' : '+';
            var operation = $"{prevPos}{sign}{value}";
            Console.WriteLine($"{t}\t{pos}\t{operation}");
            if (!string.IsNullOrEmpty(zero))
            {
                Console.WriteLine(zero);
            }
        }
        return answer;
    }
    
    public string Url => "https://adventofcode.com/2025/day/1";
    public string Title => "Day 1: Chronal Calibration. Part 2";
    public string? Input { get; set; }
}
