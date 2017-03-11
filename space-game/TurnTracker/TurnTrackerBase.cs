using System;

namespace TurnTracker
{
    public abstract class TurnTrackerBase : ITurnTracker
    {
        private readonly TimeSpan _defaultTurnLength = TimeSpan.FromDays(30);

        protected TurnTrackerBase(TimeSpan? turnLength = null)
        {
            TurnLength = turnLength ?? _defaultTurnLength;
        }

        public int TurnNumber { get; private set; } = 0;

        public virtual void Update()
        {
            TurnNumber += 1;
        }

        public DateTime StartDate { get; } = new DateTime(1990, 1, 1);

        public DateTime CurrentDate => StartDate.AddDays(TurnLength.Days * TurnNumber);

        public TimeSpan TurnLength { get; }

        public TimeSpan TimeSinceStart => TimeSpan.FromDays(TurnLength.Days * TurnNumber);
    }
}
