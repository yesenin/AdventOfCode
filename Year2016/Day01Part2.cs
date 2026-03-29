using Common;

namespace AdventOfCode.Year2016;

public class Day01Part2 : IProblemWithInput
{
    public string GetAnswer()
    {
        var answer = 0L;
        
        var lines = Input.Split(Environment.NewLine, StringSplitOptions.TrimEntries);

        var finishes = new List<long>();
        
        foreach (var line in lines)
        {
            var jumps = line.Split(',', StringSplitOptions.TrimEntries);
            (long x, long y) position = (0, 0);
            var positions = new Dictionary<string, int>();
            positions.Add($"{position.x}:{position.y}", 1);
            var previousDirection = 'N'; // N, E, S, W
            foreach (var jump in jumps)
            {
                var turn = jump[0];
                var length = long.Parse(jump.Substring(1, jump.Length - 1));
                var direction = '0';
                switch (previousDirection)
                {
                    case 'N':
                        switch (turn)
                        {
                            case 'R':
                                direction = 'E';
                                break;
                            case 'L':
                                direction = 'W';
                                break;
                        }
                        break;
                    case 'E':
                        switch (turn)
                        {
                            case 'R':
                                direction = 'S';
                                break;
                            case 'L':
                                direction = 'N';
                                break;
                        }
                        break;
                    case 'S':
                        switch (turn)
                        {
                            case 'R':
                                direction = 'W';
                                break;
                            case 'L':
                                direction = 'E';
                                break;
                        }
                        break;
                    case 'W':
                        switch (turn)
                        {
                            case 'R':
                                direction = 'N';
                                break;
                            case 'L':
                                direction = 'S';
                                break;
                        }
                        break;
                }

                var x = 0L;
                var y = 0L;
                var step = 0;
                switch (direction)
                {
                    case 'N':
                        y = position.y + length;
                        step = 1;
                        break;
                    case 'E':
                        x = position.x + length;
                        step = 1;
                        break;
                    case 'S':
                        y = position.y - length;
                        step = -1;
                        break;
                    case 'W':
                        x = position.x - length;
                        step = -1;
                        break;
                }

                if (x != 0)
                {
                    var ix = position.x + step;
                    while (ix <= x)
                    {
                        var key1 = $"{ix}:{position.y}";
                        if (positions.ContainsKey(key1))
                        {
                            finishes.Add(Math.Abs(position.x) + Math.Abs(position.y));
                            break;
                        }

                        positions.Add(key1, 1);
                        ix += step;
                    }
                }

                if (y != 0)
                {
                    var iy = position.y + step;
                    while (iy <= y)
                    {
                        var key2 = $"{position.x}:{iy}";
                        if (positions.ContainsKey(key2))
                        {
                            finishes.Add(Math.Abs(position.x) + Math.Abs(position.y));
                            break;
                        }

                        positions.Add(key2, 1);
                        iy += step;
                    }
                }

                previousDirection = direction;
            }
        }
        
        return $"{string.Join(", ", finishes)}";
    }

    public string? Url { get; }
    public string? Title { get; }
    public string? Input { get; set; }
}