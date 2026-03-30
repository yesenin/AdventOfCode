using Common;

namespace AdventOfCode.Year2015;

public class Day05Part2 : IProblemWithInput
{
    public string GetAnswer()
    {
        var lines = Input.Split('\n').Select(x => x.Trim()).ToArray();
        var answer = new List<string>();
        var count = 0;
        foreach (var line in lines)
        {
            var state = new State();
            Rec(line, 0, state);
            if (state.Pairs.Any(x => x.Value > 1)
                && state.Wrappers.Count > 0)
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

            if (!string.IsNullOrEmpty(state.PrevLetter))
            {
                var pair = $"{state.PrevLetter}{line[i]}";
                if (!state.Pairs.TryAdd(pair, 1))
                {
                    state.Pairs[pair]++;
                }
            }

            if (!string.IsNullOrEmpty(state.PrevPrevLetter))
            {
                if (state.PrevPrevLetter == line[i].ToString())
                {
                    state.Wrappers.Add($"{state.PrevPrevLetter}{state.PrevLetter}{line[i]}");
                    state.PrevPrevLetter = state.PrevLetter;
                }
            }
            
            if (i > 1)
            {
                state.PrevPrevLetter = state.PrevLetter;
            }
            
            state.PrevLetter = line[i].ToString();
            
            Rec(line, i + 1, state);
        }
    }

    class State
    {
        public Dictionary<string, int> Pairs { get; } = new Dictionary<string, int>();

        public List<string> Wrappers { get; } = new List<string>();
        
        public string? PrevLetter { get; set; }
        
        public string? PrevPrevLetter { get; set; }
    }

    public string Url => "!TBD";
    public string Title => "!TBD";
    public string? Input { get; set; }
}
