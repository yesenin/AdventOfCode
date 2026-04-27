namespace Common;

public abstract class BaseProblemWithInput : IProblemWithInput
{
    private bool ValidateInput() => !string.IsNullOrEmpty(Input);

    protected abstract long GetAnswerInner();

    public long GetAnswer()
    {
        return !ValidateInput() ? throw new ArgumentException("Invalid input") : GetAnswerInner();
    }

    public virtual string Url => "!TBD";
    public virtual string Title => "!TBD";
    public string? Input { get; set; }
}
