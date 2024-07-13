using System;

class Program
{
    static void Main()
    {
        GoalManager manager = new GoalManager();

        // Interact with the program
        bool running = true;
        while (running)
        {
            Console.WriteLine("\nEternal Quest Program");
            Console.WriteLine("1. Display Goals");
            Console.WriteLine("2. Record Event");
            Console.WriteLine("3. Add Goal");
            Console.WriteLine("4. Save Goals");
            Console.WriteLine("5. Load Goals");
            Console.WriteLine("6. Exit");
            Console.Write("Select an option: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    manager.DisplayGoals();
                    Console.WriteLine($"Total Score: {manager.TotalScore}");
                    Console.WriteLine($"Level: {manager.Level}");
                    break;
                case "2":
                    Console.Write("Enter the name of the goal to record: ");
                    string goalName = Console.ReadLine();
                    manager.RecordEvent(goalName);
                    break;
                case "3":
                    AddGoal(manager);
                    break;
                case "4":
                    Console.Write("Enter filename to save goals: ");
                    string saveFile = Console.ReadLine();
                    manager.SaveGoals(saveFile);
                    break;
                case "5":
                    Console.Write("Enter filename to load goals: ");
                    string loadFile = Console.ReadLine();
                    manager.LoadGoals(loadFile);
                    break;
                case "6":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    static void AddGoal(GoalManager manager)
    {
        Console.WriteLine("Select goal type:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.Write("Enter choice: ");
        string choice = Console.ReadLine();

        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();
        Console.Write("Enter goal description: ");
        string description = Console.ReadLine();
        Console.Write("Enter goal points: ");
        int points = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case "1":
                manager.AddGoal(new SimpleGoal(name, description, points));
                break;
            case "2":
                manager.AddGoal(new EternalGoal(name, description, points));
                break;
            case "3":
                Console.Write("Enter required count: ");
                int count = int.Parse(Console.ReadLine());
                Console.Write("Enter bonus points: ");
                int bonus = int.Parse(Console.ReadLine());
                manager.AddGoal(new ChecklistGoal(name, description, points, count, bonus));
                break;
            default:
                Console.WriteLine("Invalid choice. Goal not added.");
                break;
        }
    }
}

// Exceeding Requirements:
// 1. Gamification Enhancements: Added a leveling system where users level up based on their total score.
//    - The user levels up for every 1000 points earned.
//    - A congratulatory message is displayed when the user levels up.
// 2. Progress Tracking: Displayed the level and added a congratulatory message when leveling up.
