using System.Text;
using Common;

namespace AdventOfCode.Year2016;

public class Day02Part2 : IProblemWithInput
{
    public string GetAnswer()
    {
        var answer = 0L;
        
        var lines = Input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        var grid = new string[7, 7]
        {
            {"x", "x", "x", "x", "x", "x", "x"},
            {"x", "x", "x", "1", "x", "x", "x"},
            {"x", "x", "2", "3", "4", "x", "x"},
            {"x", "5", "6", "7", "8", "9", "x"},
            {"x", "x", "A", "B", "C", "x", "x"},
            {"x", "x", "x", "D", "x", "x", "x"},
            {"x", "x", "x", "x", "x", "x", "x"},
        };

        (int x, int y) position = (1, 3);
        var lastDigit = grid[position.y, position.x];

        var code = new StringBuilder();

        foreach (var line in lines)
        {
            for (var i = 0; i < line.Length; i++)
            {
                switch (line[i])
                {
                    case 'U':
                        position.y -= grid[position.y - 1, position.x] == "x" ? 0 : 1;
                        break;
                    case 'D':
                        position.y += grid[position.y + 1, position.x] == "x" ? 0 : 1;
                        break;
                    case 'L':
                        position.x -= grid[position.y, position.x - 1] == "x" ? 0 : 1;
                        break;
                    case 'R':
                        position.x += grid[position.y, position.x + 1] == "x" ? 0 : 1;
                        break;
                }
                lastDigit = grid[position.y, position.x];
            }
            code.Append(lastDigit);
        }

        return code.ToString();
    }

    public string Url => "!TBD";
    public string Title => "!TBD";
    public string? Input { get; set; }
}
