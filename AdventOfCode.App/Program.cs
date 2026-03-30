using System.Diagnostics;
using System.Reflection;
using AdventOfCode.App.Stuff;
using Common;
using Serilog;
using Serilog.Events;

namespace AdventOfCode.App;

internal static class Program
{
    private const string RunnerConfigFile = "runner-config.json";
    private const string InputMappingFile = "input-mapping.json";

    // Update these defaults when you want to debug a different puzzle from the IDE.
    private static readonly RunnerOptions DebugOptions = new(
        Year: 2015,
        Day: 7,
        Part: 1,
        RequestedInputKind: InputKind.Task,
        Verbose: true,
        InputPath: null);

    public static void Main(string[] args)
    {
        var options = RunnerOptions.Parse(args, DebugOptions);

        Log.Logger = CreateLogger(options.Verbose);

        try
        {
            Log.Information("Starting Advent of Code runner for year {Year}, day {Day}, part {Part}.", options.Year, options.Day, options.Part);
            Log.Debug("Verbose console logging: {Verbose}.", options.Verbose);

            if (options.ShowHelp)
            {
                WriteFrame("Usage", [
                    "AdventOfCode.App --year 2019 --day 1 --part 1",
                    "Optional: --kind sample|task",
                    "Optional: --input Inputs/2019/day01_task.txt",
                    "Optional: --verbose",
                    "Debug defaults live in Program.cs"
                ]);
                return;
            }

            var catalog = InputCatalog.Load(
                ResolveInputPath(RunnerConfigFile),
                ResolveInputPath(InputMappingFile));
            Log.Debug("Loaded input catalog from config and mapping files.");

            var resolvedInput = catalog.Resolve(
                options.Year,
                options.Day,
                options.Part,
                options.RequestedInputKind,
                options.InputPath);
            Log.Information("Resolved input {Kind} to {InputPath}.", resolvedInput.Kind, resolvedInput.RelativePath);

            var yearAssembly = YearAssemblyLoader.Load(options.Year);
            Log.Debug("Loaded year assembly {Assembly}.", yearAssembly.GetName().Name);

            WriteFrame("Advent of Code Runner", [
                $"Year: {options.Year}",
                $"Day: {options.Day}",
                $"Part: {options.Part}",
                $"Kind: {resolvedInput.Kind}",
                $"Input: {resolvedInput.RelativePath}"
            ]);

            if (!File.Exists(resolvedInput.FullPath))
            {
                Log.Error("Input file was not found at {InputPath}.", resolvedInput.FullPath);
                WriteFrame("Input missing", [
                    $"File: {resolvedInput.FullPath}",
                    "Status: not found"
                ]);
                return;
            }

            Log.Debug("Reading input file {InputPath}.", resolvedInput.FullPath);
            var input = File.ReadAllText(resolvedInput.FullPath);
            var problem = CreateProblem(yearAssembly, options.Year, options.Day, options.Part);
            InjectLogger(problem);

            problem.Input = input;

            var stopWatch = Stopwatch.StartNew();
            var actual = problem.GetAnswer();
            stopWatch.Stop();

            Log.Information("Completed {ProblemType} in {ElapsedMs} ms.", problem.GetType().FullName, stopWatch.ElapsedMilliseconds);
            WriteFrame("Result", [
                $"Answer: {actual}",
                $"Time: {stopWatch.ElapsedMilliseconds} ms"
            ]);
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static ILogger CreateLogger(bool verbose)
    {
        var logsDirectory = Path.Combine(AppContext.BaseDirectory, "logs");
        Directory.CreateDirectory(logsDirectory);

        var logFilePath = Path.Combine(logsDirectory, "advent-of-code-.log");
        var consoleMinimumLevel = verbose ? LogEventLevel.Verbose : LogEventLevel.Information;

        return new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.Console(
                restrictedToMinimumLevel: consoleMinimumLevel,
                outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
            .WriteTo.File(
                logFilePath,
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 14,
                outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();
    }

    private static void WriteFrame(string title, IEnumerable<string> lines)
    {
        var content = lines.ToArray();
        var innerWidth = Math.Max(title.Length, content.Select(line => line.Length).DefaultIfEmpty(0).Max());
        var border = "+" + new string('-', innerWidth + 2) + "+";

        Console.WriteLine(border);
        Console.WriteLine($"| {title.PadRight(innerWidth)} |");
        Console.WriteLine(border);

        foreach (var line in content)
        {
            Console.WriteLine($"| {line.PadRight(innerWidth)} |");
        }

        Console.WriteLine(border);
        Console.WriteLine();
    }

    private static string ResolveInputPath(string inputPath) =>
        Path.IsPathRooted(inputPath)
            ? inputPath
            : Path.Combine(AppContext.BaseDirectory, inputPath);

    private static IProblemWithInput CreateProblem(Assembly assembly, int year, int day, int part)
    {
        var namespaceName = $"AdventOfCode.Year{year}";
        var candidates = new[]
        {
            $"Day{day}Part{part}",
            $"Day{day:D2}Part{part}"
        }.Distinct(StringComparer.Ordinal).ToArray();

        var problemType = assembly
            .GetTypes()
            .FirstOrDefault(type =>
                type is { IsClass: true, IsAbstract: false } &&
                type.Namespace == namespaceName &&
                candidates.Contains(type.Name, StringComparer.Ordinal));

        if (problemType is null)
        {
            throw new InvalidOperationException(
                $"Could not find a problem class for year {year}, day {day}, part {part}.");
        }

        if (!typeof(IProblemWithInput).IsAssignableFrom(problemType))
        {
            throw new InvalidOperationException(
                $"Problem class '{problemType.FullName}' does not implement {nameof(IProblemWithInput)}.");
        }

        return (IProblemWithInput)Activator.CreateInstance(problemType)!;
    }

    private static void InjectLogger(IProblemWithInput problem)
    {
        var loggerProperty = problem.GetType().GetProperty("Logger", BindingFlags.Public | BindingFlags.Instance);
        if (loggerProperty is null || !loggerProperty.CanWrite || !typeof(ILogger).IsAssignableFrom(loggerProperty.PropertyType))
        {
            return;
        }

        loggerProperty.SetValue(problem, Log.ForContext(problem.GetType()));
        Log.Debug("Injected logger into {ProblemType}.", problem.GetType().FullName);
    }
}
