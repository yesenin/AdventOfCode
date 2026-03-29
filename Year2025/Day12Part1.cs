using System.Text;
using Common;

namespace AdventOfCode.Year2025;

public sealed class Day12Part1 : IProblemWithInput
{
    public string GetAnswer()
    {
        var lines = Input.Split('\n', StringSplitOptions.TrimEntries);
        var answer = 0;

        var figures = new Dictionary<int, List<string>>();

        for (var i = 0; i < 30; i += 5)
        {
            var index = int.Parse(lines[i][0].ToString());
            figures.Add(index, new List<string>());
            for (var j = 1; j < 4; j++)
            {
                figures[index].Add(lines[i + j]);
            }
        }

        var sizes = new List<Size>();
        var orders = new List<int[]>();

        foreach (var line in lines.Skip(30))
        {
            var parts = line.Split(':',  StringSplitOptions.TrimEntries);
            
            var sizeParts = parts[0].Split('x');
            var width = int.Parse(sizeParts[0]);
            var height = int.Parse(sizeParts[1]);
            sizes.Add(new Size(width, height));
            
            var orderParts = parts[1].Split(' ', StringSplitOptions.TrimEntries);
            var order = orderParts.Select(int.Parse).ToArray();
            orders.Add(order);
        }

        for (var i = 0; i < 3; i++)
        {
            ProcessSizeAndOrder(sizes[i], orders[i]);
        }

        foreach (var figure in figures)
        {
            var fig = new StringBuilder();
            foreach (var line in figure.Value)
            {
                fig.AppendLine(line);
            }
            Console.WriteLine(fig.ToString());
        }

        return $"{answer}";

        void ProcessSizeAndOrder(Size size, int[] order)
        {
            var map = new StringBuilder();
            for (var y = 0; y < size.height; y++)
            {
                var row = new StringBuilder();
                for (var x = 0; x < size.width; x++)
                {
                    row.Append('.');
                }
                map.AppendLine(row.ToString());
            }
            
            Console.WriteLine(map.ToString());
        }
    }

    public string? Url { get; }
    public string? Title { get; }
    public string? Input { get; set; }
    
    record Size(int width, int height);
}
