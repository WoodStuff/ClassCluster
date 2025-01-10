using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClassCluster.Tests;

[TestClass]
public class LineTests
{
	#region Constructor Tests
	[TestMethod]
	public void Constructor_FromTwoPoints_InitializesPoints()
	{
		Line l1 = new((1, 2), (3, 4));
		Assert.AreEqual(new(1, 2), l1.P1);
		Assert.AreEqual(new(3, 4), l1.P2);
	}

	[TestMethod]
	public void Constructor_FromTwoPoints_WithSamePoints_ThrowsError()
	{
		Assert.ThrowsException<ArgumentException>(() => _ = new Line((1, 2), (1, 2)));
	}

	[TestMethod]
	public void Constructor_FromSinglePoint_InitializesPoints()
	{
		Line l1 = new((1, 2));
		Assert.AreEqual(Point.Origin, l1.P1);
		Assert.AreEqual(new(1, 2), l1.P2);
	}

	[TestMethod]
	public void Constructor_FromSinglePoint_WithOrigin_ThrowsError()
	{
		Assert.ThrowsException<ArgumentException>(() => _ = new Line(Point.Origin));
	}

	[TestMethod]
	public void Constructor_FromCoordinates_InitializesPoints()
	{
		Line l1 = new(1, 2, 3, 4);
		Assert.AreEqual(new(1, 2), l1.P1);
		Assert.AreEqual(new(3, 4), l1.P2);
	}

	[TestMethod]
	public void Constructor_FromCoordinates_WithSamePoints_ThrowsError()
	{
		Assert.ThrowsException<ArgumentException>(() => _ = new Line(1, 2, 1, 2));
	}

	[TestMethod]
	public void Constructor_FromSlopeAndIntercept_InitializesPoints()
	{
		Line l1 = new(3, 2);
		Assert.AreEqual(new(0, 2), l1.P1);
		Assert.AreEqual(new(1, 5), l1.P2);
	}

	[TestMethod]
	public void Constructor_FromSlopeAndIntercept_InitializesSlopeAndIntercept()
	{
		Line l1 = new(4.5, 2);
		Assert.AreEqual(4.5, l1.Slope);
		Assert.AreEqual(2, l1.YIntercept);
	}
	#endregion

	#region Property Tests
	[TestMethod]
	public void AnchorPoints_AreChangeable()
	{
		Line l1 = new((1, 2), (3, 4))
		{
			P1 = (-5, 4),
			P2 = (7, -2)
		};
		Assert.AreEqual((-5, 4), l1.P1);
		Assert.AreEqual((7, -2), l1.P2);
	}

	[TestMethod]
	public void AnchorPoints_WhenChangedToEqualTheOther_ThrowsError()
	{
		Line l1 = new((1, 2), (3, 4));
		Assert.ThrowsException<ArgumentException>(() => l1.P1 = (3, 4));
		Assert.ThrowsException<ArgumentException>(() => l1.P2 = (1, 2));
	}

	[TestMethod]
	public void Slope_CalculatesCorrectSlope()
	{
		Line l1 = new((1, 3), (2, 5));
		Assert.AreEqual(2, l1.Slope);
	}

	[TestMethod]
	public void Slope_CalculatesFractionalSlopeProperly()
	{
		Line l1 = new((1, 3), (3, 4));
		Assert.AreEqual(0.5, l1.Slope);
	}

	[TestMethod]
	public void Slope_CalculatesNegativeSlopeProperly()
	{
		Line l1 = new((-1, 4), (1, -2));
		Assert.AreEqual(-3, l1.Slope);
	}

	[TestMethod]
	public void Slope_IsPositiveInfinity_ForVerticalLine()
	{
		Line l1 = Line.Vertical;
		Assert.AreEqual(double.PositiveInfinity, l1.Slope);
	}

	[TestMethod]
	public void Slope_IsZero_ForHorizontalLine()
	{
		Line l1 = Line.Horizontal;
		Assert.AreEqual(0, l1.Slope);
	}

