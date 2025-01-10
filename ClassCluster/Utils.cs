namespace ClassCluster;

internal static class Utils
{
	public static double ConvertAngle(Angles input, double angle, Angles output)
	{
		if (input == output) return angle;
		var angleInDegrees = input switch
		{
			Angles.Degrees => angle,
			Angles.Radians => angle * 180.0 / Math.PI,
			_ => throw new ArgumentException("Invalid input angle unit.")
		};
		angleInDegrees = output switch
		{
			Angles.Degrees => angleInDegrees,
			Angles.Radians => angleInDegrees * Math.PI / 180.0,
			_ => throw new ArgumentException("Invalid output angle unit.")
		};
		return angleInDegrees;
	}

	public static bool IsInfinity(object? value)
	{
		return value switch
		{
			double d => double.IsInfinity(d),
			float f => float.IsInfinity(f),
			Point p => double.IsInfinity(p.X) || double.IsInfinity(p.Y),
			Vector v => double.IsInfinity(v.X) || double.IsInfinity(v.Y),
			_ => false
		};
	}
}