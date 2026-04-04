using System;
using System.Collections.Generic;
using System.Threading;

public abstract class Activity
{
    private string _name;
    private string _description;
    private int _duration;

    protected string Name => _name;
    protected string Description => _description;
    protected int Duration => _duration;
    protected void SetDuration(int duration) => _duration = duration;

    public Activity(string name, string description)
    {
        _name = name;
        _description = description;
    }

    public void DisplayStartingMessage()
    {
        Console.Clear();
        Console.WriteLine($"Welcome to the {_name} Activity.\n");
        Console.WriteLine(_description);
        Console.WriteLine();
        
        Console.Write("How long, in seconds, would you like for your session? ");
        int durationInput = int.Parse(Console.ReadLine());
        _duration = durationInput;
        
        Console.Clear();
        Console.WriteLine("Get ready...");
        ShowSpinner(3);
    }

    public void DisplayEndingMessage()
    {
        Console.WriteLine();
        Console.WriteLine("Well done!!");
        ShowSpinner(2);
        Console.WriteLine();
        Console.WriteLine($"You have completed another {_duration} seconds of the {_name} Activity.");
        ShowSpinner(4);
        Console.Clear();
    }

    public void ShowSpinner(int seconds)
    {
        List<string> spinnerChars = new List<string> { "|", "/", "-", "\\" };
        DateTime endTime = DateTime.Now.AddSeconds(seconds);
        int i = 0;
        
        while (DateTime.Now < endTime)
        {
            Console.Write(spinnerChars[i % spinnerChars.Count]);
            Thread.Sleep(200);
            Console.Write("\b \b");  // Backspace effect
            i++;
        }
    }

    public void ShowCountDown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write(i);
            Thread.Sleep(1000);
            Console.Write("\b \b");  // Backspace effect
        }
    }

    public abstract void Run();
}