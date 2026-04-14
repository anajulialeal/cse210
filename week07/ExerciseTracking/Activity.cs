using System;

public abstract class Activity
{
    private string _date;
    private int _minutes;

    public Activity(string date, int minutes)
    {
        _date = date;
        _minutes = minutes;
    }

    public string GetDate()
    {
        return _date;
    }

    public int GetMinutes()
    {
        return _minutes;
    }

    public abstract double GetDistance(); // km
    public abstract double GetSpeed();    // km/h
    public abstract double GetPace();     // min per km

    public virtual string GetSummary()
    {
        return $"{_date} {this.GetType().Name} ({_minutes} min) - " +
               $"Distance: {GetDistance():F2} km, " +
               $"Speed: {GetSpeed():F2} kph, " +
               $"Pace: {GetPace():F2} min per km";
    }
}