namespace ClassCluster;

/// <summary>
/// Represents a 2D point.
/// </summary>
public class Point(double x, double y) : IObject2D
{
	/// <summary>
	/// The point at (0, 0).
	/// </summary>
	public static Point Origin => (0, 0);
	/// <summary>
	/// The X position of the point.
	/// </summary>
	public double X { get; set; } = x;
	/// <summary>
	/// The Y position of the point.
	/// </summary>
	public double Y { get; set; } = y;

	/// <summary>
	/// Calculates the point's euclidean distance from the origin (the point at (0, 0)).
	/// </summary>
	/// <returns>A double representing the euclidean distance.</returns>
	public double DistanceFromOrigin()
	{
		return Math.Sqrt(X * X + Y * Y);
	}
	/// <summary>
	/// Calculates the point's euclidean distance from another point.
	/// </summary>
	/// <param name="other">The point to calculate distance to.</param>
	/// <returns>A double representing the euclidean distance.</returns>
	public double Distance(Point other)
	{
		double dx = X - other.X;
		double dy = Y - other.Y;
		return Math.Sqrt(dx * dx + dy * dy);
	}
	/// <summary>
	/// Calculates two points' euclidean distance from each other.
	/// </summary>
	/// <param name="p1">The first point.</param>
	/// <param name="p2">The second point.</param>
	/// <returns>A double representing the euclidean distance.</returns>
	public static double Distance(Point p1, Point p2)
	{
		return p1.Distance(p2);
	}
	/// <summary>
	/// Calculates the point's taxicab distance from the origin (the point at (0, 0)).
	/// </summary>
	/// <returns>A double representing the taxicab distance.</returns>
	public double GridDistFromOrigin()
	{
		return Math.Abs(X) + Math.Abs(Y);
	}
	/// <summary>
	/// Calculates the point's taxicab distance from another point.
	/// </summary>
	/// <param name="other">The point to calculate taxicab distance to.</param>
	/// <returns>A double representing the taxicab distance.</returns>
	public double GridDist(Point other)
	{
		double dx = X - other.X;
		double dy = Y - other.Y;
		return Math.Abs(dx) + Math.Abs(dy);
	}
	/// <summary>
	/// Calculates two points' taxicab distance from each other.
	/// </summary>
	/// <param name="p1">The first point.</param>
	/// <param name="p2">The second point.</param>
	/// <returns>A double representing the taxicab distance.</returns>
	public static double GridDist(Point p1, Point p2)
	{
		return p1.GridDist(p2);
	}

	/// <summary>
	/// Normalizes the point as if it was a vector.
	/// Shortens the point to have a length of 1 while maintaining the direction.
	/// </summary>
	public void Normalize()
	{
		double length = DistanceFromOrigin();
		if (length != 0)
		{
			X /= length;
			Y /= length;
		}
	}

	public override string ToString()
	{
		return $"({X}, {Y})";
	}
	public override bool Equals(object? obj)
	{
		if (obj == null || GetType() != obj.GetType())
		{
			return false;
		}

		Point other = (Point)obj;
		return Math.Abs(X - other.X) < 0.000001 && Math.Abs(Y - other.Y) < 0.000001;
	}
	public override int GetHashCode()
	{
		return HashCode.Combine(X, Y);
	}

	public static implicit operator Point((double x, double y) tuple) => new(tuple.x, tuple.y);
	public static explicit operator Point(Vector v) => new(v.X, v.Y);

	public static bool operator ==(Point left, Point right)
	{
		if (left is null) return right is null;
		return left.Equals(right);
	}
	public static bool operator !=(Point left, Point right)
	{
		return !(left == right);
	}

	public static Point operator +(Point left, Point right)	=> new(left.X + right.X, left.Y + right.Y);
	public static Point operator +(Point p, Vector v) => new(p.X + v.X, p.Y + v.Y);
	public static Point operator -(Point left, Point right) => new(left.X - right.X, left.Y - right.Y);
	public static Point operator -(Point p, Vector v) => new(p.X - v.X, p.Y - v.Y);
	public static Point operator -(Point p) => new(-p.X, -p.Y);
	public static Point operator *(Point p, double scalar) => new(p.X * scalar, p.Y * scalar);
	public static Point operator /(Point p, double scalar) => new(p.X / scalar, p.Y / scalar);
}