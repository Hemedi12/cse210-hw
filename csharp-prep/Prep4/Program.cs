using System;
using System.Collections.Generic;
using System.Linq;


class Program
{
    static void Main(string[] args)
    {
       // List to store numbers
        List<double> numbers = new List<double>();

        // Get user input
        double number;
        do
        {
            Console.WriteLine("Enter a number (type 0 to finish): ");
            number = double.Parse(Console.ReadLine());
            if (number != 0)
            {
                numbers.Add(number);
            }
        } while (number != 0);

        // Core requirements
        double sum = numbers.Sum();
        double average = numbers.Average();
        double max = numbers.Max();
        double min = numbers.Min();

        // Display results
        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The average is: {average}");
        Console.WriteLine($"The largest number is: {max}");
        Console.WriteLine($"The smallest positive number is: {min}");
        // Stretch challenges
        // double smallestPositive = numbers.Where(n => n > 0).OrderBy(n => n).FirstOrDefault();
        // Console.WriteLine("The smallest positive number is: {0}", smallestPositive);

        // Sort the list
        numbers.Sort();
        Console.WriteLine("The sorted list is:");
        foreach (double num in numbers)
        {
            Console.WriteLine(num);
        }
    }
}