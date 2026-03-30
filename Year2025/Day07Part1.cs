using Common;

namespace AdventOfCode.Year2025;

public sealed  class Day07Part1 : IProblemWithInput
{
    public string GetAnswer()
    {
        var answer = 0;
        var lines = Input.Split('\n');

        var firstLine = lines.First();
        var start = firstLine.IndexOf('S');
        
        Console.WriteLine(firstLine);

        var work = new HashSet<int>
        {
            start
        };

        for (var i = 1; i < lines.Length; i++)
        {
            var newLine = lines[i];
            var toRemove = new List<int>();
            var toAdd = new List<int>();
            foreach (var item in work)
            {
                if (newLine[item] == '.')
                {
                    newLine = newLine.Substring(0, item) + "|" + newLine.Substring(item + 1);
                }

                if (i < lines.Length - 1 && lines[i+1][item] == '^')
                {
                    answer++;
                    toRemove.Add(item);
                    toAdd.Add(item - 1);
                    toAdd.Add(item + 1);
                }
            }
            Console.WriteLine(newLine + " " + answer);
            foreach (var removeItem in toRemove)
            {
                work.Remove(removeItem);
            }

            foreach (var addItem in toAdd)
            {
                work.Add(addItem);
            }
        }
        
        return answer.ToString();
    }

    public string Url => "!TBD";
    public string Title => "!TBD";
    public string? Input { get; set; }

    record Range(ulong Start, ulong End)
    {
        public bool InRange(ulong n) => n >= Start && n <= End;
    }
}
