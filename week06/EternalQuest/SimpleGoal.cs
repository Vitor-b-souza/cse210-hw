using System;

public class SimpleGoal : Goal
{
    public SimpleGoal(string name, string description, int points)
        : base(name, description, points)
    {
    }

    public override string GetStringRepresentation()
    {
        return $"Simple|{Name}|{Description}|{Points}|{IsComplete}";
    }

    public override string GetStatus()
    {
        return IsComplete ? "[X]" : "[ ]";
    }
}
