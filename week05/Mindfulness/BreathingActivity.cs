using System;
using System.Threading;

public class BreathingActivity : Activity
{
    public BreathingActivity() 
        : base("Breathing Activity", 
               "This activity will help you relax by guiding your breathing slowly.") {}

    public void Run()
    {
        DisplayStartingMessage();

        int timeLeft = _duration;

        while (timeLeft > 0)
        {
            Console.Write("\nBreathe in... ");
            ShowCountdown(4);
            timeLeft -= 4;
            if (timeLeft <= 0) break;

            Console.Write("\nBreathe out... ");
            ShowCountdown(6);
            timeLeft -= 6;
            Console.WriteLine();
        }

        DisplayEndingMessage();
    }
}
