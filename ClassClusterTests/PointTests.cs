namespace ClassCluster.Tests;

[TestClass]
public class PointTests
{
	#region Constructor Tests
	[DataTestMethod]
	[DataRow(1, 2.5)]
	[DataRow(-5.2, 0)]
	public void Constructor_InitializesProperties(double x, double y)
	{
		Point p1 = new(x, y);
		Assert.AreEqual(x, p1.X);
		Assert.AreEqual(y, p1.Y);
	}

	[DataTestMethod]
	[DataRow(1, 2.5)]
	[DataRow(-5.2, 0)]
	public void TupleConstruction_InitializesProperties(double x, double y)
	{
		Point p1 = (x, y);
		Assert.AreEqual(x, p1.X);
		Assert.AreEqual(y, p1.Y);
	}

	[DataTestMethod]
	[DataRow(1, 2.5)]
	[DataRow(-5.2, 0)]
	public void VectorConstruction_InitializesProperties(double x, double y)
	{
		Vector v1 = new(x, y);
		Point p1 = (Point)v1;
		Assert.AreEqual(x, p1.X);
		Assert.AreEqual(y, p1.Y);
	}
	#endregion

	#region Property Tests
	[DataTestMethod]
	[DataRow(3, 4, 5)]
	[DataRow(-3, -4, 5)]
	[DataRow(0, 5, 5)]
	public void DistanceFromOrigin_ReturnsCorrectDistance(double x, double y, double expected)
	{
		Point p1 = new(x, y);
		double distance = p1.DistanceFromOrigin;
		Assert.AreEqual(expected, distance);
	}

	[DataTestMethod]
	[DataRow(3, 6, 9)]
	[DataRow(-3, -6, 9)]
	[DataRow(9, 0, 9)]
	public void GridDistFromOrigin_ReturnsCorrectDistance(double x, double y, double expected)
	{
		Point p1 = new(x, y);
		double distance = p1.GridDistFromOrigin;
		Assert.AreEqual(expected, distance);
	}

	[DataTestMethod]
	[DataRow(3, 0, 0)]
	[DataRow(2.5, 2.5, Math.PI * 0.25)]
	[DataRow(0, 3, Math.PI * 0.5)]
	[DataRow(-3, 0, Math.PI)]
	[DataRow(0, -3, Math.PI * 1.5)]
	public void Theta_ReturnsCorrectAngle(double x, double y, double expected)
	{
		Point p1 = new(x, y);
		double theta = p1.Theta;
		Assert.AreEqual(expected, theta);
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
	#endregion

	#region Operator Tests
	[DataTestMethod]
	[DataRow(1, 1.5, 3.2, 6)]
	[DataRow(1, -7.2, -4.5, 8)]
	public void Addition_ReturnsCorrectSum(double x1, double y1, double x2, double y2)
	{
		Point p1 = new(x1, y1);
		Point p2 = new(x2, y2);
		Assert.AreEqual(new(x1 + x2, y1 + y2), p1 + p2);
	}

	[TestMethod]
	public void Addition_LeavesPointUnchanged_WithOrigin()
	{
		Point p1 = new(5, 8);
		Assert.AreEqual(p1, p1 + Point.Origin);
		Assert.AreEqual(p1, Point.Origin + p1);
	}

	[DataTestMethod]
	[DataRow(5, 7, 1.5, 4)]
	[DataRow(5.5, 6, 7, -2.2)]
	public void Subtraction_ReturnsCorrectDifference(double x1, double y1, double x2, double y2)
	{
		Point p1 = new(x1, y1);
		Point p2 = new(x2, y2);
		Assert.AreEqual(new(x1 - x2, y1 - y2), p1 - p2);
	}

	[TestMethod]
	public void Subtraction_ReturnsInverseOfAddition()
	{
		Point p1 = new(2, 4);
		Point p2 = new(5, 3);
		Point result = p1 + p2 - p2;
		Assert.AreEqual(p1, result);
	}

	[DataTestMethod]
	[DataRow(5.5, -2)]
	[DataRow(-4, 0)]
	public void Inverse_WorksProperly(double x, double y)
	{
		Point p1 = new(x, y);
		Point result = -p1;
		Assert.AreEqual(new(-x, -y), result);
	}

	[TestMethod]
	public void Inverse_LeavesOriginUnchanged()
	{
		Point p1 = Point.Origin;
		Point result = -p1;
		Assert.AreEqual(p1, result);
	}

	[DataTestMethod]
	[DataRow(2, 5, 3)]
	[DataRow(-2, -5, 3)]
	[DataRow(2, 5, -3)]
	public void Multiplication_ReturnsCorrectProduct(double x, double y, double scalar)
	{
		Point p1 = new(x, y);
		Point result = p1 * scalar;
		Assert.AreEqual(new(x * scalar, y * scalar), result);
	}

	[TestMethod]
	public void Multiplication_ReturnsOrigin_WhenMultiplyingByZero()
	{
		Point p1 = new(2, 5);
		Point result = p1 * 0;
		Assert.AreEqual(Point.Origin, result);
	}

	[DataTestMethod]
	[DataRow(20, 15, 5)]
	[DataRow(-20, -15, 5)]
	[DataRow(20, 15, -5)]
	public void Division_ReturnsCorrectQuotient(double x, double y, double scalar)
	{
		Point p1 = new(x, y);
		Point result = p1 / scalar;
		Assert.AreEqual(new(x / scalar, y / scalar), result);
	}

	[TestMethod]
	public void Division_ByZero_ThrowsError()
	{
		Point p1 = new(20, 15);
		Assert.ThrowsException<DivideByZeroException>(() => p1 / 0);
	}
	#endregion

	#region Method Tests
	[DataTestMethod]
	[DataRow(2, 6, 6, 9, 5)]
	[DataRow(3, -2, -1, 1, 5)]
	[DataRow(0, -2, 0, 3, 5)]
	public void DistanceToPoint_ReturnsCorrectDistance(double x1, double y1, double x2, double y2, double expected)
	{
		Point p1 = new(x1, y1);
		Point p2 = new(x2, y2);
		double distance = p1.Distance(p2);
		Assert.AreEqual(expected, distance);
	}

	[TestMethod]
	public void DistanceFromOrigin_IsEquivalentToDistanceToOriginPoint()
	{
		Point p1 = new(3, 4);
		double d1 = p1.DistanceFromOrigin;
		double d2 = p1.Distance(Point.Origin);
		Assert.AreEqual(d1, d2);
	}

	[DataTestMethod]
	[DataRow(3, 1, 8, 5, 9)]
	[DataRow(3, 4, -1, -1, 9)]
	[DataRow(6, 0, 0, -3, 9)]
	public void GridDistToPoint_ReturnsCorrectDistance(double x1, double y1, double x2, double y2, double expected)
	{
		Point p1 = new(x1, y1);
		Point p2 = new(x2, y2);
		double distance = p1.GridDist(p2);
		Assert.AreEqual(expected, distance);
	}

	[TestMethod]
	public void GridDistFromOrigin_IsEquivalentToDistanceToOriginPoint()
	{
		Point p1 = new(7, 2);
		double d1 = p1.GridDistFromOrigin;
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