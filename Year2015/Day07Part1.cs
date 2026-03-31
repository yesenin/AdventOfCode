using System.Text.RegularExpressions;
using Common;
using Serilog;

namespace AdventOfCode.Year2015;

public class Day07Part1 : BaseProblemWithInput, IProblemWithLogger
{
    public required string Wire { get; set; } = "a";

    protected override long GetAnswerInner()
    {
        var lines = Input!.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        
        var stack = new Dictionary<string, long>();

        foreach (var line in lines)
        {
            var parts = ParseLine(line);
            if (parts != null)
            {
                var leftExpr = ParseLeft(parts.Value.left);
                leftExpr?.Eval(stack, parts.Value.right);
                Logger?.Debug($"Evaluating {parts.Value.right} = {leftExpr?.GetType().Name}");
            }
        }

        return stack[Wire];
    }

    public static (string left, string right)? ParseLine(string line)
    {
        var regex = new Regex(@"(.+)\s->\s(.+)");
        var match = regex.Match(line);
        if (match.Success)
        {
            return (match.Groups[1].Value, match.Groups[2].Value);
        }
        return null;
    }

    public static Expr? ParseLeft(string left)
    {
        var tokens = left.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        if (tokens.Length == 1)
        {
            if (int.TryParse(tokens[0], out var i))
            {
                return new NumeralExpr(i);
            }
            return new MoveExpr(tokens[0]);
        }

        if (tokens.Length == 2)
        {
            if (tokens[0] == "NOT")
            {
                return new NotExpr(tokens[1]);
            }
        }

        if (tokens.Length == 3)
        {
            if (tokens[1] == "AND")
            {
                return new AndExpr(tokens[0], tokens[2]);
            }
            if (tokens[1] == "OR")
            {
                return new OrExpr(tokens[0], tokens[2]);
            }
            if (tokens[1] == "LSHIFT")
            {
                return new LShiftExpr(tokens[0], int.Parse(tokens[2]));
            }
            if (tokens[1] == "RSHIFT")
            {
                return new RShiftExpr(tokens[0], int.Parse(tokens[2]));
            }
        }
        return null;
    }

    public abstract record Expr
    {
        public abstract void Eval(Dictionary<string, long> stack, string dest);
    }

    public record NumeralExpr(int Value) : Expr
    {
        public override void Eval(Dictionary<string, long> stack, string dest)
        {
            stack.TryAdd(dest, 1);
            stack[dest] = Value;
        }
    }
    
    public record MoveExpr(string Adress) : Expr
    {
        public override void Eval(Dictionary<string, long> stack, string dest)
        {
            stack.TryAdd(dest, 1);
            stack.TryAdd(Adress, 1);
            stack[dest] = stack[Adress];
        }
    }
    
    public record NotExpr(string Address) : Expr
    {
        public override void Eval(Dictionary<string, long> stack, string dest)
        {
            stack.TryAdd(dest, 1);
            stack.TryAdd(Address, 1);
            stack[dest] = ~stack[Address];
            if (stack[dest] < 0)
            {
                // stack[dest] += 65535 + 1;
            }
        }
    }
    public record AndExpr(string Address1, string Address2) : Expr
    {
        public override void Eval(Dictionary<string, long> stack, string dest)
        {
            stack.TryAdd(dest, 1);
            stack.TryAdd(Address1, 1);
            stack.TryAdd(Address2, 1);
            stack[dest] = stack[Address1] & stack[Address2];
        }
    }
    public record OrExpr(string Address1, string Address2) : Expr
    {
        public override void Eval(Dictionary<string, long> stack, string dest)
        {
            stack.TryAdd(dest, 1);
            stack.TryAdd(Address1, 1);
            stack.TryAdd(Address2, 1);
            stack[dest] = stack[Address1] | stack[Address2];
        }
    }
    public record LShiftExpr(string Address, int Value) : Expr
    {
        public override void Eval(Dictionary<string, long> stack, string dest)
        {
            stack.TryAdd(dest, 1);
            stack.TryAdd(Address, 1);
            stack[dest] = stack[Address] << Value;
            if (stack[dest] < 0)
            {
                // stack[dest] += 65535;
            }
        }
    }
    public record RShiftExpr(string Address, int Value) : Expr
    {
        public override void Eval(Dictionary<string, long> stack, string dest)
        {
            stack.TryAdd(dest, 1);
            stack.TryAdd(Address, 1);
            stack[dest] = stack[Address] >> Value;
            if (stack[dest] < 1)
            {
                // stack[dest] += 65535;
            }
        }
    }
    
    
    public override string Title => "Handy Haversacks";
    public override string Url => "https://adventofcode.com/2015/day/7";
    public ILogger? Logger { get; set; }
}
