using System;

namespace TurnTracker.InMemoryTurnTracker
{
    public class TurnTracker : TurnTrackerBase
    {
        public TurnTracker() : base(TimeSpan.FromDays(30))
        {
        }
    }
}
