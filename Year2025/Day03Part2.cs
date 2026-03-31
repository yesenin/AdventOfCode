using System.Text;
using Common;

namespace AdventOfCode.Year2025;

public sealed class Day03Part2 : IProblemWithInput
{
    public long GetAnswer()
    {
        long answer = 0;
        var lines = Input.Split('\n');
        foreach (var line in lines)
        {
            answer += MaxSubsequenceNumber(line.Trim(), 12);
        }
        return answer;
    }

    private long FindMax(string line)
    {
        long result = 0;
        var numbers = line.Select(x => int.Parse(x.ToString())).ToArray();
        foreach (var arrangement in GetCombinations(numbers.Length, 12))
        {
            var localSum = new StringBuilder();
            foreach (var i in arrangement.OrderBy(x => x))
            {
                localSum.Append(numbers[i]);
            }

            var sum = long.Parse(localSum.ToString());

            if (sum > result)
            {
                result = sum;
            }
        }

        return result;
    }
    
    IEnumerable<int[]> GetArrangements(int n, int k)
    {
        var used = new bool[n];
        var buffer = new int[k];

        IEnumerable<int[]> Backtrack(int depth)
        {
            if (depth == k)
            {
                // copy, otherwise it will change later
                yield return (int[])buffer.Clone();
                yield break;
            }

            for (int i = 0; i < n; i++)
            {
                if (used[i]) continue;

                used[i] = true;
                buffer[depth] = i;
                foreach (var result in Backtrack(depth + 1))
                    yield return result;
                used[i] = false;
            }
        }

        return Backtrack(0);
    }
    
    IEnumerable<int[]> GetCombinations(int n, int k)
    {
        var buffer = new int[k];

        IEnumerable<int[]> Backtrack(int depth, int start)
        {
            if (depth == k)
            {
                yield return (int[])buffer.Clone();
                yield break;
            }

            for (int i = start; i < n; i++)
            {
                buffer[depth] = i;
                foreach (var result in Backtrack(depth + 1, i + 1))
                    yield return result;
            }
        }

        return Backtrack(0, 0);
    }

    private long MaxSubsequenceNumber(string digits, int k)
    {
        if (digits == null) 
            throw new ArgumentNullException(nameof(digits));

        int n = digits.Length;
        if (k <= 0 || k > n)
            throw new ArgumentOutOfRangeException(nameof(k));

        // how many digits can be removed?
        int toRemove = n - k;
        // there will construct the best number
        var stack = new List<char>(n);

        foreach (char c in digits)
        {
            // while can remove and there is smaller digit at the end — remove it
            while (toRemove > 0 && stack.Count > 0 && stack[^1] < c)
            {
                stack.RemoveAt(stack.Count - 1);
                toRemove--;
            }

            stack.Add(c);
        }

        // if string was non-increasing (like 987654...), couldn't remove enough — cut from the end
        if (toRemove > 0)
        {
            stack.RemoveRange(stack.Count - toRemove, toRemove);
        }

        // guaranteed length ≥ k, take first k
        if (stack.Count > k)
            stack.RemoveRange(k, stack.Count - k);

        return long.Parse(new string(stack.ToArray()));
    }

    
    public string Url => "!TBD";
    public string Title => "!TBD";
    public string? Input { get; set; }
}
