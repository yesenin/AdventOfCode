namespace Common;

public abstract class BaseProblem : IProblem
{
    public string GetAnswerWithMetric()
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        var answer = GetAnswer();
        stopwatch.Stop();
        var elapsedMs = stopwatch.ElapsedMilliseconds;
        return $"{answer} (computed in {elapsedMs} ms)";
    }

    public abstract long GetAnswer();

    public virtual string Url => "!TBD";

    public virtual string Title => "!TBD";
}
