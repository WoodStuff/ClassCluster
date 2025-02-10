using ClassCluster.Interfaces;

namespace ClassCluster;

/// <summary>
/// Represents an unbounded 2D line.
/// </summary>
public class Line : IObject2D<Line>
{
	private Point _p1;
	private Point _p2;

	/// <summary>
	/// A horizontal line on the X axis.
	/// </summary>
	public static Line Horizontal => new((0, 0), (1, 0));
	/// <summary>
	/// A vertical line on the Y axis.
	/// </summary>
	public static Line Vertical => new((0, 0), (0, 1));
	/// <summary>
	/// The first anchor point for the line.
	/// </summary>
	public Point P1
	{
		get => _p1;
		set {
			if (value == _p2) throw new ArgumentException("A line cannot have two same anchor points");
			_p1 = value;
		}
	}
	/// <summary>
	/// The second anchor point for the line.
	/// </summary>
	public Point P2
	{
		get => _p2;
		set
		{
			if (value == _p1) throw new ArgumentException("A line cannot have two same anchor points");
			_p2 = value;
		}
	}

	public Line(Point p1, Point p2)
	{
		if (p1 == p2)
			throw new ArgumentException("A line cannot have two same anchor points");
		_p1 = p1;
		_p2 = p2;
	}
	public Line(Point point) : this(Point.Origin, point) { }
	public Line(double p1x, double p1y, double p2x, double p2y) : this(new Point(p1x, p1y), new Point(p2x, p2y)) { }
	public Line(double s, double i) : this(new Point(0, i), new Point(1, s + i)) { }

	/// <summary>
	/// The rate of change of the line. An increase in X by 1 adds to the Y by the slope.
	/// </summary>
	public double Slope
	{
		get
		{
			Point p = ToOrigin().P2;
			double slope = p.Y / p.X;
			if (slope == double.NegativeInfinity) slope = double.PositiveInfinity;
			return slope;
		}
	}
	/// <summary>
	/// The Y at which the line intersects the Y axis.
	/// </summary>
	public double YIntercept
	{
		get
		{
			if (IsVertical)
			{
				double p = P1.X;
				if (p == 0) return 0;
				if (p > 0) return double.NegativeInfinity;
				if (p < 0) return double.PositiveInfinity;
			}
			return P1.Y - Slope * P1.X;
		}
	}
	/// <summary>
	/// The X at which the line intersects the X axis.
	/// </summary>
	public double XIntercept
	{
		get
		{
			if (IsHorizontal)
			{
				double p = P1.Y;
				if (p == 0) return 0;
				if (p > 0) return double.NegativeInfinity;
				if (p < 0) return double.PositiveInfinity;
			}
			return -YIntercept / Slope;
		}
	}
	/// <summary>
	/// Checks if the line is straight horizontal.
	/// </summary>
	/// <returns>If the line has slope equal to 0.</returns>
	public bool IsHorizontal => Slope == 0;
	/// <summary>
	/// Checks if the line is straight vertical.
	/// </summary>
	/// <returns>If the line has +infinity slope.</returns>
	public bool IsVertical => Slope == double.PositiveInfinity;

