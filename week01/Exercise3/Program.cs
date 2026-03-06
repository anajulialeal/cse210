using System;

class Program
{
    static void Main(string[] args)
    {
        string playAgain = "yes";

        while (playAgain == "yes")
        {
            Random randomGenerator = new Random();
            int magicNumber = randomGenerator.Next(1, 101);

            int guess = 0;
            int guesses = 0;

            while (guess != magicNumber)
            {
                Console.Write("What is your guess? ");
                guess = int.Parse(Console.ReadLine());
                guesses++;

                if (guess < magicNumber)
                {
                    Console.WriteLine("Higher");
                }
                else if (guess > magicNumber)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine("You guessed it!");
                }
            }

            Console.WriteLine($"You guessed it in {guesses} guesses!");

            Console.Write("Do you want to play again? ");
            playAgain = Console.ReadLine();
        }
    }
}

/* Logica
Computador escolhe número
        ↓
Jogador chuta
        ↓
Maior ou menor?
        ↓
Continua até acertar
        ↓
"You guessed it!"
*/