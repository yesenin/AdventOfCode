using Common;

namespace AdventOfCode.Year2025;

public sealed class Day04Part2 : IProblemWithInput
{
    public long GetAnswer()
    {
        var lines = Input.Split('\n');
        var grid = new char[lines.Length, lines.First().Length];
        
        for (var j = 0; j < lines.Length; j++)
        {
            var line = lines[j];
            for (var i = 0; i < line.Length; i++)
            {
                grid[j, i] = line[i];
            }
        }

        var count = new HashSet<string>();
        int deleted;
        var toDelete = new Queue<(int x, int y)>();
        do
        {
            deleted = 0;
            for (var y = 0; y < grid.GetLength(0); y++)
            {
                for (var x = 0; x < grid.GetLength(1); x++)
                {
                    var current = grid[y, x];
                    var neighbors = GetNeighbors(x, y);
                    var rolls = neighbors.Where(x => x.value == '@');
                    if (current == '@' && rolls.Count() < 4)
                    {
                        count.Add($"{x}:{y}");
                        deleted++;
                        toDelete.Enqueue((x, y));
                    }
                }
            }

            while (toDelete.Any())
            {
                var item = toDelete.Dequeue();
                grid[item.y, item.x] = '.';
            }
        } while (deleted > 0);
        
        
        (int x, int y, char value)[] GetNeighbors(int x, int y)
        {
            var neighbors = new List<(int x, int y, char value)>();
            for (var j = -1; j <= 1; j++)
            {
                for (var i = -1; i <= 1; i++)
                {
                    if (i == 0 && j == 0) continue;
                    var newX = x + i;
                    var newY = y + j;
                    if (newX >= 0 && newX < grid.GetLength(1) && newY >= 0 && newY < grid.GetLength(0))
                    {
                        neighbors.Add((newX, newY, grid[newY, newX]));
                    }
                }
            }
            return neighbors.ToArray();
        }

        return count.Count;
    }

    public string Url => "https://adventofcode.com/2025/day/4";
    public string Title => "Sled Rental. Part 2";
    public string? Input { get; set; }
}
