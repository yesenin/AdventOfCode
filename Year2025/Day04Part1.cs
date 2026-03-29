using Common;

namespace AdventOfCode.Year2025;

public sealed class Day04Part1 : IProblemWithInput
{
    public string GetAnswer()
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
                }
            }
        }   
        
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

        return count.Count.ToString();
    }

    public string? Url { get; }
    public string? Title { get; }
    public string? Input { get; set; }
}
