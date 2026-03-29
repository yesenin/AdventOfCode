namespace AdventOfCode.App.Stuff;

internal sealed class YearMappingEntry
{
    public int Year { get; set; }

    public List<DayMappingEntry> Days { get; set; } = [];
}
