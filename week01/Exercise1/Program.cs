using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("What is your first name? ");
         string firstName = Console.ReadLine();

        Console.Write("What is your last name? ");
        string lastName = Console.ReadLine();

        Console.WriteLine($"Your name is {lastName}, {firstName} {lastName}");


    }
}


/*

Perguntar
↓
Guardar resposta
↓
Perguntar
↓
Guardar resposta
↓
Mostrar resultado

-----------------------------------
What is your first name? Ana
What is your last name? Hilamatu

Your name is Silva, Ana Hilamatu. - "Your name is last-name, first-name, last-name" 
------------------------------------
*/