using System;
using System.Collections.Generic;

// Main program class
public class Program
{
    static void Main(string[] args)
    {
        Scripture scripture = new Scripture(
            new Reference("Proverbs 3:5-6"), 
            "Trust in the Lord with all your heart and lean not on your own understanding; " +
            "in all your ways submit to Him, and He will make your paths straight."
        );
        
        // Main loop continues until all words are hidden
        while (!scripture.AllWordsHidden())
        {
            Console.Clear();
            Console.WriteLine(scripture);
            Console.WriteLine("Press enter to hide words or type 'quit' to exit.");
            
            string input = Console.ReadLine();
            if (input.ToLower() == "quit")
                break;

            scripture.HideWords(3); // Hides 3 words randomly
        }

        // Extra feature: Ending message when all words are hidden
        if (scripture.AllWordsHidden())
        {
            Console.WriteLine("Congratulations! You've successfully memorized the scripture.");
            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }
    }
}

// Represents a scripture passage
public class Scripture
{
    public Reference Reference { get; private set; }
    public List<Word> Words { get; private set; }
    
    public Scripture(Reference reference, string text)
    {
        this.Reference = reference;
        Words = new List<Word>();
        foreach (var word in text.Split(' '))
            Words.Add(new Word(word));
    }

    public void HideWords(int count)
    {
        Random random = new Random();
        List<int> availableIndices = Words
            .Select((word, index) => new { word, index })
            .Where(x => !x.word.IsHidden)
            .Select(x => x.index)
            .ToList();

        int wordsToHide = Math.Min(count, availableIndices.Count);
        for (int i = 0; i < wordsToHide; i++)
        {
            int randomIndex = random.Next(availableIndices.Count);
            Words[availableIndices[randomIndex]].Hide();
            availableIndices.RemoveAt(randomIndex);
        }
    }

    public bool AllWordsHidden()
    {
        return Words.All(word => word.IsHidden);
    }

    public override string ToString()
    {
        var displayText = new System.Text.StringBuilder();
        displayText.Append(Reference.ToString() + ": ");
        foreach (var word in Words)
        {
            displayText.Append(word.IsHidden ? "____ " : word.Text + " ");
        }
        return displayText.ToString();
    }
}

// Represents the reference of a scripture (e.g., Proverbs 3:5-6)
public class Reference
{
    public string Text { get; private set; }

    public Reference(string text)
    {
        this.Text = text;
    }

    public override string ToString()
    {
        return Text;
    }
}

// Represents a single word within a scripture
public class Word
{
    public string Text { get; private set; }
    public bool IsHidden { get; private set; }

    public Word(string text)
    {
        this.Text = text;
    }

    public void Hide()
    {
        IsHidden = true;
    }

    public void Show()
    {
        IsHidden = false;
    }
}

// Exceeds Core Requirements: 
// 1. Added functionality for the user to receive a congratulatory message once they have successfully memorized the scripture.
// 2. Enhanced user experience by allowing users to type 'quit' to exit early, adding flexibility and control to the memorization process.