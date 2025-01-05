namespace ClassCluster.Tests;

[TestClass]
public class PointTests
{
	#region Constructor Tests
	[TestMethod]
	public void Constructor_InitializesProperties()
	{
		Point p1 = new(1, 2.5);
		Assert.AreEqual(1, p1.X);
		Assert.AreEqual(2.5, p1.Y);
	}

	[TestMethod]
	public void TupleConstruction_InitializesProperties()
	{
		Point p1 = (1, 2.5);
		Assert.AreEqual(1, p1.X);
		Assert.AreEqual(2.5, p1.Y);
	}

	[TestMethod]
	public void VectorConstruction_InitializesProperties()
	{
		Vector v1 = new(1, 2.5);
		Point p1 = (Point)v1;
		Assert.AreEqual(1, p1.X);
		Assert.AreEqual(2.5, p1.Y);
	}
	#endregion

	#region Equality Tests
	[TestMethod]
	public void Equality_ReturnsTrue_ForSameValues()
	{
		Point p1 = new(6, -8);
		Point p2 = new(6, -8);
		Assert.AreEqual(p1, p2);
	}

	[TestMethod]
	public void Equality_ReturnsFalse_ForDifferentValues()
	{
		Point p1 = new(6, -8);
		Point p2 = new(7, -8);
		Assert.AreNotEqual(p1, p2);
	}

	[TestMethod]
	public void Equality_ReturnsFalse_ForNull()
	{
		Point p1 = new(6, -8);
		Assert.AreNotEqual(null, p1);
	}
	#endregion

	#region Operator Tests
	[TestMethod]
	public void Addition_ReturnsCorrectSum()
	{
		Point p1 = new(1, 1.5);
		Point p2 = new(3.2, 6);
		Assert.AreEqual(new(4.2, 7.5), p1 + p2);
	}

	[TestMethod]
	public void Addition_HandlesNegativeCoordinatesProperly()
	{
		Point p1 = new(1, -7.2);
		Point p2 = new(-4.5, 8);
		Assert.AreEqual(new(-3.5, 0.8), p1 + p2);
	}

	[TestMethod]
	public void Addition_LeavesPointUnchanged_WithOrigin()
	{
		Point p1 = new(5, 8);
		Assert.AreEqual(p1, p1 + Point.Origin);
		Assert.AreEqual(p1, Point.Origin + p1);
	}

	[TestMethod]
	public void Subtraction_ReturnsCorrectDifference()
	{
		Point p1 = new(5, 7);
		Point p2 = new(1.5, 4);
		Assert.AreEqual(new(3.5, 3), p1 - p2);
	}

	[TestMethod]
	public void Subtraction_HandlesNegativeCoordinatesProperly()
	{
		Point p1 = new(5.5, 6);
		Point p2 = new(7, -2.2);
		Assert.AreEqual(new(-1.5, 8.2), p1 - p2);
	}

	[TestMethod]
	public void Subtraction_ReturnsInverseOfAddition()
	{
		Point p1 = new(2, 4);
		Point p2 = new(5, 3);
		Point result = p1 + p2 - p2;
		Assert.AreEqual(p1, result);
	}

	[TestMethod]
	public void Inverse_WorksProperly()
	{
		Point p1 = new(5.5, -2);
		Point result = -p1;
		Assert.AreEqual(new(-5.5, 2), result);
	}

	[TestMethod]
	public void Inverse_LeavesOriginUnchanged()
	{
		Point p1 = Point.Origin;
		Point result = -p1;
		Assert.AreEqual(p1, result);
	}

	[TestMethod]
	public void Multiplication_ReturnsCorrectProduct()
	{
		Point p1 = new(2, 5);
		Point result = p1 * 3;
		Assert.AreEqual(new(6, 15), result);
	}

	[TestMethod]
	public void Multiplication_HandlesNegativeCoordinatesProperly()
	{
		Point p1 = new(-2, -5);
		Point result = p1 * 3;
		Assert.AreEqual(new(-6, -15), result);
	}

	[TestMethod]
	public void Multiplication_HandlesNegativeScalarProperly()
	{
		Point p1 = new(2, 5);
		Point result = p1 * -3;
		Assert.AreEqual(new(-6, -15), result);
	}

	[TestMethod]
	public void Multiplication_ReturnsOrigin_WhenMultiplyingByZero()
	{
		Point p1 = new(2, 5);
		Point result = p1 * 0;
		Assert.AreEqual(Point.Origin, result);
	}

	[TestMethod]
	public void Division_ReturnsCorrectQuotient()
	{
		Point p1 = new(20, 15);
		Point result = p1 / 5;
		Assert.AreEqual(new(4, 3), result);
	}

	[TestMethod]
	public void Division_HandlesNegativeCoordinatesProperly()
	{
		Point p1 = new(-20, -15);
		Point result = p1 / 5;
		Assert.AreEqual(new(-4, -3), result);
	}

	[TestMethod]
	public void Division_HandlesNegativeScalarProperly()
	{
		Point p1 = new(20, 15);
		Point result = p1 / -5;
		Assert.AreEqual(new(-4, -3), result);
	}

