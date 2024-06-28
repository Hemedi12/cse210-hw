using System;
using System.Collections.Generic;
using System.IO;

// Entry class to represent a journal entry
class Entry {
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string Date { get; set; }

    // Constructor
    public Entry(string prompt, string response, string date) {
        Prompt = prompt;
        Response = response;
        Date = date;
    }

    // Method to format the entry for display
    public override string ToString() {
        return $"Date: {Date}\nPrompt: {Prompt}\nResponse: {Response}\n";
    }
}

// Journal class to manage entries
class Journal {
    private List<Entry> entries;

    // Constructor
    public Journal() {
        entries = new List<Entry>();
    }

    // Method to add a new entry
    public void AddEntry(Entry entry) {
        entries.Add(entry);
    }

    // Method to display all entries
    public void DisplayEntries() {
        foreach (var entry in entries) {
            Console.WriteLine(entry);
        }
    }

    // Method to save the journal to a file
    public void SaveToFile(string filename) {
        using (StreamWriter sw = new StreamWriter(filename)) {
            foreach (var entry in entries) {
                sw.WriteLine($"{entry.Date}|{entry.Prompt}|{entry.Response}");
            }
        }
    }

    // Method to load the journal from a file
    public void LoadFromFile(string filename) {
        entries.Clear();
        using (StreamReader sr = new StreamReader(filename)) {
            string line;
            while ((line = sr.ReadLine()) != null) {
                string[] parts = line.Split('|');
                if (parts.Length == 3) {
                    entries.Add(new Entry(parts[1], parts[2], parts[0]));
                }
            }
        }
    }

    // Method to edit an existing entry
    public void EditEntry(int index, Entry editedEntry) {
        if (index >= 0 && index < entries.Count) {
            entries[index] = editedEntry;
            Console.WriteLine("Entry edited successfully.");
        } else {
            Console.WriteLine("Invalid entry index.");
        }
    }

    // Method to delete an entry
    public void DeleteEntry(int index) {
        if (index >= 0 && index < entries.Count) {
            entries.RemoveAt(index);
            Console.WriteLine("Entry deleted successfully.");
        } else {
            Console.WriteLine("Invalid entry index.");
        }
    }

    // Method to get an entry by index
    public Entry GetEntry(int index) {
        if (index >= 0 && index < entries.Count) {
            return entries[index];
        } else {
            Console.WriteLine("Invalid entry index.");
            return null;
        }
    }

    // Method to count the number of entries
    public int Count() {
        return entries.Count;
    }
}

// Program class
class Program {
    static void Main(string[] args) {
        Journal journal = new Journal();
        bool running = true;
        while (running) {
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Edit an entry");
            Console.WriteLine("6. Delete an entry");
            Console.WriteLine("7. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();
            switch (choice) {
                case "1":
                    // Write a new entry
                    Console.WriteLine("Writing a new entry...");
                    // Generate a random prompt (you can implement this)
                    string prompt = GenerateRandomPrompt();
                    Console.WriteLine("Prompt: " + prompt);
                    Console.Write("Enter your response: ");
                    string response = Console.ReadLine();
                    // Get the current date
                    string date = DateTime.Now.ToString("yyyy-MM-dd");
                    // Create a new entry
                    Entry newEntry = new Entry(prompt, response, date);
                    // Add the entry to the journal
                    journal.AddEntry(newEntry);
                    break;
                case "2":
                    // Display the journal
                    Console.WriteLine("Displaying the journal...");
                    journal.DisplayEntries();
                    break;
                case "3":
                    // Save the journal to a file
                    Console.Write("Enter filename to save: ");
                    string saveFilename = Console.ReadLine();
                    journal.SaveToFile(saveFilename);
                    Console.WriteLine("Journal saved to file.");
                    break;
                case "4":
                    // Load the journal from a file
                    Console.Write("Enter filename to load: ");
                    string loadFilename = Console.ReadLine();
                    journal.LoadFromFile(loadFilename);
                    Console.WriteLine("Journal loaded from file.");
                    break;
                case "5":
                    // Edit an entry
                    Console.Write("Enter the index of the entry to edit: ");
                    int editIndex;
                    if (int.TryParse(Console.ReadLine(), out editIndex)) {
                        Entry entryToEdit = journal.GetEntry(editIndex);
                        if (entryToEdit != null) {
                            Console.WriteLine("Editing entry:");
                            Console.WriteLine(entryToEdit);

                            // Get new prompt and response
                            Console.Write("Enter new prompt: ");
                            string newPrompt = Console.ReadLine();
                            Console.Write("Enter new response: ");
                            string newResponse = Console.ReadLine();

                            // Create a new entry with edited content
                            Entry editedEntry = new Entry(newPrompt, newResponse, entryToEdit.Date);

                            // Edit the entry in the journal
                            journal.EditEntry(editIndex, editedEntry);
                        }
                    } else {
                        Console.WriteLine("Invalid input for entry index.");
                    }
                    break;
                case "6":
                    // Delete an entry
                    Console.Write("Enter the index of the entry to delete: ");
                    int deleteIndex;
                    if (int.TryParse(Console.ReadLine(), out deleteIndex)) {
                        if (deleteIndex >= 0 && deleteIndex < journal.Count()) {
                            journal.DeleteEntry(deleteIndex);
                        } else {
                            Console.WriteLine("Invalid entry index.");
                        }
                    } else {
                        Console.WriteLine("Invalid input for entry index.");
                    }
                    break;
                case "7":
                    // Exit the program
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please choose again.");
                    break;
            }
        }
    }

    // Method to generate a random prompt
    static string GenerateRandomPrompt() {
        // Implement your logic to select a random prompt from a list
        // For simplicity, you can return a static prompt for now
        List<string> prompts = new List<string> {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?"
        };
        Random rnd = new Random();
        int index = rnd.Next(prompts.Count);
        return prompts[index];
    }
}