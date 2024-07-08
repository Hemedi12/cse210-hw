using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace MindfulnessProgram
{
    class Program
    {
        // Dictionary to track activity logs
        static Dictionary<string, int> activityLogs = new Dictionary<string, int>();

        static void Main(string[] args)
        {
            LoadLogs();  // Load activity logs from a file on startup
            Console.WriteLine("Welcome to the Mindfulness Program!");

            while (true)
            {
                Console.WriteLine("Please choose an activity:");
                Console.WriteLine("1. Breathing Activity");
                Console.WriteLine("2. Reflection Activity");
                Console.WriteLine("3. Listing Activity");
                Console.WriteLine("4. Gratitude Activity");
                Console.WriteLine("5. Exit");

                string choice = Console.ReadLine();

                if (choice == "5") break;

                Activity activity;
                switch (choice)
                {
                    case "1":
                        activity = new BreathingActivity();
                        break;
                    case "2":
                        activity = new ReflectionActivity();
                        break;
                    case "3":
                        activity = new ListingActivity();
                        break;
                    case "4":
                        activity = new GratitudeActivity();  // New Gratitude Activity
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        continue;
                }

                activity.Start();
                LogActivity(activity.GetType().Name);  // Log the activity
            }

            SaveLogs();  // Save activity logs to a file before exiting
        }

        // Log activity by incrementing the count
        static void LogActivity(string activityName)
        {
            if (!activityLogs.ContainsKey(activityName))
            {
                activityLogs[activityName] = 0;
            }
            activityLogs[activityName]++;
        }

        // Load activity logs from a file
        static void LoadLogs()
        {
            if (File.Exists("activity_logs.txt"))
            {
                var lines = File.ReadAllLines("activity_logs.txt");
                foreach (var line in lines)
                {
                    var parts = line.Split(':');
                    if (parts.Length == 2)
                    {
                        activityLogs[parts[0]] = int.Parse(parts[1]);
                    }
                }
            }
        }

        // Save activity logs to a file
        static void SaveLogs()
        {
            var lines = activityLogs.Select(kvp => $"{kvp.Key}:{kvp.Value}");
            File.WriteAllLines("activity_logs.txt", lines);
        }
    }

    abstract class Activity
    {
        // Protected member variable for duration, accessible to derived classes
        protected int Duration { get; set; }

        public void Start()
        {
            ShowStartingMessage();
            SetDuration();
            PrepareToBegin();
            PerformActivity();
            ShowEndingMessage();
        }

        protected virtual void ShowStartingMessage()
        {
            Console.WriteLine($"\nStarting {GetType().Name}...");
            Console.WriteLine(GetDescription());
        }

        protected abstract string GetDescription();

        protected void SetDuration()
        {
            Console.WriteLine("Enter the duration of the activity in seconds:");
            Duration = int.Parse(Console.ReadLine());
        }

        protected void PrepareToBegin()
        {
            Console.WriteLine("Prepare to begin...");
            ShowSpinner(3);
        }

        protected abstract void PerformActivity();

        protected void ShowEndingMessage()
        {
            Console.WriteLine("Good job! You have completed the activity.");
            ShowSpinner(3);
            Console.WriteLine($"You completed the {GetType().Name} for {Duration} seconds.\n");
        }

        // Common method to show a spinner for pausing
        protected void ShowSpinner(int seconds)
        {
            for (int i = 0; i < seconds; i++)
            {
                Console.Write(".");
                Thread.Sleep(1000);
            }
            Console.WriteLine();
        }

        // Common method to show a countdown timer
        protected void ShowCountdown(int seconds)
        {
            for (int i = seconds; i > 0; i--)
            {
                Console.Write(i + " ");
                Thread.Sleep(1000);
            }
            Console.WriteLine();
        }
    }

    class BreathingActivity : Activity
    {
        protected override string GetDescription()
        {
            return "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.";
        }

        protected override void PerformActivity()
        {
            int interval = 4; // seconds for breathing in and out
            for (int i = 0; i < Duration; i += interval * 2)
            {
                Console.WriteLine("Breathe in...");
                ShowBreathingAnimation(interval);  // Enhanced breathing animation
                Console.WriteLine("Breathe out...");
                ShowBreathingAnimation(interval);
            }
        }

        // Enhanced breathing animation showing a visual growing bar
        private void ShowBreathingAnimation(int seconds)
        {
            for (int i = 0; i < seconds; i++)
            {
                Console.Write("[");
                for (int j = 0; j < i + 1; j++)
                {
                    Console.Write("=");
                }
                Console.Write(">");
                Console.WriteLine(new string(' ', seconds - i - 1) + "]");
                Thread.Sleep(1000);
            }
            Console.WriteLine();
        }
    }

    class ReflectionActivity : Activity
    {
        private static readonly List<string> Prompts = new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        private static readonly List<string> Questions = new List<string>
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };

        protected override string GetDescription()
        {
            return "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.";
        }

        protected override void PerformActivity()
        {
            Random rand = new Random();
            string prompt = Prompts[rand.Next(Prompts.Count)];
            Console.WriteLine(prompt);
            ShowSpinner(3);

            var usedQuestions = new HashSet<string>();
            int elapsed = 0;
            while (elapsed < Duration)
            {
                string question;
                do
                {
                    question = Questions[rand.Next(Questions.Count)];
                } while (usedQuestions.Contains(question));
                usedQuestions.Add(question);

                Console.WriteLine(question);
                ShowSpinner(5);
                elapsed += 5;

                if (usedQuestions.Count == Questions.Count)
                {
                    usedQuestions.Clear();  // Ensure unique prompts within a session
                }
            }
        }
    }

    class ListingActivity : Activity
    {
        private static readonly List<string> Prompts = new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        protected override string GetDescription()
        {
            return "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.";
        }

        protected override void PerformActivity()
        {
            Random rand = new Random();
            string prompt = Prompts[rand.Next(Prompts.Count)];
            Console.WriteLine(prompt);
            ShowSpinner(5);

            List<string> items = new List<string>();
            int elapsed = 0;
            while (elapsed < Duration)
            {
                string item = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(item))
                {
                    items.Add(item);
                }
                elapsed += 1;
            }
            Console.WriteLine($"You listed {items.Count} items.");
        }
    }

    // New GratitudeActivity class
    class GratitudeActivity : Activity
    {
        protected override string GetDescription()
        {
            return "This activity will help you focus on the things you are grateful for in your life. Take a moment to write down as many things as you can think of.";
        }

        protected override void PerformActivity()
        {
            Console.WriteLine("List the things you are grateful for:");
            List<string> items = new List<string>();
            int elapsed = 0;
            while (elapsed < Duration)
            {
                string item = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(item))
                {
                    items.Add(item);
                }
                elapsed += 1;
            }
            Console.WriteLine($"You listed {items.Count} things you are grateful for.");
        }
    }
}

/* 
   Exceeding Requirements:
   1. Added a new activity called "Gratitude Activity" which allows users to list things they are grateful for.
   2. Implemented activity logging to track how many times each activity is performed.
   3. Ensured that prompts in the ReflectionActivity are unique within a session.
   4. Saved and loaded activity logs from a file to persist data across sessions.
   5. Enhanced the breathing animation with a visual growing bar.
*/