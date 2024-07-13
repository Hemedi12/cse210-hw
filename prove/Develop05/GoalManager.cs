using System;
using System.Collections.Generic;
using System.IO;

public class GoalManager
{
    private List<BaseGoal> _goals;
    private int _totalScore;
    private int _level;

    public int TotalScore { get => _totalScore; private set => _totalScore = value; }
    public int Level { get => _level; private set => _level = value; }

    public GoalManager()
    {
        _goals = new List<BaseGoal>();
        _totalScore = 0;
        _level = 1;
    }

    public void AddGoal(BaseGoal goal)
    {
        _goals.Add(goal);
    }

    public void RecordEvent(string goalName)
    {
        bool found = false;
        foreach (var goal in _goals)
        {
            if (goal.Name.Equals(goalName, StringComparison.OrdinalIgnoreCase))
            {
                TotalScore += goal.RecordEvent();
                LevelUp();
                found = true;
                break;
            }
        }
        if (!found)
        {
            Console.WriteLine($"Goal with name '{goalName}' not found.");
        }
    }

    public void DisplayGoals()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals have been set yet.");
        }
        else
        {
            foreach (var goal in _goals)
            {
                string status = goal.IsComplete() ? "[X]" : "[ ]";
                if (goal is ChecklistGoal checklistGoal)
                {
                    Console.WriteLine($"{status} {goal.Name} [{goal.Description}] (Completed {checklistGoal.CurrentCount}/{checklistGoal.RequiredCount} times)");
                }
                else
                {
                    Console.WriteLine($"{status} [{goal.Description}] {goal.Name}");
                }
            }
        }
    }

    public void SaveGoals(string filename)
    {
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            outputFile.WriteLine(TotalScore);
            outputFile.WriteLine(Level);
            foreach (var goal in _goals)
            {
                outputFile.WriteLine($"{goal.GetGoalType()}:{goal.GetDetails()}");
            }
        }
    }

    public void LoadGoals(string filename)
    {
        if (!File.Exists(filename))
        {
            Console.WriteLine($"File '{filename}' not found.");
            return;
        }

        string[] lines = File.ReadAllLines(filename);
        TotalScore = int.Parse(lines[0]);
        Level = int.Parse(lines[1]);

        _goals.Clear();
        foreach (string line in lines[2..])
        {
            string[] parts = line.Split(':');
            string type = parts[0];
            string[] details = parts[1].Split(',');

            switch (type)
            {
                case "SimpleGoal":
                    var simpleGoal = new SimpleGoal(details[0], details[1], int.Parse(details[2]));
                    simpleGoal.SetCompleted(bool.Parse(details[3]));
                    _goals.Add(simpleGoal);
                    break;
                case "EternalGoal":
                    _goals.Add(new EternalGoal(details[0], details[1], int.Parse(details[2])));
                    break;
                case "ChecklistGoal":
                    var checklistGoal = new ChecklistGoal(details[0], details[1], int.Parse(details[2]), int.Parse(details[4]), int.Parse(details[5]));
                    checklistGoal.SetCurrentCount(int.Parse(details[3]));
                    _goals.Add(checklistGoal);
                    break;
            }
        }
    }

    private void LevelUp()
    {
        if (TotalScore >= Level * 1000)
        {
            Level++;
            Console.WriteLine($"Congratulations! You've reached Level {Level}!");
        }
    }
}
