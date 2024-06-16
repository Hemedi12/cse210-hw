using System;

class Program
{
    static void Main(string[] args)
    {
        
        Console.WriteLine("Enter your grade percentage please: ");
        double grade = double.Parse(Console.ReadLine());
 
        char letter;
        string sign = "";
        if (grade >= 90)
        {
            letter = 'A';
        }
        else if (grade >= 80)
        {
            letter = 'B';
        }
        else if (grade >= 70)
        {
            letter = 'C';
        }
        else if (grade >= 60)
        {
            letter = 'D';
        }
        else
        {
            letter = 'F';
        }

        if (grade >= 90 && grade <= 92 && grade <=80 && grade >= 82 && grade <=70 && grade >= 72 && grade <=60 && grade >= 62 )
        {
            sign = "-";
        }
        else if (grade >= 87 && grade <= 89 && grade <=77 && grade >= 79 && grade <=67 && grade >= 69)
        {
            sign = "+";
        }
        else if (grade >= 87 && grade <= 89)
        {
            sign = "+";
        }
        

       
        Console.WriteLine($"Your letter grade is: {letter} {sign}");

      
        if (grade >= 70)
        {
            Console.WriteLine("Congratulations! You passed the course.");
        }
        else
        {
            Console.WriteLine("Keep studying! You can do it next time.");
        }

    } 
}

    