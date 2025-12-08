using System;

public class ChecklistGoal : Goal
{
    private int _targetCount;
    private int _currentCount;
    private int _bonus;

    public int CurrentCount => _currentCount;

    public ChecklistGoal(string name, string description, int points, int targetCount, int bonus)
        : base(name, description, points)
    {
        _targetCount = targetCount;
        _bonus = bonus;
        _currentCount = 0;
    }

    public override int RecordEvent()
    {
        _currentCount++;
        if (_currentCount >= _targetCount)
        {
            return Points + _bonus;
        }
        return Points;
    }

    public override string GetStringRepresentation()
    {
        return $"Checklist|{Name}|{Description}|{Points}|{CurrentCount}|{_targetCount}|{_bonus}";
    }

    public override string GetStatus()
    {
        return $"{CurrentCount}/{_targetCount}";
    }
}
