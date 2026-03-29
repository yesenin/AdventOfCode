namespace Common;

public static class SurfaceHelper
{
    public static int GetSurfaceArea(int l, int w, int h)
    {
        if (l <= 0 || w <= 0 || h <= 0)
        {
            throw new ArgumentException("All sides must be positive");
        }
        
        return 2 * (l * w + w * h + h * l);
    }

    public static int GetMinimalSideSquare(int l, int w, int h)
    {
        if (l <= 0 || w <= 0 || h <= 0)
        {
            throw new ArgumentException("All sides must be positive");
        }
        
        var squares = new [] { l * w, w * h, h * l};
        return squares.Min();
    }
}