	[TestMethod]
	public void Division_ByZero_ThrowsError()
	{
		Point p1 = new(20, 15);
		Assert.ThrowsException<DivideByZeroException>(() => _ = p1 / 0);
	}
	#endregion

	#region Method Tests
	[TestMethod]
	public void DistanceFromOrigin_ReturnsCorrectDistance_AtPositiveCoordinates()
	{
		Point p1 = new(3, 4);
		double distance = p1.DistanceFromOrigin();
		Assert.AreEqual(5, distance);
	}

	[TestMethod]
	public void DistanceFromOrigin_ReturnsCorrectDistance_AtNegativeCoordinates()
	{
		Point p1 = new(-3, -4);
		double distance = p1.DistanceFromOrigin();
		Assert.AreEqual(5, distance);
	}

	[TestMethod]
	public void DistanceFromOrigin_ReturnsCorrectDistance_WhenPointIsOnAxis()
	{
		Point p1 = new(0, 5);
		double distance = p1.DistanceFromOrigin();
		Assert.AreEqual(5, distance);
	}

	[TestMethod]
	public void DistanceToPoint_ReturnsCorrectDistance()
	{
		Point p1 = new(2, 6);
		Point p2 = new(6, 9);
		double distance = p1.Distance(p2);
		Assert.AreEqual(5, distance);
	}

	[TestMethod]
	public void DistanceToPoint_ReturnsCorrectDistance_WhenPointsHaveDifferentSigns()
	{
		Point p1 = new(3, -2);
		Point p2 = new(-1, 1);
		double distance = p1.Distance(p2);
		Assert.AreEqual(5, distance);
	}

	[TestMethod]
	public void DistanceToPoint_ReturnsCorrectDistance_WhenPointsAreOnAxes()
	{
		Point p1 = new(0, -2);
		Point p2 = new(0, 3);
		double distance = p1.Distance(p2);
		Assert.AreEqual(5, distance);
	}

	[TestMethod]
	public void DistanceFromOrigin_IsEquivalentToDistanceToOriginPoint()
	{
		Point p1 = new(3, 4);
		double d1 = p1.DistanceFromOrigin();
		double d2 = p1.Distance(Point.Origin);
		Assert.AreEqual(d1, d2);
	}

	[TestMethod]
	public void GridDistFromOrigin_ReturnsCorrectDistance_AtPositiveCoordinates()
	{
		Point p1 = new(3, 6);
		double distance = p1.GridDistFromOrigin();
		Assert.AreEqual(9, distance);
	}

	[TestMethod]
	public void GridDistFromOrigin_ReturnsCorrectDistance_AtNegativeCoordinates()
	{
		Point p1 = new(-3, -6);
		double distance = p1.GridDistFromOrigin();
		Assert.AreEqual(9, distance);
	}

	[TestMethod]
	public void GridDistFromOrigin_ReturnsCorrectDistance_WhenPointIsOnAxis()
	{
		Point p1 = new(9, 0);
		double distance = p1.GridDistFromOrigin();
		Assert.AreEqual(9, distance);
	}

	[TestMethod]
	public void GridDistToPoint_ReturnsCorrectDistance()
	{
		Point p1 = new(3, 1);
		Point p2 = new(8, 5);
		double distance = p1.GridDist(p2);
		Assert.AreEqual(9, distance);
	}

	[TestMethod]
	public void GridDistToPoint_ReturnsCorrectDistance_WhenPointsHaveDifferentSigns()
	{
		Point p1 = new(3, 4);
		Point p2 = new(-1, -1);
		double distance = p1.GridDist(p2);
		Assert.AreEqual(9, distance);
	}

	[TestMethod]
	public void GridDistToPoint_ReturnsCorrectDistance_WhenPointsAreOnAxes()
	{
		Point p1 = new(6, 0);
		Point p2 = new(0, -3);
		double distance = p1.GridDist(p2);
		Assert.AreEqual(9, distance);
	}

	[TestMethod]
	public void GridDistFromOrigin_IsEquivalentToDistanceToOriginPoint()
	{
		Point p1 = new(7, 2);
		double d1 = p1.GridDistFromOrigin();
		double d2 = p1.GridDist(Point.Origin);
		Assert.AreEqual(d1, d2);
	}

	[TestMethod]
	public void Normalize_ReturnsNormalizedPoint()
	{
		Point p1 = new(4, 3);
		Assert.AreEqual(new(0.8, 0.6), p1.ToNormalized());
	}

	[TestMethod]
	public void Normalize_LeavesUnitPointsUnchanged()
	{
		Point p1 = new(1, 0);
		Point p2 = new(0, 1);

		Assert.AreEqual(p1, p1.ToNormalized());
		Assert.AreEqual(p2, p2.ToNormalized());
	}

	[TestMethod]
	public void Normalize_LeavesOriginUnchanged()
	{
		Point p1 = Point.Origin;
		Assert.AreEqual(p1, p1.ToNormalized());
	}

	[TestMethod]
	public void Normalize_IsEquivalentToNormalized_ForSamePoint()
	{
		Point p1 = new(4, 7);
		Point p2 = p1.ToNormalized();

		p1.Normalize();

		Assert.AreEqual(p1, p2);
	}
	#endregion
}