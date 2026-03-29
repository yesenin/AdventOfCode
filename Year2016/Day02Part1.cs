using System.Text;
using Common;

namespace AdventOfCode.Year2016;

public class Day02Part1 : IProblemWithInput
{
    public string GetAnswer()
    {
        var answer = 0L;
        
        var lines = Input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        var grid = new int[3, 3]
        {
            {1, 2, 3},
            {4, 5, 6},
            {7, 8, 9}
        };

        (int x, int y) position = (1, 1);
        var lastDigit = grid[position.y, position.x];

        var code = new StringBuilder();

        foreach (var line in lines)
        {
            for (var i = 0; i < line.Length; i++)
            {
                switch (line[i])
                {
                    case 'U':
                        position.y -= position.y > 0 ? 1 : 0;
                        break;
                    case 'D':
                        position.y += position.y < 2 ? 1 : 0;
                        break;
                    case 'L':
                        position.x -= position.x > 0 ? 1 : 0;
                        break;
                    case 'R':
                        position.x += position.x < 2 ? 1 : 0;
                        break;
                }
                lastDigit = grid[position.y, position.x];
            }
            code.Append(lastDigit);
        }

        return code.ToString();
    }

    public string? Url { get; }
    public string? Title { get; }
    public string? Input { get; set; }
}