namespace AdventOfCode.Year2015;

public static class Day01Common
{
    public static int ChangeFloor(int floor, char c)
    {
        return c switch
        {
            '(' => floor + 1,
            ')' => floor - 1,
            _ => floor
        };
    }
}
