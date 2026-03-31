using System.Text.RegularExpressions;
using Common;

namespace AdventOfCode.Year2021;

public class Day05Part1 : IProblemWithInput
{
    public long GetAnswer()
    {
        var answer = 0L;
        var lines = Input.Split(Environment.NewLine, StringSplitOptions.TrimEntries);

        var lineRegex = new Regex(@"(\d+),(\d+)\s->\s(\d+),(\d+)");

        var grid = new Dictionary<string, int>();
        
        foreach (var line in lines)
        {
            var match = lineRegex.Match(line);
            if (match.Success)
            {
                var x1 =  int.Parse(match.Groups[1].Value);
                var y1 = int.Parse(match.Groups[2].Value);
                var x2 =  int.Parse(match.Groups[3].Value);
                var y2 = int.Parse(match.Groups[4].Value);
                
                var fromX = Math.Min(x1, x2);
                var fromY = Math.Min(y1, y2);
                var toX = Math.Max(x1, x2);
                var toY = Math.Max(y1, y2);

                if (fromX != toX && fromY != toY)
                {
                    continue;
                }

                for (var x = fromX; x <= toX; x++)
                {
                    for (var y = fromY; y <= toY; y++)
                    {
                        var key = $"({x},{y})";
                        if (!grid.TryAdd(key, 1))
                        {
                            grid[key]++;
                        }
                    }
                }
            }
        }

        answer = grid.Count(x => x.Value >= 2);
        
        return answer;
    }

    public string Url => "!TBD";
    public string Title => "!TBD";
    public string? Input { get; set; }

    class Card(int id)
    {
        private readonly Dictionary<int, List<int>> _lines = new Dictionary<int, List<int>>();
        private readonly Dictionary<int, bool> _numbers = new Dictionary<int, bool>();

        public void AddLine(List<int> line)
        {
            _lines.TryAdd(_lines.Count,  line);
            foreach (var number in line)
            {
                _numbers.Add(number, false);
            }
        }

        public bool CheckNumber(int number)
        {
            if (!_numbers.Keys.Contains(number))
            {
                return false;
            }
            _numbers[number] = true;
            LastCrossed = number;
            return AnyRow() || AnyColumn();
        }

        private bool AnyRow()
        {
            return _lines.Any(line => line.Value.All(n => _numbers[n]));
        }

        private bool AnyColumn()
        {
            return Enumerable.Range(0, _lines.Count).Any(col => _lines
                .Count(n => _numbers[n.Value[col]]) == 5);
        }

        public List<int> NotCrossedNumbers()
        {
            return _numbers.Where(n => !n.Value)
                .Select(n => n.Key).ToList();
        }
        
        public int LastCrossed { get; private set; }
        
        public int Id { get; private set; } = id;

        public int Score()
        {
            var fullRow = _lines.Single(x => x.Value.All(y => _numbers[y])).Value.Sum();
            return fullRow * LastCrossed;
        }
    }
}
