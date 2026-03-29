namespace AdventOfCode.Year2015.Tests;

public class Day03Tests
{
    [Theory]
    [InlineData(">", "2")]
    [InlineData("^>v<", "4")]
    [InlineData("^v^v^v^v^v", "2")]
    public void Day3_Part1_Samples_Test(string input, string expectedResult)
    {
        var sut = new Day03Part1
        {
            Input = input
        };
        var result = sut.GetAnswer();
        Assert.Equal(expectedResult, result);
    }
}
