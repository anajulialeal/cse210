using System;
using System.Collections.Generic;

public class PromptGenerator
{
    public List<string> _prompts = new List<string>()
{
    "What was the best moment of your day?",
    "What challenge did you overcome today?",
    "What made you smile today?",
    "What is something new you learned today?",
    "What are you grateful for today?"
};

    public string GetRandomPrompt()
    {
        Random random = new Random();
        int index = random.Next(_prompts.Count);
        return _prompts[index];
    }
}