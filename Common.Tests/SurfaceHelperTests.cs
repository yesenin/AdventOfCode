namespace Common.Tests;

public class SurfaceHelperTests
{
    [Fact]
    public void ShouldReturnSquareOfAllSides()
    {
        Assert.Equal(52, SurfaceHelper.GetSurfaceArea(2, 3, 4));
        Assert.Equal(42, SurfaceHelper.GetSurfaceArea(1, 1, 10));
    }
    
    [Fact]
    public void ShouldThrowExceptionForNegativeSides()
    {
        Assert.Throws<ArgumentException>(() => SurfaceHelper.GetSurfaceArea(-1, 2, 3));
        Assert.Throws<ArgumentException>(() => SurfaceHelper.GetSurfaceArea(1, -2, 3));
        Assert.Throws<ArgumentException>(() => SurfaceHelper.GetSurfaceArea(1, 2, -3));
    }
    
    [Fact]
    public void ShouldReturnMinimalSideSquare()
    {
        Assert.Equal(6, SurfaceHelper.GetMinimalSideSquare(2, 3, 4));
        Assert.Equal(1, SurfaceHelper.GetMinimalSideSquare(1, 1, 10));
    }
}
