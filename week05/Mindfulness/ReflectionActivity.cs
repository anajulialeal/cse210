using System;
using System.Collections.Generic;
using System.Threading;

public class ReflectionActivity : Activity
{
    // Private member variables
    private List<string> _prompts;
    private List<string> _questions;
    private Random _random;

    public ReflectionActivity() : base(
        "Reflection",
        "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life."
    )
    {
        _prompts = new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };
        
        _questions = new List<string>
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
        
        _random = new Random();
    }
    
    public override void Run()
    {
        DisplayStartingMessage();
        
        // Show random prompt
        string prompt = _prompts[_random.Next(_prompts.Count)];
        Console.WriteLine("\nConsider the following prompt:\n");
        Console.WriteLine($"--- {prompt} ---\n");
        Console.WriteLine("When you have something in mind, press enter to continue.");
        Console.ReadLine();
        
        Console.WriteLine("\nNow ponder on each of the following questions as they relate to this experience.");
        Console.Write("You may begin in: ");
        ShowCountDown(5);
        Console.Clear();
        
        // Continue showing random questions until duration is reached
        DateTime endTime = DateTime.Now.AddSeconds(Duration);
        int questionIndex = 0;
        
        while (DateTime.Now < endTime)
        {
            if (questionIndex >= _questions.Count)
            {
                questionIndex = 0;
            }
            
            Console.Write($"\n> {_questions[questionIndex]} ");
            ShowSpinner(8);
            questionIndex++;
        }
        
        DisplayEndingMessage();
    }
}