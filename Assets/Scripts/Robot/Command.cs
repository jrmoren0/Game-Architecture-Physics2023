

public abstract class Command 
{
    public abstract void Execute();

    public abstract void Undo();

    public abstract bool _isComplete { get; }


}
