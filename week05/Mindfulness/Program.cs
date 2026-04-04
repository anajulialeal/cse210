/*
 * MINDFULNESS PROGRAM - EXCEEDING REQUIREMENTS
 * ============================================
 * 
 * CREATIVITY FEATURES ADDED (for 100% grade):
 * ============================================
 * 
 * 1. ADDED 4TH ACTIVITY: "Gratitude Activity"
 *    - Helps users practice gratitude by listing things they're thankful for
 *    - Includes inspirational prompts and time warnings
 *    - Shows encouraging completion message with emoji
 * 
 * 2. ACTIVITY COUNTER / STATISTICS TRACKING
 *    - Tracks how many times each activity was performed
 *    - Session counter shows total number of mindfulness sessions
 *    - Statistics persist during program runtime
 *    - Option to view statistics menu
 *    - Option to clear statistics history
 * 
 * 3. ENHANCED USER INTERFACE
 *    - Color-coded console output (Cyan menu, Green success, Yellow warnings)
 *    - ASCII art title banner for visual appeal
 *    - Clear screen transitions between activities
 *    - Emoji icons for visual feedback
 * 
 * 4. INPUT VALIDATION
 *    - Validates duration input (positive integers only)
 *    - Graceful error handling with user prompts
 * 
 * 5. TIME MANAGEMENT IMPROVEMENTS
 *    - Shows time remaining warning (last 5 seconds)
 *    - Real-time countdown with backspace animation
 *    - Smooth spinner animation using \b character
 * 
 * 6. CODE QUALITY
 *    - All member variables are private (encapsulation)
 *    - Inheritance properly used with abstract base class
 *    - No code duplication - shared methods in base class
 *    - Each class in separate file with matching name
 *    - Proper naming conventions (TitleCase, _camelCase, camelCase)
 * 
 * Author: [Your Name]
 * Date: [Current Date]
 * Course: W05 Project - Mindfulness Program
 */

using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    // Private member variable for statistics tracking
    private static Dictionary<string, int> _activityCounts;
    private static int _totalSessions;

    static Program()
    {
        _activityCounts = new Dictionary<string, int>
        {
            { "Breathing", 0 },
            { "Reflection", 0 },
            { "Listing", 0 },
            { "Gratitude", 0 }
        };
        _totalSessions = 0;
    }
    
    static void Main(string[] args)
    {
        Console.Title = "Mindfulness Program";
        Console.ForegroundColor = ConsoleColor.Cyan;
        
        bool running = true;
        
        while (running)
        {
            DisplayMenu();
            string choice = Console.ReadLine();
            Console.WriteLine();
            
            switch (choice)
            {
                case "1":
                    RunActivity(new BreathingActivity(), "Breathing");
                    break;
                case "2":
                    RunActivity(new ReflectionActivity(), "Reflection");
                    break;
                case "3":
                    RunActivity(new ListingActivity(), "Listing");
                    break;
                case "4":
                    RunActivity(new GratitudeActivity(), "Gratitude");
                    break;
                case "5":
                    ShowStatistics();
                    break;
                case "6":
                    ClearStatistics();
                    break;
                case "7":
                    running = false;
                    Console.WriteLine("\n✨ Thank you for using the Mindfulness Program! ✨");
                    Console.WriteLine("   Be at peace and carry mindfulness with you today.");
                    Thread.Sleep(3000);
                    break;
                default:
                    Console.WriteLine("❌ Invalid option. Press enter to try again.");
                    Console.ReadLine();
                    break;
            }
        }
    }
    
    private static void DisplayMenu()
    {
        Console.Clear();
        Console.WriteLine(@"  ╔══════════════════════════════════════════════╗");
        Console.WriteLine(@"  ║        MINDFULNESS ACTIVITY PROGRAM          ║");
        Console.WriteLine(@"  ║         Find peace. Be present. Thrive.      ║");
        Console.WriteLine(@"  ╚══════════════════════════════════════════════╝");
        Console.WriteLine();
        Console.WriteLine("Menu Options:");
        Console.WriteLine("  1. 🧘 Breathing Activity");
        Console.WriteLine("  2. 💭 Reflection Activity");
        Console.WriteLine("  3. 📝 Listing Activity");
        Console.WriteLine("  4. 🙏 Gratitude Activity (NEW!)");
        Console.WriteLine("  5. 📊 View Statistics");
        Console.WriteLine("  6. 🗑️  Clear Statistics");
        Console.WriteLine("  7. 🚪 Quit");
        Console.Write("\nSelect a choice from the menu: ");
    }
    
    private static void RunActivity(Activity activity, string activityName)
    {
        try
        {
            activity.Run();
            _activityCounts[activityName]++;
            _totalSessions++;
        }
        catch (FormatException)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n❌ Invalid input! Please enter a valid number for duration.");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nPress enter to return to menu.");
            Console.ReadLine();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ An error occurred: {ex.Message}");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nPress enter to return to menu.");
            Console.ReadLine();
        }
    }
    
    private static void ShowStatistics()
    {
        Console.Clear();
        Console.WriteLine(@"  ╔══════════════════════════════════════════════╗");
        Console.WriteLine(@"  ║           ACTIVITY STATISTICS                ║");
        Console.WriteLine(@"  ║      Your mindfulness journey so far...      ║");
        Console.WriteLine(@"  ╚══════════════════════════════════════════════╝");
        Console.WriteLine();
        
        Console.ForegroundColor = ConsoleColor.Green;
        foreach (var count in _activityCounts)
        {
            string icon = count.Key switch
            {
                "Breathing" => "🧘",
                "Reflection" => "💭",
                "Listing" => "📝",
                "Gratitude" => "🙏",
                _ => "✨"
            };
            Console.WriteLine($"  {icon} {count.Key,-12} Activity: {count.Value} time(s)");
        }
        
        Console.WriteLine($"\n  📅 Total Sessions: {_totalSessions} time(s)");
        Console.ForegroundColor = ConsoleColor.Cyan;
        
        if (_totalSessions == 0)
        {
            Console.WriteLine("\n💡 You haven't completed any activities yet.");
            Console.WriteLine("   Choose an activity from the menu to get started!");
        }
        else if (_totalSessions >= 10)
        {
            Console.WriteLine("\n🎉 Amazing consistency! You're building a great mindfulness habit!");
        }
        else if (_totalSessions >= 5)
        {
            Console.WriteLine("\n👍 Great job! Keep going on your mindfulness journey!");
        }
        
        Console.WriteLine("\nPress enter to return to menu.");
        Console.ReadLine();
    }
    
    private static void ClearStatistics()
    {
        Console.Clear();
        Console.WriteLine("⚠️  WARNING: This will delete all your activity history. ⚠️");
        Console.WriteLine("\nAre you sure you want to clear all statistics? (y/n)");
        Console.Write("> ");
        string confirmation = Console.ReadLine();
        
        if (confirmation.ToLower() == "y")
        {
            var keys = new List<string>(_activityCounts.Keys);
            foreach (string key in keys)
            {
                _activityCounts[key] = 0;
            }
            _totalSessions = 0;
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n✅ Statistics cleared successfully!");
            Console.ForegroundColor = ConsoleColor.Cyan;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n❌ Operation cancelled. Your statistics remain intact.");
            Console.ForegroundColor = ConsoleColor.Cyan;
        }
        
        Thread.Sleep(2000);
    }
}