	/// <summary>
	/// Checks if the line is parallel to another line.
	/// </summary>
	/// <param name="other">The line to check parallelness.</param>
	/// <returns>Result of the check.</returns>
	public bool IsParallelTo(Line other)
	{
		return Slope == other.Slope;
	}
	/// <summary>
	/// Checks if two lines are parallel.
	/// </summary>
	/// <param name="l1">The first line to check parallelness of.</param>
	/// <param name="l2">The second line to check parallelness of.</param>
	/// <returns>Result of the check.</returns>
	public static bool AreParallel(Line l1, Line l2)
	{
		return l1.IsParallelTo(l2);
	}
	/// <summary>
	/// Checks if the line is perpendicular to another line.
	/// </summary>
	/// <param name="other">The line to check perpendicularness.</param>
	/// <returns>Result of the check.</returns>
	public bool IsPerpendicularTo(Line other)
	{
		if (IsHorizontal) return other.IsVertical;
		if (IsVertical) return other.IsHorizontal;
		return Slope * other.Slope == -1;
	}
	/// <summary>
	/// Checks if two lines are perpendicular.
	/// </summary>
	/// <param name="l1">The first line to check perpendicularness of.</param>
	/// <param name="l2">The second line to check perpendicularness of.</param>
	/// <returns>Result of the check.</returns>
	public static bool ArePerpendicular(Line l1, Line l2)
	{
		return l1.IsPerpendicularTo(l2);
	}
	/// <summary>
	/// Checks if the line contains a point.
	/// </summary>
	/// <param name="p">The point to check for.</param>
	/// <returns>If the line includes the point.</returns>
	public bool Contains(Point p)
	{
		if (IsVertical) return p.X == P1.X;
		return PointAtX(p.X)! == p;
	}
	/// <summary>
	/// Gets the point at the specified x coordinate.
	/// </summary>
	/// <param name="x">The x coordinate.</param>
	/// <returns>The point that is contained by the line at the specified x coordinate.</returns>
	public Point? PointAtX(double x)
	{
		if (IsVertical) return null;
		if (IsHorizontal) return new(x, P1.Y);
		return new(x, Slope * x + YIntercept);
	}
	/// <summary>
	/// Gets the point at the specified y coordinate.
	/// </summary>
	/// <param name="y">The y coordinate.</param>
	/// <returns>The point that is contained by the line at the specified y coordinate.</returns>
	public Point? PointAtY(double y)
	{
		if (IsHorizontal) return null;
		if (IsVertical) return new(P1.X, y);
		return new((y - YIntercept) / Slope, y);
	}
	/// <summary>
	/// Finds the intersection of two lines. Returns null if the lines are parallel.
	/// </summary>
	/// <param name="other">The other line.</param>
	/// <returns>A point representing the intersection.</returns>
	public Point? Intersection(Line other)
	{
		if (IsParallelTo(other)) return null;

		double xIntersection = (other.YIntercept - YIntercept) / (Slope - other.Slope);
		double yIntersection = PointAtX(xIntersection)!.Value.Y;

		return new Point(xIntersection, yIntersection);
	}
	/// <summary>
	/// Moves the line to pass through the origin.
	/// </summary>
	/// <param name="p2">By default the first anchor point will be moved to (0, 0). This should be set to true if the second anchor point is to be moved to the origin.</param>
	/// <param name="normalize">If the other point should be normalized.</param>
	/// <returns>The line passing through the origin.</returns>
	public Line ToOrigin(bool p2 = false, bool normalize = false)
	{
		Line l = p2 ? this - (Vector)P2 : this - (Vector)P1;
		if (normalize)
		{
			l.P1 = l.P1.ToNormalized();
			l.P2 = l.P2.ToNormalized();
		}
		return l;
	}
	/// <summary>
	/// Creates a line parallel to this one that passes through a given point.
	/// </summary>
	/// <param name="p">The point the line must pass through.</param>
	/// <returns>A new parallel line passing through the point p.</returns>
	public Line ParallelThrough(Point p)
	{
		return new Line(p, p + P2 - P1);
	}

	/// <summary>
	/// Clones the line with the same anchor points.
	/// </summary>
	/// <returns>A copy of the line.</returns>
	public Line Clone()
	{
		return (Line)MemberwiseClone();
	}

	public override string ToString() => $"-{P1}--{P2}-";
	public override bool Equals(object? obj)
	{
		if (obj == null || GetType() != obj.GetType())
		{
			return false;
		}

		Line other = (Line)obj;
		double inter = IsVertical ? PointAtY(0)!.Value.X : YIntercept;
		double otherInter = other.IsVertical ? other.PointAtY(0)!.Value.X : other.YIntercept;
		return Math.Abs(Slope - other.Slope) < 0.000001 && Math.Abs(inter - otherInter) < 0.000001;
	}
	public override int GetHashCode()
	{
		double inter = Slope == double.PositiveInfinity || Slope == double.NegativeInfinity ? P1.X : YIntercept;
		return HashCode.Combine(Slope, inter);
	}

	public static bool operator ==(Line left, Line right)
	{
		if (left is null) return right is null;
		return left.Equals(right);
	}
	public static bool operator !=(Line left, Line right)
	{
		return !(left == right);
	}

	public static Line operator +(Line l, Vector v) => new(l.P1 + v, l.P2 + v);
	public static Line operator -(Line l, Vector v) => new(l.P1 - v, l.P2 - v);
}