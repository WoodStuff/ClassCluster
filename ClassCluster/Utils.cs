namespace ClassCluster;

internal static class Utils
{
	/// <summary>
	/// Converts an <paramref name="angle"/> from the <paramref name="input"/> unit into the <paramref name="output"/> unit.
	/// </summary>
	/// <param name="input">The angle unit that <paramref name="angle"/> is in.</param>
	/// <param name="angle">The value of the angle.</param>
	/// <param name="output">The angle unit to convert the angle into.</param>
	/// <returns>A double representing the angle in the <paramref name="output"/> unit.</returns>
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

	/// <summary>
	/// Checks if a value is infinite.
	/// </summary>
	/// <param name="value">The value to check.</param>
	/// <returns>If the value is at an infinite coordinate.</returns>
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