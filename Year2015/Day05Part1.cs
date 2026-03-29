using Common;

namespace AdventOfCode.Year2015;

public class Day05Part1 : IProblemWithInput
{
    public string GetAnswer()
    {
        var lines = Input.Split('\n').Select(x => x.Trim()).ToArray();
        var answer = new List<string>();
        var count = 0;
        foreach (var line in lines)
        {
            var state = new State()
            {
                PrevConsonant = '_',
            };
            Rec(line, 0, state);
            if (state.Vowels.Sum(x => x.Value) > 2 
                && state.DoubleConsonant.Count > 0
                && state.BadDoubles.Count == 0)
            {
                answer.Add($"{line} is a nice");
                count++;
            }
            else
            {
                answer.Add($"{line} is a naughty");
            }
        }
        
        return string.Join("\n", answer) + $"\n{count}";

        void Rec(string line, int i, State state)
        {
            if (i == line.Length)
            {
                return;
            }

            if ("aeiou".Contains(line[i]))
            {
                if (state.Vowels.ContainsKey(line[i]))
                {
                    state.Vowels[line[i]]++;
                }
                else
                {
                    state.Vowels.Add(line[i], 1);
                }
            }
            if (line[i] == state.PrevConsonant)
            {
                state.DoubleConsonant.Add($"{state.PrevConsonant}{line[i]}");
            }

            if (new[] {"ab", "cd", "pq", "xy"}.Contains($"{state.PrevConsonant}{line[i]}"))
            {
                state.BadDoubles.Add($"{state.PrevConsonant}{line[i]}");
            }
            state.PrevConsonant = line[i];
            Rec(line, i + 1, state);
        }
    }

    class State
    {
        public Dictionary<char, int> Vowels { get; } = new Dictionary<char, int>();

        public List<string> DoubleConsonant { get; } = new List<string>();

        public List<string> BadDoubles { get; } = new List<string>();
        
        public char PrevConsonant { get; set; }
    }

    public string? Url { get; }
    public string? Title { get; }
    public string? Input { get; set; }
}