public class SimpleGoal : BaseGoal
{
    private bool _completed;

    public bool Completed { get => _completed; private set => _completed = value; }

    public SimpleGoal(string name, string description, int points) : base(name, description, points)
    {
        _completed = false;
    }

    public void SetCompleted(bool completed)
    {
        _completed = completed;
    }

    public override int RecordEvent()
    {
        if (!Completed)
        {
            Completed = true;
            return Points;
        }
        return 0;
    }

    public override bool IsComplete() => Completed;

    public override string GetGoalType() => "SimpleGoal";

    public override string GetDetails() => $"{Name},{Description},{Points},{Completed}";
}