	[TestMethod]
	public void YIntercept_ReturnsCorrectNumber()
	{
		Line l1 = new((1, 4), (3, 6));
		Assert.AreEqual(3, l1.YIntercept);
	}

	[TestMethod]
	public void YIntercept_IsZero_WhenLinePassesThroughOrigin()
	{
		Line l1 = new((2, 3));
		Assert.AreEqual(0, l1.YIntercept);
	}

	[TestMethod]
	public void YIntercept_HandlesNegativeInterceptProperly()
	{
		Line l1 = new((3, 4), (4, 6));
		Assert.AreEqual(-2, l1.YIntercept);
	}

	[TestMethod]
	public void XIntercept_ReturnsCorrectNumber()
	{
		Line l1 = new((4, 1), (6, 3));
		Assert.AreEqual(3, l1.XIntercept);
	}

	[TestMethod]
	public void XIntercept_IsZero_WhenLinePassesThroughOrigin()
	{
		Line l1 = new((2, 3));
		Assert.AreEqual(0, l1.XIntercept);
	}

	[TestMethod]
	public void XIntercept_HandlesNegativeInterceptProperly()
	{
		Line l1 = new((1, 4), (3, 6));
		Assert.AreEqual(-3, l1.XIntercept);
	}

	[TestMethod]
	public void IsHorizontal_ReturnsTrue_ForStaticHorizontalLine()
	{
		Line l1 = Line.Horizontal;
		Assert.IsTrue(l1.IsHorizontal);
	}

	[TestMethod]
	public void IsHorizontal_ReturnsTrue_ForHorizontalLineOnAxis()
	{
		Line l1 = new((5, 0));
		Assert.IsTrue(l1.IsHorizontal);
	}

	[TestMethod]
	public void IsHorizontal_ReturnsTrue_ForHorizontalLineOffAxis()
	{
		Line l1 = new((0, 1), (5, 1));
		Assert.IsTrue(l1.IsHorizontal);
	}

	[TestMethod]
	public void IsHorizontal_ReturnsFalse_ForDiagonalLine()
	{
		Line l1 = new((0, 1), (3, 2));
		Assert.IsFalse(l1.IsHorizontal);
	}

	[TestMethod]
	public void IsHorizontal_ReturnsFalse_ForStaticVerticalLine()
	{
		Line l1 = Line.Vertical;
		Assert.IsFalse(l1.IsHorizontal);
	}

	[TestMethod]
	public void IsHorizontal_ReturnsFalse_ForVerticalLine()
	{
		Line l1 = new((0, 5));
		Assert.IsFalse(l1.IsHorizontal);
	}

	[TestMethod]
	public void IsVertical_ReturnsTrue_ForStaticVerticalLine()
	{
		Line l1 = Line.Vertical;
		Assert.IsTrue(l1.IsVertical);
	}

	[TestMethod]
	public void IsVertical_ReturnsTrue_ForVerticalLineOnAxis()
	{
		Line l1 = new((0, 5));
		Assert.IsTrue(l1.IsVertical);
	}

	[TestMethod]
	public void IsVertical_ReturnsTrue_ForVerticalLineOffAxis()
	{
		Line l1 = new((1, 0), (1, 5));
		Assert.IsTrue(l1.IsVertical);
	}

	[TestMethod]
	public void IsVertical_ReturnsFalse_ForDiagonalLine()
	{
		Line l1 = new((0, 1), (3, 2));
		Assert.IsFalse(l1.IsVertical);
	}

	[TestMethod]
	public void IsVertical_ReturnsFalse_ForStaticHorizontalLine()
	{
		Line l1 = Line.Horizontal;
		Assert.IsFalse(l1.IsVertical);
	}

	[TestMethod]
	public void IsVertical_ReturnsFalse_ForHorizontalLine()
	{
		Line l1 = new((5, 0));
		Assert.IsFalse(l1.IsVertical);
	}
	#endregion

