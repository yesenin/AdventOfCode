using Common;

namespace AdventOfCode.Year2020;

public class Day05Part2 : IProblemWithInput
{
    public string GetAnswer()
    {
        ulong answer = 0;
        var lines = Input.Split('\n', StringSplitOptions.TrimEntries);
        
        var seats = new List<ulong>();
        
        foreach (var line in lines)
        {
            seats.Add((ulong)GetId(line));
        }
        
        seats.Sort();

        for (var i = 0; i < seats.Count - 1; i++)
        {
            if (seats[i + 1] - seats[i] > 1)
            {
                answer = seats[i] + 1;
                break;
            }
        }
        
        return $"{answer}";

        int GetId(string input)
        {
            var i = 0;
            var rowStart = 0;
            var rowEnd = 127;
            var colStart = 0;
            var colEnd = 7;
            while (i < input.Length)
            {
                switch (input[i])
                {
                    case 'F':
                        rowEnd = rowStart + (rowEnd - rowStart) / 2;
                        break;
                    case 'B':
                        rowStart = rowStart + (rowEnd - rowStart) / 2 + 1;
                        break;
                    case 'L':
                        colEnd = colStart + (colEnd - colStart) / 2;
                        break;
                    case 'R':
                        colStart = colStart + (colEnd - colStart) / 2 + 1;
                        break;
                }
                i++;
            }
            
            return rowStart * 8 + colStart;
        }
    }

    public string? Url { get; }
    public string? Title { get; }
    public string? Input { get; set; }
} 