using System;
using System.Collections.Generic;
using System.Threading;

public class ListingActivity : Activity
{
    // Private member variables
    private List<string> _prompts;
    private Random _random;

    public ListingActivity() : base(
        "Listing",
        "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area."
    )
    {
        _prompts = new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };
        
        _random = new Random();
    }
    
    public override void Run()
    {
        DisplayStartingMessage();
        
        // Show random prompt
        string prompt = _prompts[_random.Next(_prompts.Count)];
        Console.WriteLine("\nList as many things as you can to the following prompt:\n");
        Console.WriteLine($"--- {prompt} ---\n");
        
        Console.Write("You may begin in: ");
        ShowCountDown(5);
        Console.WriteLine();
        
        // Get items from user until duration is reached
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
        
        // Display number of items entered
        Console.WriteLine($"\nYou listed {items.Count} items!");
        
        DisplayEndingMessage();
    }
}