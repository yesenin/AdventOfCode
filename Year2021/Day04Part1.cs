using Common;

namespace AdventOfCode.Year2021;

public class Day04Part1 : IProblemWithInput
{
    public string GetAnswer()
    {
        var answer = 0L;
        var lines = Input.Split(Environment.NewLine, StringSplitOptions.TrimEntries);

        var series = lines.First().Split(',', StringSplitOptions.TrimEntries).Select(int.Parse).ToArray();
        
        var cards = new List<Card>();
        
        var i = 1;
        var currentCard = new Card(i);
        
        foreach (var line in lines.Skip(2))
        {
            if (line == "")
            {
                cards.Add(currentCard);
                currentCard = new Card(++i);
                continue;
            }
            currentCard.AddLine(line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList());
        }
        cards.Add(currentCard);

        answer = PlaySeries();
        
        return $"{answer}";

        int PlaySeries()
        {
            var i = 0;
            var winCards = new HashSet<Card>();
            while (i < series.Length)
            {
                var number =  series[i];
                foreach (var card in cards)
                {
                    if (card.CheckNumber(number))
                    {
                        var notCrossed = card.NotCrossedNumbers();
                        return notCrossed.Sum() * card.LastCrossed;
                    }
                }
                i++;
            }

            if (winCards.Any())
            {
                return winCards.First().Score();
            }

            return 0;
        }
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
