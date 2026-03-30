using System.Runtime.InteropServices;

namespace AdventOfCode.Year2015.Tests;

public class Day07Tests
{
    [Theory]
    [InlineData("d", "72")]
    [InlineData("e", "507")]
    [InlineData("f", "492")]
    [InlineData("g", "114")]
    [InlineData("h", "65412")]
    [InlineData("i", "65079")]
    [InlineData("x", "123")]
    [InlineData("y", "456")]
    public void Day07_Part1_Sample_Test(string wire, string expectedResult)
    {
        var input = """
                    123 -> x
                    456 -> y
                    x AND y -> d
                    x OR y -> e
                    x LSHIFT 2 -> f
                    y RSHIFT 2 -> g
                    NOT x -> h
                    NOT y -> i
                    """;

        var sut = new Day07Part1
        {
            Input = input,
            Wire = wire
        };
        var result = sut.GetAnswer();
        
        Assert.Equal(expectedResult, result);
    }
}
