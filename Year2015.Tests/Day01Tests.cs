namespace AdventOfCode.Year2015.Tests;

public class Day01Tests
{
    [Theory]
    [InlineData("(())", "0")]
    [InlineData("()()", "0")]
    [InlineData("(((", "3")]
    [InlineData("(()(()(", "3")]
    [InlineData("))(((((", "3")]
    [InlineData("())", "-1")]
    [InlineData("))(", "-1")]
    [InlineData(")))", "-3")]
    [InlineData(")())())", "-3")]
    public void Day1_Part1_Samples_Test(string input, string expectedResult)
    {
        var sut = new Day01Part1
        {
            Input = input
        };
        var result = sut.GetAnswer();
        Assert.Equal(expectedResult, result);
    }
    
    [Theory]
    [InlineData(")", "1")]
    [InlineData("()())", "5")]
    public void Day1_Part2_Samples_Test(string input, string expectedResult)
    {
        var sut = new Day01Part2
        {
            Input = input
        };
        var result = sut.GetAnswer();
        Assert.Equal(expectedResult, result);
    }
    
    [Theory]
    [InlineData("(()", "-1")]
    public void Day1_Part2_Custom_Test(string input, string expectedResult)
    {
        var sut = new Day01Part2
        {
            Input = input
        };
        var result = sut.GetAnswer();
        Assert.Equal(expectedResult, result);
    }
}