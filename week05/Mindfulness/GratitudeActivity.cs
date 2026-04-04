using System;
using System.Collections.Generic;
using System.Threading;

public class GratitudeActivity : Activity
{
    // Private member variables
    private List<string> _prompts;
    private Random _random;

    public GratitudeActivity() : base(
        "Gratitude",
        "This activity will help you cultivate gratitude by reflecting on the positive aspects of your life. Research shows that practicing gratitude improves well-being and happiness."
    )
    {
        _prompts = new List<string>
        {
            "What are three things you are grateful for today?",
            "Who is someone that made you smile recently?",
            "What is a skill or talent you are thankful for?",
            "What is a challenge that made you stronger?",
            "What is something beautiful you saw today?",
            "Who is a person that has inspired you?",
            "What is a memory you cherish?"
        };
        
        _random = new Random();
    }
    
    public override void Run()
    {
        DisplayStartingMessage();
        
        // Show random gratitude prompt
        string prompt = _prompts[_random.Next(_prompts.Count)];
        Console.WriteLine($"\n{prompt}\n");
        
        Console.Write("Take a moment to reflect... ");
        ShowSpinner(3);
        
        Console.WriteLine("\n\nNow, write your thoughts below (press Enter after each item):\n");
        
        DateTime endTime = DateTime.Now.AddSeconds(Duration);
        List<string> items = new List<string>();
        
        while (DateTime.Now < endTime)
        {
            Console.Write("> ");
            string item = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(item))
            {
                items.Add(item);
            }
        }
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\nYou recorded {items.Count} things you're grateful for!");
        Console.ForegroundColor = ConsoleColor.Cyan;
        
        DisplayEndingMessage();
    }
}