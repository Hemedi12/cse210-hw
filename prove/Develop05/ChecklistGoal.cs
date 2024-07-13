public class ChecklistGoal : BaseGoal
{
    private int _requiredCount;
    private int _currentCount;
    private int _bonus;

    public int RequiredCount { get => _requiredCount; set => _requiredCount = value; }
    public int CurrentCount { get => _currentCount; private set => _currentCount = value; }
    public int Bonus { get => _bonus; set => _bonus = value; }

    public ChecklistGoal(string name, string description, int points, int requiredCount, int bonus) 
        : base(name, description, points)
    {
        _requiredCount = requiredCount;
        _bonus = bonus;
        _currentCount = 0;
    }

    public void SetCurrentCount(int currentCount)
    {
        _currentCount = currentCount;
    }

    public override int RecordEvent()
    {
        if (CurrentCount < RequiredCount)
        {
            CurrentCount++;
            if (CurrentCount == RequiredCount)
            {
                return Points + Bonus;
            }
            return Points;
        }
        return 0;
    }

    public override bool IsComplete() => CurrentCount >= RequiredCount;

    public override string GetGoalType() => "ChecklistGoal";

    public override string GetDetails() => $"{Name},{Description},{Points},{CurrentCount},{RequiredCount},{Bonus}";
}
