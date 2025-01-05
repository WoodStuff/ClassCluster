using ClassCluster.Interfaces;

namespace ClassCluster;

/// <summary>
/// Represents a 2D circle.
/// </summary>
public class Circle : IFigure2D
{
	public Point Center { get; set; }
	public double Radius { get; set; }

	public Circle(Point center, double radius)
	{
		if (radius <= 0) throw new ArgumentException("Radius must be positive");
		Center = center;
		Radius = radius;
	}
	public Circle(double radius)
	{
		if (radius <= 0) throw new ArgumentException("Radius must be positive");
		Center = Point.Origin;
		Radius = radius;
	}

	public double Diameter => Radius * 2;
	public double Circumference => Radius * Math.Tau;
	public double Area => Radius * Radius * Math.PI;

	public override string ToString() => $"Circle({Radius}) : {Center}";
	public override bool Equals(object? obj)
	{
		if (obj == null || GetType() != obj.GetType()) return false;

		Circle other = (Circle)obj;
		return Center == other.Center && Math.Abs(Radius - other.Radius) < 1e-6;
	}
	public override int GetHashCode()
	{
		return HashCode.Combine(Center, Radius);
	}
}