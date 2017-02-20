namespace Core.Classes
{
    public class DistanceRange : Range
	{
		public DistanceRange(Distance defaultLowerBound, Distance defaultUpperBound) : base(defaultLowerBound.Kilometers, defaultUpperBound.Kilometers)
		{
		}

		public void AddValue(Distance value)
		{
			base.AddValue(value.Kilometers);
		}

		public new Distance LowerBound => Distance.FromKilometers(GetLowerBound());

		public new Distance UpperBound => Distance.FromKilometers(GetUpperBound());
	}
}
