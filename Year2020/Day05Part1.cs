using Common;

namespace AdventOfCode.Year2020;

public class Day05Part1 : IProblemWithInput
{
    public string GetAnswer()
    {
        ulong answer = 0;
        var lines = Input.Split('\n', StringSplitOptions.TrimEntries);
        
        foreach (var line in lines)
        {
            var a = (ulong)GetId(line);
            if (a > answer) answer = a;
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

    public string Url { get; } = "https://adventofcode.com/2020/day/5";
    public string Title { get; } = "Binary Boarding. Part 1";
    public string? Input { get; set; }
} 
