using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EternalQuest
{
    public class GoalManager
    {
        private List<Goal> _goals;
        private int _score;
        private int _level;
        private Dictionary<string, int> _achievements;
        private Random _random;

        // Creative additions:
        // 1. Level system - players level up every 1000 points
        // 2. Achievement badges for milestones
        // 3. Random encouragement messages
        // 4. Daily streak tracking (bonus for recording goals on consecutive days)
        private int _dailyStreak;
        private DateTime _lastRecordDate;

        public GoalManager()
        {
            _goals = new List<Goal>();
            _score = 0;
            _level = 1;
            _achievements = new Dictionary<string, int>();
            _random = new Random();
            _dailyStreak = 0;
            _lastRecordDate = DateTime.MinValue;
        }

        public void Start()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                DisplayPlayerInfo();
                Console.WriteLine("\nMenu Options:");
                Console.WriteLine("1. Create New Goal");
                Console.WriteLine("2. List Goals");
                Console.WriteLine("3. Save Goals");
                Console.WriteLine("4. Load Goals");
                Console.WriteLine("5. Record Event");
                Console.WriteLine("6. View Achievements");
                Console.WriteLine("7. Quit");
                Console.Write("Select a choice from the menu: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateGoal();
                        break;
                    case "2":
                        ListGoalDetails();
                        break;
                    case "3":
                        SaveGoals();
                        break;
                    case "4":
                        LoadGoals();
                        break;
                    case "5":
                        RecordEvent();
                        break;
                    case "6":
                        ShowAchievements();
                        break;
                    case "7":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        public void DisplayPlayerInfo()
        {
            Console.WriteLine($"╔════════════════════════════════════════╗");
            Console.WriteLine($"║           ETERNAL QUEST               ║");
            Console.WriteLine($"╠════════════════════════════════════════╣");
            Console.WriteLine($"║ Score: {_score,8}  Level: {_level,3}  Streak: {_dailyStreak,3} ║");
            Console.WriteLine($"╚════════════════════════════════════════╝");
        }

        public void ListGoalNames()
        {
            for (int i = 0; i < _goals.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_goals[i].GetName()}");
            }
        }

        public void ListGoalDetails()
        {
            Console.WriteLine("\nYour Goals:");
            if (_goals.Count == 0)
            {
                Console.WriteLine("No goals created yet. Create a goal using option 1.");
            }
            else
            {
                for (int i = 0; i < _goals.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
                }
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        public void CreateGoal()
        {
            Console.Clear();
            Console.WriteLine("The types of goals are:");
            Console.WriteLine("1. Simple Goal");
            Console.WriteLine("2. Eternal Goal");
            Console.WriteLine("3. Checklist Goal");
            Console.Write("Which type of goal would you like to create? ");
            
            string type = Console.ReadLine();
            
            Console.Write("What is the name of your goal? ");
            string name = Console.ReadLine();
            
            Console.Write("What is a short description of it? ");
            string description = Console.ReadLine();
            
            Console.Write("What is the amount of points associated with this goal? ");
            int points = int.Parse(Console.ReadLine());

            switch (type)
            {
                case "1":
                    _goals.Add(new SimpleGoal(name, description, points));
                    break;
                case "2":
                    _goals.Add(new EternalGoal(name, description, points));
                    break;
                case "3":
                    Console.Write("How many times does this goal need to be accomplished for a bonus? ");
                    int target = int.Parse(Console.ReadLine());
                    Console.Write($"What is the bonus for accomplishing it {target} times? ");
                    int bonus = int.Parse(Console.ReadLine());
                    _goals.Add(new ChecklistGoal(name, description, points, target, bonus));
                    break;
                default:
                    Console.WriteLine("Invalid goal type.");
                    break;
            }
            
            Console.WriteLine($"\nGoal '{name}' created successfully!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public void RecordEvent()
        {
            Console.Clear();
            if (_goals.Count == 0)
            {
                Console.WriteLine("No goals available. Please create a goal first.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Select a goal to record progress:");
            for (int i = 0; i < _goals.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
            }
            
            Console.Write("Which goal did you accomplish? ");
            int index = int.Parse(Console.ReadLine()) - 1;
            
            if (index >= 0 && index < _goals.Count)
            {
                Goal goal = _goals[index];
                int pointsEarned = goal.GetPoints();
                
                if (goal is ChecklistGoal checklistGoal)
                {
                    bool wasComplete = checklistGoal.IsComplete();
                    checklistGoal.RecordEvent();
                    
                    if (!wasComplete && checklistGoal.IsComplete())
                    {
                        pointsEarned += checklistGoal.GetBonus();
                        Console.WriteLine($"\n🎉 BONUS! You completed the checklist goal and earned an extra {checklistGoal.GetBonus()} points! 🎉");
                        CheckAchievement("checklist_master");
                    }
                    else if (!wasComplete)
                    {
                        Console.WriteLine($"\nProgress on '{goal.GetName()}': {checklistGoal.GetAmountCompleted()}/{checklistGoal.GetTarget()}");
                    }
                }
                else
                {
                    goal.RecordEvent();
                }
                
                _score += pointsEarned;
                
                int newLevel = _score / 1000 + 1;
                if (newLevel > _level)
                {
                    _level = newLevel;
                    Console.WriteLine($"\n🌟 LEVEL UP! You are now Level {_level}! 🌟");
                    CheckAchievement("level_up");
                }
                
                CheckDailyStreak();
                
                string[] encouragements = {
                    "🎯 Great job! Keep up the momentum!",
                    "💪 You're crushing it!",
                    "⭐ Amazing progress!",
                    "🏆 Every step brings you closer!",
                    "✨ You're doing fantastic!",
                    "🎈 Keep going - you've got this!",
                    "🌟 Another goal accomplished!"
                };
                Console.WriteLine($"\n{encouragements[_random.Next(encouragements.Length)]}");
                Console.WriteLine($"You earned {pointsEarned} points! Total score: {_score}");
                
                CheckAchievement("score_milestone");
            }
            else
            {
                Console.WriteLine("Invalid selection.");
            }
            
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private void CheckDailyStreak()
        {
            DateTime today = DateTime.Today;
            if (_lastRecordDate == DateTime.MinValue)
            {
                _dailyStreak = 1;
                Console.WriteLine("\n🔥 Daily streak started! Record events daily to build your streak! 🔥");
            }
            else if (_lastRecordDate == today.AddDays(-1))
            {
                _dailyStreak++;
                Console.WriteLine($"\n🔥 {_dailyStreak} day streak! +{_dailyStreak * 10} bonus points! 🔥");
                _score += _dailyStreak * 10; // Bonus points for maintaining streak
                
                if (_dailyStreak >= 7)
                {
                    CheckAchievement("streak_7");
                }
                if (_dailyStreak >= 30)
                {
                    CheckAchievement("streak_30");
                }
            }
            else if (_lastRecordDate < today.AddDays(-1))
            {
                if (_dailyStreak > 0)
                {
                    Console.WriteLine($"\n💔 Your {_dailyStreak} day streak has ended. Start a new streak today! 💔");
                }
                _dailyStreak = 1;
            }
            _lastRecordDate = today;
        }

        private void CheckAchievement(string achievementType)
        {
            if (achievementType == "checklist_master" && !_achievements.ContainsKey("checklist_master"))
            {
                _achievements["checklist_master"] = 1;
                Console.WriteLine("\n🏅 ACHIEVEMENT UNLOCKED: Checklist Master! 🏅");
            }
            else if (achievementType == "level_up" && _level == 5 && !_achievements.ContainsKey("level_5"))
            {
                _achievements["level_5"] = 1;
                Console.WriteLine("\n🏅 ACHIEVEMENT UNLOCKED: Level 5 Warrior! 🏅");
            }
            else if (achievementType == "level_up" && _level == 10 && !_achievements.ContainsKey("level_10"))
            {
                _achievements["level_10"] = 1;
                Console.WriteLine("\n🏅 ACHIEVEMENT UNLOCKED: Level 10 Champion! 🏅");
            }
            else if (achievementType == "streak_7" && !_achievements.ContainsKey("streak_7"))
            {
                _achievements["streak_7"] = 1;
                Console.WriteLine("\n🏅 ACHIEVEMENT UNLOCKED: Weekly Warrior (7 day streak)! 🏅");
            }
            else if (achievementType == "streak_30" && !_achievements.ContainsKey("streak_30"))
            {
                _achievements["streak_30"] = 1;
                Console.WriteLine("\n🏅 ACHIEVEMENT UNLOCKED: Monthly Master (30 day streak)! 🏅");
            }
            else if (achievementType == "score_milestone")
            {
                if (_score >= 5000 && !_achievements.ContainsKey("score_5000"))
                {
                    _achievements["score_5000"] = 1;
                    Console.WriteLine("\n🏅 ACHIEVEMENT UNLOCKED: 5000 Point Club! 🏅");
                }
                else if (_score >= 10000 && !_achievements.ContainsKey("score_10000"))
                {
                    _achievements["score_10000"] = 1;
                    Console.WriteLine("\n🏅 ACHIEVEMENT UNLOCKED: 10000 Point Legend! 🏅");
                }
            }
        }

        public void ShowAchievements()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║           ACHIEVEMENTS                 ║");
            Console.WriteLine("╠════════════════════════════════════════╣");
            
            if (_achievements.Count == 0)
            {
                Console.WriteLine("║   No achievements yet. Keep going!    ║");
            }
            else
            {
                if (_achievements.ContainsKey("checklist_master"))
                    Console.WriteLine("║  🏅 Checklist Master                    ║");
                if (_achievements.ContainsKey("level_5"))
                    Console.WriteLine("║  🏅 Level 5 Warrior                     ║");
                if (_achievements.ContainsKey("level_10"))
                    Console.WriteLine("║  🏅 Level 10 Champion                   ║");
                if (_achievements.ContainsKey("streak_7"))
                    Console.WriteLine("║  🏅 Weekly Warrior                      ║");
                if (_achievements.ContainsKey("streak_30"))
                    Console.WriteLine("║  🏅 Monthly Master                      ║");
                if (_achievements.ContainsKey("score_5000"))
                    Console.WriteLine("║  🏅 5000 Point Club                     ║");
                if (_achievements.ContainsKey("score_10000"))
                    Console.WriteLine("║  🏅 10000 Point Legend                  ║");
            }
            
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        public void SaveGoals()
        {
            Console.Write("Enter filename to save (e.g., goals.txt): ");
            string filename = Console.ReadLine();
            
            using (StreamWriter outputFile = new StreamWriter(filename))
            {
                outputFile.WriteLine($"SCORE:{_score},{_level},{_dailyStreak},{_lastRecordDate:yyyy-MM-dd}");
                
                string achievements = string.Join("|", _achievements.Keys);
                outputFile.WriteLine($"ACHIEVEMENTS:{achievements}");
                
                foreach (Goal goal in _goals)
                {
                    outputFile.WriteLine(goal.GetStringRepresentation());
                }
            }
            
            Console.WriteLine($"Goals saved to {filename} successfully!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public void LoadGoals()
        {
            Console.Write("Enter filename to load (e.g., goals.txt): ");
            string filename = Console.ReadLine();
            
            if (!File.Exists(filename))
            {
                Console.WriteLine("File not found!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }
            
            string[] lines = File.ReadAllLines(filename);
            _goals.Clear();
            _achievements.Clear();
            
            foreach (string line in lines)
            {
                if (line.StartsWith("SCORE:"))
                {
                    string[] parts = line.Substring(6).Split(',');
                    _score = int.Parse(parts[0]);
                    _level = int.Parse(parts[1]);
                    _dailyStreak = int.Parse(parts[2]);
                    if (parts.Length > 3 && parts[3] != "1/1/0001")
                    {
                        _lastRecordDate = DateTime.Parse(parts[3]);
                    }
                }
                else if (line.StartsWith("ACHIEVEMENTS:"))
                {
                    string achievements = line.Substring(13);
                    if (!string.IsNullOrEmpty(achievements))
                    {
                        string[] achievementList = achievements.Split('|');
                        foreach (string achievement in achievementList)
                        {
                            _achievements[achievement] = 1;
                        }
                    }
                }
                else
                {
                    string[] parts = line.Split(':');
                    string goalType = parts[0];
                    string[] data = parts[1].Split(',');
                    
                    switch (goalType)
                    {
                        case "SimpleGoal":
                            _goals.Add(new SimpleGoal(data[0], data[1], int.Parse(data[2]), bool.Parse(data[3])));
                            break;
                        case "EternalGoal":
                            _goals.Add(new EternalGoal(data[0], data[1], int.Parse(data[2])));
                            break;
                        case "ChecklistGoal":
                            _goals.Add(new ChecklistGoal(data[0], data[1], int.Parse(data[2]), int.Parse(data[3]), int.Parse(data[4]), int.Parse(data[5])));
                            break;
                    }
                }
            }
            
            Console.WriteLine($"Goals loaded from {filename} successfully!");
            Console.WriteLine($"Current Score: {_score}, Level: {_level}, Streak: {_dailyStreak}");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}