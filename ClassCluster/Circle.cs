using ClassCluster.Interfaces;

namespace ClassCluster;

/// <summary>
/// Represents a circle.
/// </summary>
public class Circle : IFigure2D<Circle>
{
	private double _radius;

	/// <summary>
	/// The center of the circle.
	/// </summary>
	public Point Center { get; set; }
	/// <summary>
	/// The radius of the circle - the distance from the center to the edge.
	/// </summary>
	public double Radius
	{
		get => _radius;
		set
		{
			if (value <= 0 || double.IsNaN(value)) throw new ArgumentException("Radius must be positive");
			_radius = value;
		}
	}

	public Circle(Point center, double radius)
	{
		Center = center;
		Radius = radius;
	}
	public Circle(double radius) : this(Point.Origin, radius) { }

	/// <summary>
	/// The diameter of the edge - twice the radius.
	/// </summary>
	public double Diameter
	{
		get => Radius * 2;
		set => Radius = value / 2;
	}
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
	public double Distance(Point p) => p.Distance(Center) - Radius;
	/// <summary>
	/// Finds the point on this circle on a specified angle.
	/// </summary>
	/// <param name="angle">The angle to find the point at.</param>
	/// <param name="type">The unit <paramref name="angle"/> is expressed in.</param>
	/// <returns>The point on this circle located at the <paramref name="angle"/></returns>
	public Point PointAtAngle(double angle, AngleUnit type = AngleUnit.Radians) => Center + Vector.FromAngle(angle, type) * Radius;
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

	// interface members
	double IFigure2D<Circle>.Perimeter => Circumference;

	public override string ToString() => $"Circle({Radius}) : {Center}";
	public override bool Equals(object? obj)
	{
		if (obj == null || GetType() != obj.GetType()) return false;

		Circle other = (Circle)obj;
		return Center == other.Center && Math.Abs(Radius - other.Radius) < 1e-6;
	}
	public override int GetHashCode() => HashCode.Combine(Center, Radius);

	public static bool operator ==(Circle left, Circle right)
	{
		if (left is null) return right is null;
		return left.Equals(right);
	}
	public static bool operator !=(Circle left, Circle right) => !(left == right);

	/// <summary>
	/// Moves the circle by a vector.
	/// </summary>
	public static Circle operator +(Circle c, Vector v) => new(c.Center + v, c.Radius);
	/// <summary>
	/// Increases the radius of the circle by some number.
	/// </summary>
	public static Circle operator +(Circle c, double n) => new(c.Center, c.Radius + n);
	/// <summary>
	/// Subtracts a vector from the circle.
	/// </summary>
	public static Circle operator -(Circle c, Vector v) => new(c.Center - v, c.Radius);
	/// <summary>
	/// Decreases the radius of the circle by some number.
	/// </summary>
	public static Circle operator -(Circle c, double n) => new(c.Center, c.Radius - n);
}