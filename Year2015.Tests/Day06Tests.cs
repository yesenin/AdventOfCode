namespace AdventOfCode.Year2015.Tests;

public class Day06Tests
{
    [Theory]
    [InlineData("turn on 0,0 through 0,0", "1")]
    [InlineData("turn on 0,0 through 999,999", "1000000")]
    [InlineData("toggle 0,0 through 999,0", "1000")]
    [InlineData("turn on 499,499 through 500,500", "4")]
    public void Day06_Part1_Sample1_Test(string input, string expectedResult)
    {
        var sut = new Day06Part1
        {
            Input = string.Join('\n', input)
        };
        
        var result = sut.GetAnswer();
        
        Assert.Equal(expectedResult, result);
    }
}
