namespace AlmostGoodEngine.Physics
{
	public struct Interval
	{
		public float Min { get; set; }
		public float Max { get; set; }

		public Interval(float min, float max)
		{
			Min = min;
			Max = max;
		}
	}
}
