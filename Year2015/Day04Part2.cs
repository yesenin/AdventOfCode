using System.Security.Cryptography;
using System.Text;
using Common;

namespace AdventOfCode.Year2015;

public class Day04Part2 : BaseProblemWithInput
{
    protected override long GetAnswerInner()
    {
        var lines = Input!.Split('\n');

        var answer = 0;

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
                if (md5.Substring(0, 6) == "000000")
                {
                    break;
                }
                i++;
            }
            
            answer += i;
        }
        
        return answer;
    }
    
    record Point(int X, int Y);

    public override string Url => "https://adventofcode.com/2015/day/4";
    public override string Title => "!TBD";
}
