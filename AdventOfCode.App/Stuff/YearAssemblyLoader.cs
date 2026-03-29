using System.Reflection;
using System.Runtime.Loader;
using Serilog;

namespace AdventOfCode.App.Stuff;

internal static class YearAssemblyLoader
{
    public static Assembly Load(int year)
    {
        var assemblyName = $"Year{year}";
        Log.Debug("Resolving year assembly {AssemblyName}.", assemblyName);

        var loadedAssembly = AssemblyLoadContext.Default.Assemblies
            .FirstOrDefault(assembly => string.Equals(assembly.GetName().Name, assemblyName, StringComparison.Ordinal));

        if (loadedAssembly is not null)
        {
            Log.Debug("Using already loaded assembly {AssemblyName}.", loadedAssembly.GetName().Name);
            return loadedAssembly;
        }

        var assemblyPath = Path.Combine(AppContext.BaseDirectory, $"{assemblyName}.dll");
        if (!File.Exists(assemblyPath))
        {
            Log.Error("Year assembly {AssemblyPath} was not found.", assemblyPath);
            throw new FileNotFoundException($"Year assembly '{assemblyPath}' was not found.", assemblyPath);
        }

        Log.Information("Loading year assembly from {AssemblyPath}.", assemblyPath);
        return AssemblyLoadContext.Default.LoadFromAssemblyPath(assemblyPath);
    }
}
