namespace Common;
using Serilog;

public interface IProblemWithLogger
{
    ILogger? Logger { get; set; }
}
