public class EternalGoal : BaseGoal
{
    public EternalGoal(string name, string description, int points) : base(name, description, points) { }

    public override int RecordEvent()
    {
        return Points;
    }

    public override bool IsComplete() => false;

    public override string GetGoalType() => "EternalGoal";

    public override string GetDetails() => $"{Name},{Description},{Points}";
}
