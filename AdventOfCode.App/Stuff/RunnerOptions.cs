namespace AdventOfCode.App.Stuff;

internal sealed record RunnerOptions(
    int Year,
    int Day,
    int Part,
    InputKind? RequestedInputKind,
    string? InputPath,
    bool Verbose = false,
    bool ShowHelp = false)
{
    public static RunnerOptions Parse(string[] args, RunnerOptions defaults)
    {
        var parsed = ParseArguments(args);

        if (HasHelpFlag(parsed))
        {
            return defaults with { ShowHelp = true };
        }

        var year = GetInt(parsed, ["--year", "-y", "positional:0"], defaults.Year);
        var day = GetInt(parsed, ["--day", "-d", "positional:1"], defaults.Day);
        var part = GetInt(parsed, ["--part", "-p", "positional:2"], defaults.Part);
        var inputKind = GetInputKind(parsed, defaults.RequestedInputKind);
        var inputPath = GetString(parsed, ["--input", "-i"], defaults.InputPath);
        var verbose = GetBool(parsed, ["--verbose", "-verbose", "-v"], defaults.Verbose);

        return new RunnerOptions(year, day, part, inputKind, inputPath, verbose);
    }

    private static Dictionary<string, string> ParseArguments(string[] args)
    {
        var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        var positional = new List<string>();

        for (var i = 0; i < args.Length; i++)
        {
            var arg = args[i];

            if (arg is "-h" or "--help")
            {
                result["--help"] = "true";
                continue;
            }

            if (arg is "-verbose" or "-v")
            {
                if (i + 1 < args.Length && bool.TryParse(args[i + 1], out var verboseValue))
                {
                    result["--verbose"] = verboseValue.ToString();
                    i++;
                }
                else
                {
                    result["--verbose"] = "true";
                }
                continue;
            }

            if (arg.StartsWith("--", StringComparison.Ordinal))
            {
                var separatorIndex = arg.IndexOf('=');
                if (separatorIndex > 0)
                {
                    result[arg[..separatorIndex]] = arg[(separatorIndex + 1)..];
                    continue;
                }

                if (i + 1 < args.Length && !args[i + 1].StartsWith("-", StringComparison.Ordinal))
                {
                    result[arg] = args[++i];
                    continue;
                }

                result[arg] = "true";
                continue;
            }

            if (arg.StartsWith("-", StringComparison.Ordinal))
            {
                if (i + 1 >= args.Length)
                {
                    throw new ArgumentException($"Missing value for argument '{arg}'.");
                }

                result[arg] = args[++i];
                continue;
            }

            positional.Add(arg);
        }

        if (positional.Count > 0)
        {
            result["positional:year"] = positional[0];
        }

        if (positional.Count > 1)
        {
            result["positional:day"] = positional[1];
        }

        if (positional.Count > 2)
        {
            result["positional:part"] = positional[2];
        }

        return result;
    }

    private static bool HasHelpFlag(IReadOnlyDictionary<string, string> args) =>
        args.ContainsKey("--help");

    private static InputKind? GetInputKind(IReadOnlyDictionary<string, string> args, InputKind? fallback)
    {
        var value = GetString(args, new[] { "--kind", "-k", "--input-kind" }, null);
        if (value is null)
        {
            return fallback;
        }

        return value.Trim().ToLowerInvariant() switch
        {
            "sample" => InputKind.Sample,
            "task" => InputKind.Task,
            _ => throw new ArgumentException($"Invalid input kind '{value}'. Use 'sample' or 'task'.")
        };
    }

    private static bool GetBool(IReadOnlyDictionary<string, string> args, string[] keys, bool fallback)
    {
        var value = GetString(args, keys, null);
        if (value is null)
        {
            return fallback;
        }

        if (!bool.TryParse(value, out var parsed))
        {
            throw new ArgumentException($"Invalid boolean value '{value}'.");
        }

        return parsed;
    }

    private static int GetInt(IReadOnlyDictionary<string, string> args, string[] keys, int fallback)
    {
        var value = GetString(args, keys, null);

        if (value is null)
        {
            return fallback;
        }

        if (!int.TryParse(value, out var parsed))
        {
            throw new ArgumentException($"Invalid numeric value '{value}'.");
        }

        return parsed;
    }

    private static string? GetString(IReadOnlyDictionary<string, string> args, string[] keys, string? fallback)
    {
        foreach (var key in keys)
        {
            if (args.TryGetValue(key, out var value))
            {
                return value;
            }
        }

        return fallback;
    }
}
