using Common;

namespace AdventOfCode.Year2025;

public sealed class Day07Part2 : IProblemWithInput
{
    public string Input { get; set; } = string.Empty;

    public string GetAnswer()
    {
        var lines = Input.Split('\n');
        // TODO: review that
        var memo = new Dictionary<string, ulong>();

        var map = new Stack<string>();
        var beams = new Stack<int>();

        ulong answer = Rec(0, map, beams);

        Console.WriteLine("Done");

        return answer.ToString();

        ulong Rec(int lineIndex, Stack<string> s, Stack<int> b)
        {
            var memoKey = $"{lineIndex}:{string.Join(',', b.ToArray())}";
            if (memo.TryGetValue(memoKey, out var cached))
            {
                return cached;
            }

            if (lineIndex == lines.Length)
            {
                return 1;
            }

            var line = lines[lineIndex];
            var nextLine = lineIndex < lines.Length - 1 ? lines[lineIndex + 1] : string.Empty;
            var toAdd = new List<int>();
            var toRemove = 0;

            if (lineIndex == 0)
            {
                toAdd.Add(line.IndexOf('S'));
            }
            else
            {
                foreach (var beamIndex in b.Where(beamIndex => line[beamIndex] == '.'))
                {
                    line = string.Concat(
                        line.AsSpan(0, beamIndex),
                        "|",
                        line.AsSpan(beamIndex + 1)
                    );
                }

                if (!string.IsNullOrEmpty(nextLine))
                {
                    foreach (var beamIndex in b.Where(beamIndex => nextLine[beamIndex] == '^'))
                    {
                        toRemove++;
                        toAdd.Add(beamIndex - 1);
                        toAdd.Add(beamIndex + 1);
                    }
                }
            }

            s.Push(line);
            for (var i = 0; i < toRemove; i++)
            {
                b.Pop();
            }

            ulong localAnswer = 0;

            if (toAdd.Any())
            {
                foreach (var toAddItem in toAdd)
                {
                    b.Push(toAddItem);
                    localAnswer += Rec(lineIndex + 1, s, b);
                    if (b.Any())
                    {
                        b.Pop();
                    }
                }
            }
            else
            {
                localAnswer += Rec(lineIndex + 1, s, b);
            }

            s.Pop();

            memo[memoKey] = localAnswer;
            return localAnswer;
        }
    }
    
    public string Url => "!TBD";
    public string Title => "!TBD";
}
