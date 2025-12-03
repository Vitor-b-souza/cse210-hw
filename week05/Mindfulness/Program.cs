using System;

class Program
{
    static void Main(string[] args)
    {
        int choice = 0;

        while (choice != 4)
        {
            Console.Clear();
            Console.WriteLine("Mindfulness Program");
            Console.WriteLine("1. Start Breathing Activity");
            Console.WriteLine("2. Start Reflecting Activity");
            Console.WriteLine("3. Start Listing Activity");
            Console.WriteLine("4. Quit");
            Console.Write("\nSelect an option: ");

            choice = int.Parse(Console.ReadLine());

            if (choice == 1)
            {
                var activity = new BreathingActivity();
                activity.Run();
            }
            else if (choice == 2)
            {
                var activity = new ReflectingActivity();
                activity.Run();
            }
            else if (choice == 3)
            {
                var activity = new ListingActivity();
                activity.Run();
            }
        }
    }
}
