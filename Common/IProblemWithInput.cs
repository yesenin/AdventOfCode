namespace Common;

public interface IProblemWithInput : IProblem
{
    string? Input { get; set; }
}