	#region Equality Tests
	[TestMethod]
	public void Equality_ReturnsTrue_ForSamePoints()
	{
		Line l1 = new((1, 2), (3, 4));
		Line l2 = new((1, 2), (3, 4));
		Assert.AreEqual(l1, l2);
	}

	[TestMethod]
	public void Equality_ReturnsTrue_ForSamePointsInReverseOrder()
	{
		Line l1 = new((1, 2), (3, 4));
		Line l2 = new((3, 4), (1, 2));
		Assert.AreEqual(l1, l2);
	}

	[TestMethod]
	public void Equality_ReturnsTrue_ForSameLinesWithDifferentPoints()
	{
		Line l1 = new((1, 2), (3, 4));
		Line l2 = new((5, 6), (7, 8));
		Assert.AreEqual(l1, l2);
	}

	[TestMethod]
	public void Equality_ReturnsFalse_ForDifferentLines()
	{
		Line l1 = new((1, 2), (3, 4));
		Line l2 = new((4, 1), (3, 5));
		Assert.AreNotEqual(l1, l2);
	}

	[TestMethod]
	public void Equality_ReturnsFalse_ForDifferentLinesWithSameSlope()
	{
		Line l1 = new((1, 2), (3, 4));
		Line l2 = new((3, 2), (5, 4));
		Assert.AreNotEqual(l1, l2);
	}
	#endregion

	#region Operator Tests
	[TestMethod]
	public void Addition_ReturnsCorrectSum()
	{
		Line l1 = new((2, 1), (4, 2));
		Line result = l1 + new Vector(1, 2);
		Assert.AreEqual(new Line((3, 3), (5, 4)), result);
	}

	[TestMethod]
	public void Addition_ReturnsUnchangedLine_WithZeroVector()
	{
		Line l1 = new((2, 1), (4, 2));
		Assert.AreEqual(l1, l1 + Vector.Zero);
	}

	[TestMethod]
	public void Subtraction_ReturnsCorrectDifference()
	{
		Line l1 = new((2, 1), (4, 2));
		Line result = l1 - new Vector(1, 2);
		Assert.AreEqual(new Line((1, -1), (3, 0)), result);
	}

	[TestMethod]
	public void Subtraction_ReturnsInverseOfAddition()
	{
		Line l1 = new((2, 1), (4, 2));
		Vector v1 = new(1, 2);
		Line result = l1 + v1 - v1;
		Assert.AreEqual(l1, result);
	}
	#endregion

	#region Method Tests
	[TestMethod]
	public void IsParallel_ReturnsTrue_ForSameLines()
	{
		Line l1 = new((1, 2), (3, 4));
		Line l2 = new((5, 6), (7, 8));
		Assert.IsTrue(l1.IsParallelTo(l2));
	}

	[TestMethod]
	public void IsParallel_ReturnsTrue_ForLinesWithEqualSlope()
	{
		Line l1 = new((1, 2), (3, 4));
		Line l2 = new((3, 2), (5, 4));
		Assert.IsTrue(l1.IsParallelTo(l2));
	}

	[TestMethod]
	public void IsParallel_ReturnsTrue_ForLinesWithEqualSlope_WithSlopeConstructor()
	{
		Line l1 = new(8.7, 5);
		Line l2 = new(8.7, -3.5);
		Assert.IsTrue(l1.IsParallelTo(l2));
	}

	[TestMethod]
	public void IsParallel_ReturnsFalse_ForLinesWithDifferentSlope()
	{
		Line l1 = new((1, 2), (3, 4));
		Line l2 = new((1, 5), (4, 3));
		Assert.IsFalse(l1.IsParallelTo(l2));
	}

	[TestMethod]
	public void IsParallel_ReturnsFalse_ForLinesWithDifferentSlope_WithSlopeConstructor()
	{
		Line l1 = new(8.7, 5);
		Line l2 = new(8.6, -3.5);
		Assert.IsFalse(l1.IsParallelTo(l2));
	}

	[TestMethod]
	public void IsPerpendicular_ReturnsTrue_ForPerpendicularLines()
	{
		Line l1 = new((1, 1), (3, 5));
		Line l2 = new((0, 4), (4, 2));
		Assert.IsTrue(l1.IsPerpendicularTo(l2));
	}

