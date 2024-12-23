namespace ClassCluster;

public static class Utils
{
	// to be moved into Angle class
	public static double ConvertAngle(Angles input, double angle, Angles output)
	{
		if (input == output) return angle;
		var angleInDegrees = input switch
		{
			Angles.Degrees => angle,
			Angles.Radians => angle * 180.0 / Math.PI,
			_ => throw new ArgumentException("Invalid input angle unit."),
		};
		angleInDegrees = output switch
		{
			Angles.Degrees => angleInDegrees,
			Angles.Radians => angleInDegrees * Math.PI / 180.0,
			_ => throw new ArgumentException("Invalid output angle unit."),
		};
		return angleInDegrees;
	}
}