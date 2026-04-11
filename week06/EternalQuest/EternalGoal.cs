using System;

namespace EternalQuest
{
    public class EternalGoal : Goal
    {
        public EternalGoal(string name, string description, int points)
            : base(name, description, points)
        {
        }

        public EternalGoal(string name, string description, int points, bool unused)
            : base(name, description, points)
        {
            // Eternal goals are never complete, the unused parameter is for loading compatibility
        }

        public override void RecordEvent()
        {
            // No state change needed - eternal goals can always be recorded
        }

        public override bool IsComplete()
        {
            return false; // Eternal goals are never complete
        }

        public override string GetStringRepresentation()
        {
            return $"EternalGoal:{_shortName},{_description},{_points}";
        }
    }
}