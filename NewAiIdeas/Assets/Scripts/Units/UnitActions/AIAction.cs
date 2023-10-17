public abstract class AIAction
{
    public abstract float Evaluate(AIContext context);
    public abstract void Execute(AIContext context);
}