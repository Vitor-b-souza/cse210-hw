using System;
using System.Collections.Generic;

public class ReflectingActivity : Activity
{
    private List<string> _prompts;
    private List<string> _questions;

    public ReflectingActivity() 
        : base("Reflecting Activity",
               "This activity will help you reflect on times when you were strong.") 
    {
        _prompts = new List<string>
        {
            "Think of a time when you overcame a challenge.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you learned something important."
        };

        _questions = new List<string>
        {
            "Why was this experience meaningful to you?",
            "What did you learn from it?",
            "How can you apply this lesson in your life?",
            "How did this experience help you grow?"
        };
    }

    private string GetRandomPrompt()
    {
        var rand = new Random();
        return _prompts[rand.Next(_prompts.Count)];
    }

    private string GetRandomQuestion()
    {
        var rand = new Random();
        return _questions[rand.Next(_questions.Count)];
    }

    public void Run()
    {
        DisplayStartingMessage();

        Console.WriteLine("Consider the following prompt:");
        Console.WriteLine($"--- {GetRandomPrompt()} ---");
        Console.WriteLine("\nWhen you have something in mind, press Enter to continue...");
        Console.ReadLine();

        Console.WriteLine("Now reflect on these questions:");
        
        int timeLeft = _duration;
        while (timeLeft > 0)
        {
            string q = GetRandomQuestion();
            Console.Write($"> {q} ");
            ShowSpinner(5);
            Console.WriteLine();

            timeLeft -= 5;
        }

        DisplayEndingMessage();
    }
}
