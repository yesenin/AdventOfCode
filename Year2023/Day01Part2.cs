using Common;

namespace AdventOfCode.Year2023;

public class Day01Part2 : IProblemWithInput
{
    public string GetAnswer()
    {
        var lines = Input.Split('\n').Select(l => l.Trim()).ToArray();
        var answer = 0;

        var digits = new List<string>
        {
            "zero",
            "one",
            "two",
            "three",
            "four",
            "five",
            "six",
            "seven",
            "eight",
            "nine"
        };

        foreach (var line in lines)
        {
            var i = 0;
            var digit_i = -1;
            while (!("0123456789".Contains(line[i]) || HasString(line.Substring(i), out digit_i)))
            {
                i++;
            }
            var j = line.Length - 1;
            var digit_j = -1;
            while (!("0123456789".Contains(line[j]) || HasString(line.Substring(j), out digit_j)))
            {
                j--;
            }
            
            var a = digit_i >= 0 ? digit_i : int.Parse(line[i].ToString());
            var b = digit_j >= 0 ? digit_j : int.Parse(line[j].ToString());
            //Console.WriteLine($"{a} {b}");
            answer += int.Parse($"{a}{b}");
        }
        
        return answer.ToString();

        bool HasString(string s, out int i)
        {
            var result = false;
            i = -1;
            foreach (var digit in digits)
            {
                if (s.Length >= digit.Length && s.Substring(0, digit.Length) == digit)
                {
                    i = digits.IndexOf(digit);
                    result = true;
                    break;
                }
            }
            return result;
        }
    }

    public string? Url { get; }
    public string? Title { get; }
    public string? Input { get; set; }
}