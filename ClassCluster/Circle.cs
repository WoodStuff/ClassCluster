using ClassCluster.Interfaces;

namespace ClassCluster;

/// <summary>
/// Represents a 2D circle.
/// </summary>
public class Circle : IFigure2D
{
	private double _radius;

	/// <summary>
	/// The center of the circle.
	/// </summary>
	public Point Center { get; set; }
	/// <summary>
	/// The radius of the edge - the distance from the center to the edge.
	/// </summary>
	public double Radius
	{
		get => _radius;
		set
		{
			if (value <= 0) throw new ArgumentException("Radius must be positive");
			_radius = value;
		}
	}

	public Circle(Point center, double radius)
	{
		if (radius <= 0) throw new ArgumentException("Radius must be positive");
		Center = center;
		Radius = radius;
	}
	public Circle(double radius) : this(Point.Origin, radius) { }

	/// <summary>
	/// The diameter of the edge - twice the radius.
	/// </summary>
	public double Diameter => Radius * 2;
	/// <summary>
	/// The circumference of the circle.
	/// </summary>
	public double Circumference => Radius * Math.Tau;
	/// <summary>
	/// The area of the circle.
	/// </summary>
	public double Area => Radius * Radius * Math.PI;

	/// <summary>
	/// Calculates a point's signed distance from the circle.
	/// Outside values are positive and inside values are negative.
	/// </summary>
	/// <param name="p">The point to calculate distance from.</param>
	/// <returns>A double representing the signed distance.</returns>
	public double Distance(Point p)
	{
		return p.Distance(Center) - Radius;
	}
	/// <summary>
	/// Finds the relative position of a point to the circle.
	/// </summary>
	/// <param name="p">The point to locate relative to the circle.</param>
	/// <returns>If the point is outside, on the boundary, or inside the circle.</returns>
	public Position Locate(Point p)
	{
		return double.Sign(Distance(p)) switch
		{
			1 => Position.Outside,
			0 => Position.On,
			-1 => Position.Inside,
			_ => throw new InvalidOperationException("Unexpected sign value.")
		};
	}

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