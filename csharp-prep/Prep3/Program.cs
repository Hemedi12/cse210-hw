using System;
using System.Globalization;

class Program
{
    static void Main(string[] args)
    {
      
        Console.WriteLine("Welcome to Guess My Number!");

        
        do
        {
          
            Random random = new Random();
            int magicNumber = random.Next(1, 101);

            
            int guessCount = 0;

        
            int guess;
            do
            {
                guessCount++;

                
                Console.WriteLine("What is your guess? ");
                guess = int.Parse(Console.ReadLine());

            
                if (guess < magicNumber)
                {
                    Console.WriteLine("Higher!");
                }
                else if (guess > magicNumber)
                {
                    Console.WriteLine("Lower!");
                }

            } while (guess != magicNumber);

            
            Console.WriteLine("You guessed it in {0} guesses!", guessCount);

            
            Console.WriteLine("Play again? (yes/no)");
        } while (Console.ReadLine().ToLower() == "yes");

    
        Console.WriteLine("Thanks for playing!");
    }}
