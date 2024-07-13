using System;

public abstract class BaseGoal
{
    private string _name;
    private string _description;
    private int _points;

    public string Name { get => _name; set => _name = value; }
    public string Description { get => _description; set => _description = value; }
    public int Points { get => _points; set => _points = value; }

    protected BaseGoal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    public abstract int RecordEvent();
    public abstract bool IsComplete();
    public abstract string GetGoalType();
    public abstract string GetDetails();
}
