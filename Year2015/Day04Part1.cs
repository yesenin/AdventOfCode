using System.Security.Cryptography;
using System.Text;
using Common;

namespace AdventOfCode.Year2015;

public class Day04Part1 : BaseProblemWithInput
{
    protected override string GetAnswerInner()
    {
        var lines = Input!.Split('\n');

        var answer = 0;

        foreach (var line in lines)
        {
            string md5 = string.Empty;
            while (md5.Length < 6 || md5[..5] != "00000")
            {
                answer++;
                byte[] bytes = Encoding.UTF8.GetBytes($"{line}{answer}");
                byte[] hash = MD5.HashData(bytes);
                md5 = Convert.ToHexString(hash);
            }
        }
        
        return answer.ToString();
    }

    public override string Url => "https://adventofcode.com/2015/day/4";
    public override string Title => "!TBD";
}
