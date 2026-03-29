namespace AdventOfCode.Year2015;

public static class Day03Common
{
    public static Point ShiftPoint(Point point, char direction)
    {
        return direction switch
        {
            '^' => point with { Y = point.Y + 1 },
            'v' => point with { Y = point.Y - 1 },
            '>' => point with { X = point.X + 1 },
            '<' => point with { X = point.X - 1 },
            _ => point
        };
    }
}

public record Point(int X, int Y);
