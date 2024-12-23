namespace ClassCluster;

/// <summary>
/// Represents an unbounded 2D line.
/// </summary>
public class Line : IObject2D
{
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
	public Point P1 { get; set; }
	/// <summary>
	/// The second anchor point for the line.
	/// </summary>
	public Point P2 { get; set; }

	public Line(Point p1, Point p2)
	{
		if (p1 == p2) throw new ArgumentException("Cannot create a line with two same points");
		P1 = p1;
		P2 = p2;
	}
	public Line(Point point)
	{
		if (point == Point.Origin) throw new ArgumentException("Cannot create a line with two same points");
		P1 = Point.Origin;
		P2 = point;
	}
	public Line(double s, double i)
	{
		P1 = new(0, i);
		P2 = new(1, s + i);
	}
	public Line(double p1x, double p1y, double p2x, double p2y)
	{
		P1 = new(p1x, p1y);
		P2 = new(p2x, p2y);
	}

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
	public double YIntercept
	{
		get
		{
			if (IsVertical())
			{
				double p = P1.X;
				if (p == 0) return 0;
				if (p > 0) return double.NegativeInfinity;
				if (p < 0) return double.PositiveInfinity;
			}
			return P1.Y - Slope * P1.X;
		}
	}
	public double XIntercept
	{
		get
		{
			if (IsHorizontal())
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
	/// <returns>Result of the check.</returns>
	public bool IsHorizontal()
	{
		return Slope == 0;
	}
	/// <summary>
	/// Checks if the line is straight vertical.
	/// </summary>
	/// <returns>Result of the check.</returns>
	public bool IsVertical()
	{
		return Slope == double.PositiveInfinity;
	}
	/// <summary>
	/// Checks if the line is parallel to another line.
	/// </summary>
	/// <param name="other">The line to check parallelness.</param>
	/// <returns>Result of the check.</returns>
	public bool IsParallel(Line other)
	{
		return Slope == other.Slope;
	}
	/// <summary>
	/// Checks if two lines are parallel.
	/// </summary>
	/// <param name="l1">The first line to check parallelness of.</param>
	/// <param name="l2">The second line to check parallelness of.</param>
	/// <returns>Result of the check.</returns>
	public static bool IsParallel(Line l1, Line l2)
	{
		return l1.IsParallel(l2);
	}
	/// <summary>
	/// Checks if the line is perpendicular to another line.
	/// </summary>
	/// <param name="other">The line to check perpendicularness.</param>
	/// <returns>Result of the check.</returns>
	public bool IsPerpendicular(Line other)
	{
		return Slope * other.Slope == -1;
	}
	/// <summary>
	/// Checks if two lines are perpendicular.
	/// </summary>
	/// <param name="l1">The first line to check perpendicularness of.</param>
	/// <param name="l2">The second line to check perpendicularness of.</param>
	/// <returns>Result of the check.</returns>
	public static bool IsPerpendicular(Line l1, Line l2)
	{
		return l1.IsPerpendicular(l2);
	}
	/// <summary>
	/// Checks if the line contains a point.
	/// </summary>
	/// <param name="p">The point to check for.</param>
	/// <returns>If the line includes the point.</returns>
	public bool Contains(Point p)
	{
		if (IsVertical()) return p.X == P1.X;
		return Math.Round(PointAtX(p.X)!.Y, 6) == p.Y;
	}
	/// <summary>
	/// Gets the point at the specified x coordinate.
	/// </summary>
	/// <param name="x">The x coordinate.</param>
	/// <returns>The point that is contained by the line at the specified x coordinate.</returns>
	public Point? PointAtX(double x)
	{
		if (IsVertical()) return null;
		if (IsHorizontal()) return new(x, P1.Y);
		return new(x, Slope * x + YIntercept);
	}
	/// <summary>
	/// Gets the point at the specified y coordinate.
	/// </summary>
	/// <param name="y">The y coordinate.</param>
	/// <returns>The point that is contained by the line at the specified y coordinate.</returns>
	public Point? PointAtY(double y)
	{
		if (IsHorizontal()) return null;
		if (IsVertical()) return new(P1.X, y);
		return new((y - YIntercept) / Slope, y);
	}
	/// <summary>
	/// Finds the intersection of two lines. Returns null if the lines are parallel.
	/// </summary>
	/// <param name="other">The other line.</param>
	/// <returns>A point representing the intersection.</returns>
	public Point? Intersection(Line other)
	{
		if (IsParallel(other)) return null;

		double xIntersection = (other.YIntercept - YIntercept) / (Slope - other.Slope);
		double yIntersection = PointAtX(xIntersection)!.Y;

		return new Point(xIntersection, yIntersection);
	}
	/// <summary>
	/// Moves the line to pass through the origin.
	/// </summary>
	/// <param name="p2">By default the first anchor point will be moved to (0, 0). This should be set to true if the second anchor point is to be moved to the origin.</param>
	/// <returns>The line passing through the origin.</returns>
	public Line ToOrigin(bool p2 = false, bool normalize = false)
	{
		Line l = p2 ? this - (Vector)P2 : this - (Vector)P1;
		if (normalize)
		{
			l.P1.Normalize();
			l.P2.Normalize();
		}
		return l;
	}

	/// <summary>
	/// Clones the line with the same anchor points.
	/// </summary>
	/// <returns>A copy of the line.</returns>
	public Line Clone()
	{
		return (Line)MemberwiseClone();
	}

	public static Line Parallel(Line l, Point p)
	{
		return new Line(p, p + l.P2 - l.P1);
	}

	public override string ToString()
	{
		return $"-{P1}--{P2}-";
	}
	public override bool Equals(object? obj)
	{
		if (obj == null || GetType() != obj.GetType())
		{
			return false;
		}

		Line other = (Line)obj;
		double inter = IsVertical() ? PointAtY(0)!.X : YIntercept;
		double otherInter = other.IsVertical() ? other.PointAtY(0)!.X : other.YIntercept;
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