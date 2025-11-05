using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter your grade percentage: ");
        int grade = int.Parse(Console.ReadLine());

        string letter = "";
        string sign = "";

        if (grade >= 90)
            letter = "A";
        else if (grade >= 80)
            letter = "B";
        else if (grade >= 70)
            letter = "C";
        else if (grade >= 60)
            letter = "D";
        else
            letter = "F";

        if (grade >= 70)
            Console.WriteLine("Congratulations, you passed!");
        else
            Console.WriteLine("Keep trying, you'll do better next time!");

        int lastDigit = grade % 10;
        if (letter != "F" && letter != "A")
        {
            if (lastDigit >= 7)
                sign = "+";
            else if (lastDigit < 3)
                sign = "-";
        }
        else if (letter == "A" && grade < 94)
        {
            if (lastDigit < 3)
                sign = "-";
        }

        Console.WriteLine($"Your grade is: {letter}{sign}");
    }
}
