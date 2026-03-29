namespace AdventOfCode.App.Stuff;

internal sealed class DayMappingEntry
{
    public int Day { get; set; }

    public List<InputMappingEntry> Parts { get; set; } = [];
}
