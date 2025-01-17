﻿namespace TSCA.CodingTracker;

internal class CodingSession
{
    internal int Id { get; set; }
    internal DateTime StartTime { get; private set; }
    internal DateTime EndTime { get; private set; }
    internal TimeSpan Duration { get; private set; }

    internal System.Timers.Timer? timer;
    internal Action<TimeSpan>? TimerElapsed;

    internal CodingSession(int id, DateTime startTime, DateTime endTime, TimeSpan duration)
    {
        this.Id = id;
        this.StartTime = startTime;
        this.EndTime = endTime;
        this.Duration = duration;
    }

    internal CodingSession(DateTime startTime, DateTime endTime, TimeSpan duration)
    {
        this.StartTime = startTime;
        this.EndTime = endTime;
        this.Duration = duration;
    }

    internal CodingSession(DateTime startTime)
    {
        this.StartTime = startTime;
        this.EndTime = new DateTime();
    }

    internal void StartTimer()
    {
        timer = new System.Timers.Timer(1000);
        timer.Elapsed += OnTimerElapsed;
        timer.Start();
    }

    internal void StopTimer()
    {
        EndTime = DateTime.Now;
        timer.Stop();
        timer.Dispose();
    }

    private void OnTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        TimeSpan elapsedTime = DateTime.Now - StartTime;
        Duration = elapsedTime;
        TimerElapsed?.Invoke(elapsedTime);
    }
}