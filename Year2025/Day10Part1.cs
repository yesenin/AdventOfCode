using System.Text;
using System.Text.RegularExpressions;
using Common;

namespace AdventOfCode.Year2025;

public partial class Day10Part1 : IProblemWithInput
{
    public long GetAnswer()
    {
        var lines = Input.Split('\n', StringSplitOptions.TrimEntries);
        var answer = 0;

        var targetRegex = TargetRegex();
        var buttonRegex = ButtonRegex();
        var joltageRegex = JoltageRegex();

        var inputs = new List<Input>();
        
        foreach (var line in lines)
        {
            var targetMatch = targetRegex.Match(line);
            var target = targetMatch.Groups[1].Value.Replace(".", "0").Replace("#", "1");
            var buttonMatches =  buttonRegex.Matches(line);
            var buttons = buttonMatches
                .Select(m => new Button(m.Groups[1].Value.Split(',').Select(int.Parse).ToArray(), target.Length))
                .ToArray();
            var joltageMatch = joltageRegex.Match(line);
            var joltage = joltageMatch.Groups[1].Value.Split(',', StringSplitOptions.TrimEntries).Select(int.Parse).ToArray();
            
            inputs.Add(
                new Input(target, buttons, joltage)    
            );
        }

        foreach (var input in inputs)
        {
            DoInput(input);
        }

        return answer;
    }

    private void DoInput(Input input)
    {
        // b -- button indecies
        // q -- clicks
        var dict = new Dictionary<string, HashSet<string>>(); 
        Rec(input.buttons, 0);
        
        Console.WriteLine(input.target);

        void Rec(Button[] buttons, int index)
        {
            if (index == buttons.Length)
            {
                return;
            }
            var button = buttons[index];
            var a = 0b0 ^ button.SwitchesAsBin;
            var key = $"{index}";
            dict.Add(key, new HashSet<string>());
            dict[key].Add(Convert.ToString(a, 2).PadLeft(input.target.Length, '0'));
            var added = true;
            while (added)
            {
                var temp = dict[key].Count;
                a ^=  button.SwitchesAsBin;
                if (a != 0b0)
                {
                    dict[key].Add(Convert.ToString(a, 2).PadLeft(input.target.Length, '0'));
                }

                added = temp != dict[key].Count;
            }
            Rec(buttons, index + 1);
        }
    }

    public string Url => "!TBD";
    public string Title => "!TBD";
    public string? Input { get; set; }

    [GeneratedRegex(@"\[(.+?)\]")]
    private static partial Regex TargetRegex();
    
    [GeneratedRegex(@"\((.+?)\)")]
    private static partial Regex ButtonRegex();
    
    [GeneratedRegex(@"\{(.+?)\}")]
    private static partial Regex JoltageRegex();
}

record Input(string target, Button[] buttons, int[] joltage)
{
    public int TargetAsBin => Convert.ToInt32(target, 2);
}

class Button
{
    private string _switches;
    
    public Button(int[] switches, int targetSize)
    {
        var s = new StringBuilder();
        for (var i = 0; i < targetSize; i++)
        {
            s.Append(switches.Contains(i) ? "1" : "0");
        }
        _switches = s.ToString();
    }
    
    public string Switches => _switches;
    
    public int SwitchesAsBin => Convert.ToInt32(_switches, 2);
}
