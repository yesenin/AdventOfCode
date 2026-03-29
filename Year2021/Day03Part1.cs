using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Common;

namespace AdventOfCode.Year2021;

public class Day03Part1 : IProblemWithInput
{
    public string GetAnswer()
    {
        var answer = 0L;
        var lines = Input.Split(Environment.NewLine, StringSplitOptions.TrimEntries);

        var gamma = new StringBuilder();
        var epsilon = new StringBuilder();

        var zeros = Enumerable.Range(0, lines.Length).ToList();
        var ones = Enumerable.Range(0, lines.Length).ToList();
        
        for (var i = 0; i < lines.First().Length; i++)
        {
            foreach (var line in lines)
            {
                if (line[i] == '0')
                {
                    zeros[i]++;
                }
                else
                {
                    ones[i]++;
                }
            }
        }

        for (var i = 0; i < lines.First().Length; i++)
        {
            if (zeros[i] > ones[i])
            {
                gamma.Append("0");
                epsilon.Append("1");
            }
            else
            {
                gamma.Append("1");
                epsilon.Append("0");
            }
        }
        
        var gammaInt = int.Parse(gamma.ToString(), NumberStyles.BinaryNumber);
        var epsilonInt = int.Parse(epsilon.ToString(), NumberStyles.BinaryNumber);
        
        answer = gammaInt * epsilonInt;
        
        return $"{answer}";
    }

    public string? Url { get; }
    public string? Title { get; }
    public string? Input { get; set; }
}