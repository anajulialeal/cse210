// EXCEEDING REQUIREMENTS:
// The program was improved by using a random number of words to hide each time the user presses Enter.
// Instead of always hiding the same number of words, it now hides between 2 and 4 words randomly.
// This makes the memorization process more dynamic and a little more challenging.
// Additionally, the program avoids hiding words that are already hidden, improving efficiency.
using System;

class Program
{
    static void Main(string[] args)
    {
        Reference reference = new Reference("John", 3, 16);

        Scripture scripture = new Scripture(
            reference,
            "For God so loved the world that he gave his only begotten Son"
        );

        while (true)
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());

            if (scripture.IsCompletelyHidden())
            {
                break;
            }

            Console.WriteLine("\nPress Enter to continue or type 'quit':");
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
            {
                break;
            }

           Random rand = new Random();
            int wordsToHide = rand.Next(2, 5);
            scripture.HideRandomWords(wordsToHide);
        }
    }
}