using System.Text;
using Common;

namespace AdventOfCode.Year2016;

public class Day04Part2 : IProblemWithInput
{
    public string GetAnswer()
    {
        var answer = 0L;
        
        var lines = Input.Split(Environment.NewLine, StringSplitOptions.TrimEntries);

        foreach (var line in lines)
        {
            var parts = line.Split("[");
            parts[1] = parts[1].Substring(0, parts[1].Length - 1);

            var words = parts[0].Split('-');
            var shifts = int.Parse(words.Last());

            var result = new List<string>();

            for (var i = 0; i < 3; i++)
            {
                // qzmt
                // ranu
                // sbov
                // tcpw
                // udqx
                // very
                var newWord = string.Join("", words[i].Select(x => Shift(x, shifts)));
                result.Add(newWord);
            }
            
            Console.WriteLine($"{shifts} -> {string.Join(" ", result)}");
        }
        
        return answer.ToString();
        
        char Shift(char c, int shift)
        {
            int offset = c - 'a';
            int shifted = (offset + shift) % 26;
            if (shifted < 0) shifted += 26;

            return (char)('a' + shifted);
        }
    }

    public string Url => "!TBD";
    public string Title => "!TBD";
    public string? Input { get; set; }
}
