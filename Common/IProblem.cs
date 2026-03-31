namespace Common;

public interface IProblem
{
    long GetAnswer();
    
    string Url { get; }
    
    string Title { get; }
}
