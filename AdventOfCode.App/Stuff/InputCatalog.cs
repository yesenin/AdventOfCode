using System.Text.Json;

namespace AdventOfCode.App.Stuff;

internal sealed class InputCatalog
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly RunnerConfig _config;
    private readonly Dictionary<int, Dictionary<int, Dictionary<int, InputMappingEntry>>> _entries;

    private InputCatalog(RunnerConfig config, Dictionary<int, Dictionary<int, Dictionary<int, InputMappingEntry>>> entries)
    {
        _config = config;
        _entries = entries;
    }

    public static InputCatalog Load(string configPath, string mappingPath)
    {
        if (!File.Exists(configPath))
        {
            throw new FileNotFoundException($"Runner config file '{configPath}' was not found.", configPath);
        }

        if (!File.Exists(mappingPath))
        {
            throw new FileNotFoundException($"Input mapping file '{mappingPath}' was not found.", mappingPath);
        }

        var config = JsonSerializer.Deserialize<RunnerConfig>(File.ReadAllText(configPath), JsonOptions)
            ?? throw new InvalidOperationException($"Could not read runner config from '{configPath}'.");

        var mapping = JsonSerializer.Deserialize<InputMappingFile>(File.ReadAllText(mappingPath), JsonOptions)
            ?? throw new InvalidOperationException($"Could not read input mapping from '{mappingPath}'.");

        var entries = mapping.Years.ToDictionary(
            year => year.Year,
            year => year.Days.ToDictionary(
                day => day.Day,
                day => day.Parts.ToDictionary(part => part.Part, part => part)));

        return new InputCatalog(config, entries);
    }

    public ResolvedInput Resolve(int year, int day, int part, InputKind? inputKind, string? inputPath)
    {
        if (!string.IsNullOrWhiteSpace(inputPath))
        {
            var physicalPath = ResolvePhysicalPath(inputPath);
            return new ResolvedInput(inputPath, physicalPath, InputKind.Custom);
        }

        if (!_entries.TryGetValue(year, out var yearEntry))
        {
            throw new InvalidOperationException($"No input mapping exists for year {year}.");
        }

        var resolvedKind = inputKind ?? ParseInputKind(_config.DefaultInputKind);
        if (!yearEntry.TryGetValue(day, out var dayEntry))
        {
            throw new InvalidOperationException($"No input mapping exists for year {year}, day {day}.");
        }

        if (!dayEntry.TryGetValue(part, out var entry))
        {
            throw new InvalidOperationException($"No input mapping exists for year {year}, day {day}, part {part}.");
        }

        var relativePath = resolvedKind switch
        {
            InputKind.Sample => entry.Sample,
            InputKind.Task => entry.Task,
            InputKind.Custom => throw new InvalidOperationException("Custom input kind is only valid with --input."),
            _ => throw new InvalidOperationException($"Unsupported input kind '{resolvedKind}'.")
        };

        if (string.IsNullOrWhiteSpace(relativePath))
        {
            throw new InvalidOperationException(
                $"No {resolvedKind.ToString().ToLowerInvariant()} input is configured for year {year}, day {day}, part {part}.");
        }

        return new ResolvedInput(relativePath, ResolvePhysicalPath(relativePath), resolvedKind);
    }

    private static InputKind ParseInputKind(string value) =>
        value.Trim().ToLowerInvariant() switch
        {
            "sample" => InputKind.Sample,
            "task" => InputKind.Task,
            _ => throw new InvalidOperationException($"Invalid default input kind '{value}'.")
        };

    private static string ResolvePhysicalPath(string inputPath) =>
        Path.IsPathRooted(inputPath)
            ? inputPath
            : Path.Combine(AppContext.BaseDirectory, inputPath);
}
