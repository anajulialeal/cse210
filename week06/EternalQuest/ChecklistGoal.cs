using System;

namespace EternalQuest
{
    public class ChecklistGoal : Goal
    {
        private int _amountCompleted;
        private int _target;
        private int _bonus;

        public ChecklistGoal(string name, string description, int points, int target, int bonus)
            : base(name, description, points)
        {
            _target = target;
            _bonus = bonus;
            _amountCompleted = 0;
        }

        public ChecklistGoal(string name, string description, int points, int target, int bonus, int amountCompleted)
            : base(name, description, points)
        {
            _target = target;
            _bonus = bonus;
            _amountCompleted = amountCompleted;
        }

        public override void RecordEvent()
        {
            if (_amountCompleted < _target)
            {
                _amountCompleted++;
            }
        }

        public override bool IsComplete()
        {
            return _amountCompleted >= _target;
        }

        public override string GetDetailsString()
        {
            string status = IsComplete() ? "[X]" : "[ ]";
            return $"{status} {_shortName} ({_description}) -- Completed {_amountCompleted}/{_target} times";
        }

        public override string GetStringRepresentation()
        {
            return $"ChecklistGoal:{_shortName},{_description},{_points},{_target},{_bonus},{_amountCompleted}";
        }

        public int GetBonus() => _bonus;
        public int GetAmountCompleted() => _amountCompleted;
        public int GetTarget() => _target;
    }
}