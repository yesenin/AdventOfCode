using System.Text;
using System.Text.RegularExpressions;
using Common;

namespace AdventOfCode.Year2025;

public sealed class Day11Part1 : IProblemWithInput
{
    public string GetAnswer()
    {
        var lines = Input.Split('\n', StringSplitOptions.TrimEntries);
        var answer = 0;

        var dict = new Dictionary<string, List<string>>();

        foreach (var line in lines)
        {
            var colonParts = line.Split(':', StringSplitOptions.TrimEntries);
            dict.Add(colonParts[0], colonParts[1].Split(' ', StringSplitOptions.TrimEntries).ToList());
        }

        var foo = new List<string>();
        var sb = new StringBuilder();
        sb.Append("you");
        Rec("you", sb);

        return $"{answer}";

        void Rec(string key, StringBuilder path)
        {
            // path.Append($" -> {key}");
            if (key == "out")
            {
                // foo.Add(sb.ToString());
                answer++;
                return;
            }
            foreach (var nextKey in dict[key])
            {
                Rec(nextKey, path);
            }
        }
    }

    public string? Url { get; }
    public string? Title { get; }
    public string? Input { get; set; }
}
