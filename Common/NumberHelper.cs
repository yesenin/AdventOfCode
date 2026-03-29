namespace Common;

public static class NumberHelper
{
    /// <summary>
    /// Алгоритм Евклида для нахождения НОД
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static int GetGcd(int a, int b)
    {
        var max = Math.Max(a, b);
        var min = Math.Min(a, b);
        while (min != 0)
        {
            int temp = min;
            min = max % min;
            max = temp;
        }
        return max;
    }

    public static int GetGcdRec(int a, int b)
    {
        if (a == 0)
        {
            return b;
        }

        return GetGcdRec(b % a, a);
    }
    
    public static bool IsRelativelyPrime(int a, int b)
    {
        return GetGcd(a, b) == 1;
    }
    
    public static bool IsPrime(int number)
    {
        if (number < 2) return false;
        if (number is 2 or 3 or 5) return true;
        if (number % 2 == 0) return false;
        if (number % 3 == 0) return false;
        if (number % 5 == 0) return false;
        var limit = (int)Math.Sqrt(number);
        for (var i = 3; i <= limit; i += 2)
        {
            if (number % i == 0) return false;
        }
        return true;
    }

    public static int GetNextPrime(int n)
    {
        while (!IsPrime(n))
        {
            n++;
        }

        return n;
    }
}