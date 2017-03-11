using System;

namespace TurnTracker
{
    public interface ITurnTracker
    {
        int TurnNumber { get; }
        void Update();
        System.DateTime StartDate { get; }
        System.DateTime CurrentDate { get; }
        TimeSpan TurnLength { get; }
        TimeSpan TimeSinceStart { get; }
    }
}
