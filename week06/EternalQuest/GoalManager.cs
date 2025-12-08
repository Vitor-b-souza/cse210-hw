using System;
using System.IO;
using System.Collections.Generic;

public class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score;

    public void Start()
    {
        bool running = true;
        while (running)
        {
            Console.WriteLine($"Score: {_score}");
            Console.WriteLine("1. Create Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Save Goals");
            Console.WriteLine("4. Load Goals");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Quit");
            string choice = Console.ReadLine();

            if (choice == "1") CreateGoal();
            else if (choice == "2") ListGoals();
            else if (choice == "3") SaveGoals();
            else if (choice == "4") LoadGoals();
            else if (choice == "5") RecordEvent();
            else if (choice == "6") running = false;
        }
    }

    private void CreateGoal()
    {
        Console.WriteLine("1. Simple");
        Console.WriteLine("2. Eternal");
        Console.WriteLine("3. Checklist");
        string type = Console.ReadLine();

        Console.Write("Name: ");
        string name = Console.ReadLine();

        Console.Write("Description: ");
        string description = Console.ReadLine();

        Console.Write("Points: ");
        int points = int.Parse(Console.ReadLine());

        if (type == "1")
        {
            _goals.Add(new SimpleGoal(name, description, points));
        }
        else if (type == "2")
        {
            _goals.Add(new EternalGoal(name, description, points));
        }
        else if (type == "3")
        {
            Console.Write("Target count: ");
            int target = int.Parse(Console.ReadLine());

            Console.Write("Bonus: ");
            int bonus = int.Parse(Console.ReadLine());

            _goals.Add(new ChecklistGoal(name, description, points, target, bonus));
        }
    }

    private void ListGoals()
    {
        int i = 1;
        foreach (Goal g in _goals)
        {
            Console.WriteLine($"{i}. {g.GetStatus()} {g.Name}");
            i++;
        }
    }

    private void RecordEvent()
    {
        ListGoals();
        Console.Write("Choose goal: ");
        int index = int.Parse(Console.ReadLine()) - 1;
        if (index >= 0 && index < _goals.Count)
        {
            int gained = _goals[index].RecordEvent();
            _score += gained;
        }
    }

    private void SaveGoals()
    {
        Console.Write("File name: ");
        string file = Console.ReadLine();

        using (StreamWriter sw = new StreamWriter(file))
        {
            sw.WriteLine(_score);
            foreach (Goal g in _goals)
            {
                sw.WriteLine(g.GetStringRepresentation());
            }
        }
    }

    private void LoadGoals()
    {
        Console.Write("File name: ");
        string file = Console.ReadLine();

        _goals.Clear();

        string[] lines = File.ReadAllLines(file);
        _score = int.Parse(lines[0]);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] p = lines[i].Split("|");
            if (p[0] == "Simple")
            {
                SimpleGoal g = new SimpleGoal(p[1], p[2], int.Parse(p[3]));
                if (p[4] == "True") g.RecordEvent();
                _goals.Add(g);
            }
            else if (p[0] == "Eternal")
            {
                EternalGoal g = new EternalGoal(p[1], p[2], int.Parse(p[3]));
                _goals.Add(g);
            }
            else if (p[0] == "Checklist")
            {
                ChecklistGoal g = new ChecklistGoal(p[1], p[2], int.Parse(p[3]), int.Parse(p[5]), int.Parse(p[6]));
                int count = int.Parse(p[4]);
                for (int c = 0; c < count; c++) g.RecordEvent();
                _goals.Add(g);
            }
        }
    }
}
