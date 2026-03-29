namespace Common;

public interface IProblem
{
    string GetAnswer();
    
    string Url { get; }
    
    string Title { get; }
}
