using System.Security.Cryptography;
using System.Text;
using Common;

namespace AdventOfCode.Year2015;

public class Day04Part1 : BaseProblemWithInput
{
    protected override string GetAnswerInner()
    {
        var lines = Input!.Split('\n');

        var answer = new List<string>();

        foreach (var line in lines)
        {
            var noZeros = true;
            string md5 = "";
            var i = 1;
            while (noZeros)
            {
                byte[] bytes = Encoding.UTF8.GetBytes($"{line}{i}");
                byte[] hash = MD5.HashData(bytes);
                md5 = Convert.ToHexString(hash);
                if (md5.Substring(0, 5) == "00000")
                {
                    break;
                }
                i++;
            }
            
            answer.Add(i.ToString());
        }
        
        return string.Join("\n", answer);
    }
    
    record Point(int X, int Y);

    public override string Url => "https://adventofcode.com/2015/day4";
    public override string Title => "!TBD";
}