	[TestMethod]
	public void IsPerpendicular_ReturnsTrue_ForHorizontalAndVerticalLines()
	{
		Line l1 = Line.Horizontal;
		Line l2 = Line.Vertical;
		Assert.IsTrue(l1.IsPerpendicularTo(l2));
		Assert.IsTrue(l2.IsPerpendicularTo(l1));
	}

	[TestMethod]
	public void IsPerpendicular_ReturnsFalse_WhenLinesAreNotPerpendicular()
	{
		Line l1 = new((1, 2), (3, 4));
		Line l2 = new((2, 3), (5, -2));
		Assert.IsFalse(l1.IsPerpendicularTo(l2));
	}

	[TestMethod]
	public void IsPerpendicular_ReturnsFalse_ForSameLines()
	{
		Line l1 = new((1, 2), (3, 4));
		Line l2 = new((5, 6), (7, 8));
		Assert.IsFalse(l1.IsPerpendicularTo(l2));
	}

	[TestMethod]
	public void IsPerpendicular_ReturnsFalse_ForParallelLines()
	{
		Line l1 = new((1, 2), (3, 4));
		Line l2 = new((3, 2), (5, 4));
		Assert.IsFalse(l1.IsPerpendicularTo(l2));
	}

	[TestMethod]
	public void IsPerpendicular_ReturnsFalse_ForNonPerpendicularLines()
	{
		Line l1 = new((1, 2), (3, 4));
		Line l2 = new((1, 5), (4, 3));
		Assert.IsFalse(l1.IsPerpendicularTo(l2));
	}

	[TestMethod]
	public void Contains_ReturnsTrue_ForPointOnDiagonalLine()
	{
		Line l1 = new((1, 3), (2, 5));
		Point p1 = (3, 7);
		Assert.IsTrue(l1.Contains(p1));
	}

	[TestMethod]
	public void Contains_ReturnsTrue_ForPointOnDiagonalLine_WithNegativeCoordinates()
	{
		Line l1 = new((1, 3), (2, 5));
		Point p1 = (-4, -7);
		Assert.IsTrue(l1.Contains(p1));
	}

	[TestMethod]
	public void Contains_ReturnsTrue_ForAnchorPoint()
	{
		Line l1 = new((1, 3), (2, 5));
		Point p1 = (1, 3);
		Assert.IsTrue(l1.Contains(p1));
	}

	[TestMethod]
	public void Contains_ReturnsTrue_ForOrigin_WithSinglePointConstructor()
	{
		Line l1 = new((2, 5));
		Point p1 = Point.Origin;
		Assert.IsTrue(l1.Contains(p1));
	}

	[TestMethod]
	public void Contains_ReturnsTrue_ForOrigin_WhenYInterceptIsZero()
	{
		Line l1 = new(6.5, 0);
		Point p1 = Point.Origin;
		Assert.IsTrue(l1.Contains(p1));
	}

	[TestMethod]
	public void Contains_ReturnsTrue_ForPointOnHorizontalLine()
	{
		Line l1 = Line.Horizontal;
		Point p1 = (2, 0);
		Assert.IsTrue(l1.Contains(p1));
	}

	[TestMethod]
	public void Contains_ReturnsTrue_ForPointOnVerticalLine()
	{
		Line l1 = Line.Vertical;
		Point p1 = (0, 2);
		Assert.IsTrue(l1.Contains(p1));
	}

	[TestMethod]
	public void Contains_ReturnsFalse_ForPointNotOnDiagonalLine()
	{
		Line l1 = new((1, 3), (2, 5));
		Point p1 = (4, 6);
		Assert.IsFalse(l1.Contains(p1));
	}

	[TestMethod]
	public void Contains_ReturnsFalse_ForPointOnXAxisAndVerticalLine()
	{
		Line l1 = Line.Vertical;
		Point p1 = (5, 0);
		Assert.IsFalse(l1.Contains(p1));
	}

