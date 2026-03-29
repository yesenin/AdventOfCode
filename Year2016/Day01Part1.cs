using Common;

namespace AdventOfCode.Year2016;

public class Day01Part1 : IProblemWithInput
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

                switch (direction)
                {
                    case 'N':
                        position = (position.x, position.y + length);
                        break;
                    case 'E':
                        position = (position.x + length, position.y);
                        break;
                    case 'S':
                        position = (position.x, position.y - length);
                        break;
                    case 'W':
                        position = (position.x - length, position.y);
                        break;
                }
                previousDirection = direction;
            }
            finishes.Add(Math.Abs(position.x) + Math.Abs(position.y));
        }
        
        return $"{string.Join(", ", finishes)}";
    }

    public string? Url { get; }
    public string? Title { get; }
    public string? Input { get; set; }
}