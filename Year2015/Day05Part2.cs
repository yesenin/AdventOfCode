using Common;

namespace AdventOfCode.Year2015;

public class Day05Part2 : BaseProblemWithInput
{
    protected override string GetAnswerInner()
    {
        var lines = Input!.Split('\n').Select(x => x.Trim()).ToArray();
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
        public Dictionary<string, int> Pairs { get; } = [];

        public List<string> Wrappers { get; } = [];
        
        public string? PrevLetter { get; set; }
        
        public string? PrevPrevLetter { get; set; }
    }

    public override string Url => "https://adventofcode.com/2015/day/5";
    public override string Title => "!TBD";
}
