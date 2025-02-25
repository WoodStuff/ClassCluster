using ClassCluster.Interfaces;

namespace ClassCluster;

/// <summary>
/// Represents a 2D point.
/// </summary>
public struct Point : IObject2D<Point>
{
	private const double Tolerance = 1e-6;

	private double _x;
	private double _y;

	/// <summary>
	/// The point at (0, 0).
	/// </summary>
	public static Point Origin => new(0, 0);

	/// <summary>
	/// The X position of the point.
	/// </summary>
	public readonly double X => _x;
	/// <summary>
	/// The Y position of the point.
	/// </summary>
	public readonly double Y => _y;

	public Point(double x, double y)
	{
		_x = x;
		_y = y;
	}

	/// <summary>
	/// Calculates the point's euclidean distance from the origin (the point at (0, 0)).
	/// </summary>
	/// <returns>A double representing the euclidean distance.</returns>
	public readonly double DistanceFromOrigin => Math.Sqrt(X * X + Y * Y);
	/// <summary>
	/// Calculates the point's taxicab distance from the origin (the point at (0, 0)).
	/// </summary>
	/// <returns>A double representing the taxicab distance.</returns>
	public readonly double GridDistFromOrigin => Math.Abs(X) + Math.Abs(Y);
	/// <summary>
	/// Calculates the point's angle in radians from the origin, from 0 to <see cref="Math.Tau"/>.
	/// </summary>
	public readonly double Theta
	{
		get
		{
			double angle = Math.Atan2(Y, X);
			if (angle < 0) angle += Math.Tau;
			return angle;
		}
	}
	
	/// <summary>
	/// Calculates the point's euclidean distance from another point.
	/// </summary>
	/// <param name="other">The point to calculate distance to.</param>
	/// <returns>A double representing the euclidean distance.</returns>
	public readonly double Distance(Point other)
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
	public static double Distance(Point p1, Point p2) => p1.Distance(p2);
	/// <summary>
	/// Calculates the point's taxicab distance from another point.
	/// </summary>
	/// <param name="other">The point to calculate taxicab distance to.</param>
	/// <returns>A double representing the taxicab distance.</returns>
	public readonly double GridDist(Point other)
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
	public static double GridDist(Point p1, Point p2) => p1.GridDist(p2);
	/// <summary>
	/// Normalizes the point as if it was a vector.
	/// Shortens the point to have a length of 1 while maintaining the direction and returns it.
	/// Returns the origin if the given point is the origin.
	/// </summary>
	/// <returns>The normalized point</returns>
	public readonly Point ToNormalized()
	{
		double length = DistanceFromOrigin;
		if (length == 0) return this;
		return new(X / length, Y / length);
	}

	/// <summary>
	/// Normalizes the point as if it was a vector.
	/// Shortens the point to have a length of 1 while maintaining the direction.
	/// Returns the origin if the given point is the origin.
	/// </summary>
	public void Normalize()
	{
		double length = DistanceFromOrigin;
		if (length == 0) return;
		_x /= length;
		_y /= length;
	}

	public override readonly string ToString() => $"({X}, {Y})";
	public override readonly bool Equals(object? obj)
	{
		if (obj == null || GetType() != obj.GetType())
		{
			return false;
		}

		Point other = (Point)obj;
		return Math.Abs(X - other.X) < Tolerance && Math.Abs(Y - other.Y) < Tolerance;
	}
	public override readonly int GetHashCode() => HashCode.Combine(X, Y);

	public static implicit operator Point((double x, double y) tuple) => new(tuple.x, tuple.y);
	public static explicit operator Point(Vector v) => new(v.X, v.Y);

	public static bool operator ==(Point left, Point right) => left.Equals(right);
	public static bool operator !=(Point left, Point right) => !(left == right);

	public static Point operator +(Point left, Point right)	=> new(left.X + right.X, left.Y + right.Y);
	public static Point operator +(Point p, Vector v) => new(p.X + v.X, p.Y + v.Y);
	public static Point operator -(Point left, Point right) => new(left.X - right.X, left.Y - right.Y);
	public static Point operator -(Point p, Vector v) => new(p.X - v.X, p.Y - v.Y);
	public static Point operator -(Point p) => new(-p.X, -p.Y);
	public static Point operator *(Point p, double scalar) => new(p.X * scalar, p.Y * scalar);
	public static Point operator *(double scalar, Point p) => p * scalar;
	public static Point operator /(Point p, double scalar)
	{
		if (scalar == 0) throw new DivideByZeroException("Attempted to divide a point by zero.");
		return new(p.X / scalar, p.Y / scalar);
	}
}