	[TestMethod]
	public void Contains_ReturnsFalse_ForPointOnYAxisAndHorizontalLine()
	{
		Line l1 = Line.Horizontal;
		Point p1 = (0, 5);
		Assert.IsFalse(l1.Contains(p1));
	}

	[TestMethod]
	public void PointAtX_CalculatesCorrectPoint()
	{
		Line l1 = new((1, 1), (2, 3));
		Point? p1 = l1.PointAtX(3);
		Assert.AreEqual((3, 5), p1);
	}

	[TestMethod]
	public void PointAtX_HandlesHorizontalLineOnAxisProperly()
	{
		Line l1 = Line.Horizontal;
		Point? p1 = l1.PointAtX(3);
		Assert.AreEqual((3, 0), p1);
	}

	[TestMethod]
	public void PointAtX_HandlesHorizontalLineOffAxisProperly()
	{
		Line l1 = new((0, 5), (1, 5));
		Point? p1 = l1.PointAtX(3);
		Assert.AreEqual((3, 5), p1);
	}

	[TestMethod]
	public void PointAtX_ReturnsOrigin_ForXZero_WithSinglePointConstructor()
	{
		Line l1 = new((8, 5));
		Point? p1 = l1.PointAtX(0);
		Assert.AreEqual(Point.Origin, p1);
	}

	[TestMethod]
	public void PointAtX_ReturnsNull_ForVerticalLine()
	{
		Line l1 = Line.Vertical;
		Point? p1 = l1.PointAtX(3);
		Assert.IsNull(p1);
	}

	[TestMethod]
	public void PointAtX_ReturnsNull_ForVerticalLineContainingGivenX()
	{
		Line l1 = Line.Vertical;
		Point? p1 = l1.PointAtX(0);
		Assert.IsNull(p1);
	}

	[TestMethod]
	public void PointAtY_CalculatesCorrectPoint()
	{
		Line l1 = new((1, 1), (3, 2));
		Point? p1 = l1.PointAtY(3);
		Assert.AreEqual((5, 3), p1);
	}

	[TestMethod]
	public void PointAtY_HandlesVerticalLineOnAxisProperly()
	{
		Line l1 = Line.Vertical;
		Point? p1 = l1.PointAtY(3);
		Assert.AreEqual((0, 3), p1);
	}

	[TestMethod]
	public void PointAtY_HandlesVerticalLineOffAxisProperly()
	{
		Line l1 = new((5, 0), (5, 1));
		Point? p1 = l1.PointAtY(3);
		Assert.AreEqual((5, 3), p1);
	}

	[TestMethod]
	public void PointAtY_ReturnsOrigin_ForYZero_WithSinglePointConstructor()
	{
		Line l1 = new((8, 5));
		Point? p1 = l1.PointAtY(0);
		Assert.AreEqual(Point.Origin, p1);
	}

	[TestMethod]
	public void PointAtY_ReturnsNull_ForHorizontalLine()
	{
		Line l1 = Line.Horizontal;
		Point? p1 = l1.PointAtY(3);
		Assert.IsNull(p1);
	}

	[TestMethod]
	public void PointAtY_ReturnsNull_ForHorizontalLineContainingGivenX()
	{
		Line l1 = Line.Horizontal;
		Point? p1 = l1.PointAtY(0);
		Assert.IsNull(p1);
	}

	[TestMethod]
	public void Intersection_CalculatesCorrectPoint()
	{
		Line l1 = new((1, 1), (3, 5));
		Line l2 = new((1, 6), (3, 0));
		Point? p1 = l1.Intersection(l2);
		Assert.AreEqual((2, 3), p1);
	}

	[TestMethod]
	public void Intersection_HandlesPerpendicularLinesCorrectly()
	{
		Line l1 = new((1, 1), (3, 5));
		Line l2 = new((0, 4), (4, 2));
		Point? p1 = l1.Intersection(l2);
		Assert.AreEqual((2, 3), p1);
	}

	[TestMethod]
	public void Intersection_ReturnsDuplicatePoint_WhenBothLinesHaveSameAnchorPoint()
	{
		Line l1 = new((6, 7.5), (4, 9));
		Line l2 = new((6, 7.5), (8.5, -2));
		Point? p1 = l1.Intersection(l2);
		Assert.AreEqual((6, 7.5), p1);
	}

	[TestMethod]
	public void Intersection_ReturnsOrigin_ForTwoLinesWithSinglePointConstructor()
	{
		Line l1 = new((8, 5));
		Line l2 = new((-3.5, 8));
		Point? p1 = l1.Intersection(l2);
		Assert.AreEqual(Point.Origin, p1);
	}

	[TestMethod]
	public void Intersection_ReturnsNull_ForEqualLines()
	{
		Line l1 = new((1, 2), (3, 4));
		Line l2 = new((1, 2), (3, 4));
		Point? p1 = l1.Intersection(l2);
		Assert.IsNull(p1);

	}

	[TestMethod]
	public void Intersection_ReturnsNull_ForParallelLines()
	{
		Line l1 = new((1, 2), (3, 4));
		Line l2 = new((3, 2), (5, 4));
		Point? p1 = l1.Intersection(l2);
		Assert.IsNull(p1);
	}

	[TestMethod]
	public void ToOrigin_MovesAnchorPointToOrigin()
	{
		Line l1 = new((1, 2), (3, 4));
		Line result = l1.ToOrigin();
		Assert.AreEqual(Point.Origin, result.P1);
		Assert.AreEqual(new((2, 2)), result);
	}

	[TestMethod]
	public void ToOrigin_LeavesLineUnchanged_WhenLinePassesThroughOrigin()
	{
		Line l1 = new((3, 4));
		Line result = l1.ToOrigin();
		Assert.AreEqual(l1, result);
	}

	[TestMethod]
	public void ToOrigin_MovesSecondAnchorPointToOrigin_WhenArgumentIsSpecified()
	{
		Line l1 = new((1, 2), (3, 4));
		Line result = l1.ToOrigin(p2: true);
		Assert.AreEqual(Point.Origin, result.P2);
		Assert.AreEqual(new((-2, -2)), result);
	}

	[TestMethod]
	public void ToOrigin_ReturnsSameLines_RegardlessOfWhichAnchorPointIsMoved()
	{
		Line l1 = new((1, 2), (3, 4));
		Line result1 = l1.ToOrigin(p2: false);
		Line result2 = l1.ToOrigin(p2: true);
		Assert.AreEqual(result1, result2);
	}

	[TestMethod]
	public void ToOrigin_NormalizesSecondPoint_WhenArgumentIsSpecified()
	{
		Line l1 = new((1, 2), (4, 6));
		Line result = l1.ToOrigin(normalize: true);
		Assert.AreEqual(Point.Origin, result.P1);
		Assert.AreEqual((0.6, 0.8), result.P2);
	}

	[TestMethod]
	public void ToOrigin_NormalizesFirstPoint_WhenArgumentsAreSpecified()
	{
		Line l1 = new((1, 2), (4, 6));
		Line result = l1.ToOrigin(p2: true, normalize: true);
		Assert.AreEqual((-0.6, -0.8), result.P1);
		Assert.AreEqual(Point.Origin, result.P2);
	}

	[TestMethod]
	public void ParallelThrough_CreatesParallelLinePassingThroughPoint()
	{
		Line l1 = new((1, 2), (3, 4));
		Point p1 = (-2, 1);

		Line result = l1.ParallelThrough(p1);

		Assert.IsTrue(result.Contains(p1));
		Assert.IsTrue(result.IsParallelTo(l1));
	}

	[TestMethod]
	public void ParallelThrough_CreatesIdenticalLine_WithPointOnOriginalLine()
	{
		Line l1 = new((1, 2), (3, 4));
		Point p1 = (5, 6);

		Line result = l1.ParallelThrough(p1);

		Assert.AreEqual(l1, result);
	}
	#endregion
}