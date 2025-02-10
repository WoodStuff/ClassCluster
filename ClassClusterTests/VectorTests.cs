namespace ClassCluster.Tests;

[TestClass]
public class VectorTests
{
	#region Constructor Tests
	[DataTestMethod]
	[DataRow(1, 2.5)]
	[DataRow(-5.2, 0)]
	public void Constructor_InitializesProperties(double x, double y)
	{
		Vector v1 = new(x, y);
		Assert.AreEqual(x, v1.X);
		Assert.AreEqual(y, v1.Y);
	}

	[DataTestMethod]
	[DataRow(1, 2.5)]
	[DataRow(-5.2, 0)]
	public void PointConstruction_InitializesProperties(double x, double y)
	{
		Point p1 = new(x, y);
		Vector v1 = (Vector)p1;
		Assert.AreEqual(x, v1.X);
		Assert.AreEqual(y, v1.Y);
	}
	#endregion

	#region Property Tests
	[DataTestMethod]
	[DataRow(3, 4, 5)]
	[DataRow(-3, -4, 5)]
	[DataRow(0, 5, 5)]
	public void Magnitude_ReturnsCorrectMagnitude(double x, double y, double expected)
	{
		Vector v1 = new(x, y);
		double magnitude = v1.Magnitude;
		Assert.AreEqual(expected, magnitude);
	}

	[DataTestMethod]
	[DataRow(3, 0, 0)]
	[DataRow(2.5, 2.5, Math.PI * 0.25)]
	[DataRow(0, 3, Math.PI * 0.5)]
	[DataRow(-3, 0, Math.PI)]
	[DataRow(0, -3, Math.PI * 1.5)]
	public void Theta_ReturnsCorrectAngle(double x, double y, double expected)
	{
		Vector v1 = new(x, y);
		double theta = v1.Theta;
		Assert.AreEqual(expected, theta);
	}
	#endregion

	#region Equality Tests
	[TestMethod]
	public void Equality_ReturnsTrue_ForSameValues()
	{
		Vector v1 = new(6, -8);
		Vector v2 = new(6, -8);
		Assert.AreEqual(v1, v2);
	}

	[TestMethod]
	public void Equality_ReturnsFalse_ForDifferentValues()
	{
		Vector v1 = new(6, -8);
		Vector v2 = new(7, -8);
		Assert.AreNotEqual(v1, v2);
	}

	[TestMethod]
	public void Equality_ReturnsFalse_ForNull()
	{
		Vector v1 = new(6, -8);
		Assert.AreNotEqual(null, v1);
	}
	#endregion

	#region Operator Tests
	[DataTestMethod]
	[DataRow(1, 1.5, 3.2, 6)]
	[DataRow(1, -7.2, -4.5, 8)]
	public void Addition_ReturnsCorrectSum(double x1, double y1, double x2, double y2)
	{
		Vector v1 = new(x1, y1);
		Vector v2 = new(x2, y2);
		Assert.AreEqual(new(x1 + x2, y1 + y2), v1 + v2);
	}

	[TestMethod]
	public void Addition_LeavesVectorUnchanged_WithZeroVector()
	{
		Vector v1 = new(5, 8);
		Assert.AreEqual(v1, v1 + Vector.Zero);
		Assert.AreEqual(v1, Vector.Zero + v1);
	}

	[DataTestMethod]
	[DataRow(5, 7, 1.5, 4)]
	[DataRow(5.5, 6, 7, -2.2)]
	public void Subtraction_ReturnsCorrectDifference(double x1, double y1, double x2, double y2)
	{
		Vector v1 = new(x1, y1);
		Vector v2 = new(x2, y2);
		Assert.AreEqual(new(x1 - x2, y1 - y2), v1 - v2);
	}

	[TestMethod]
	public void Subtraction_ReturnsInverseOfAddition()
	{
		Vector v1 = new(2, 4);
		Vector v2 = new(5, 3);
		Vector result = v1 + v2 - v2;
		Assert.AreEqual(v1, result);
	}

	[DataTestMethod]
	[DataRow(5.5, -2)]
	[DataRow(-4, 0)]
	public void Inverse_WorksProperly(double x, double y)
	{
		Vector v1 = new(x, y);
		Vector result = -v1;
		Assert.AreEqual(new(-x, -y), result);
	}

	[TestMethod]
	public void Inverse_LeavesZeroVectorUnchanged()
	{
		Vector v1 = Vector.Zero;
		Vector result = -v1;
		Assert.AreEqual(v1, result);
	}

	[DataTestMethod]
	[DataRow(2, 5, 3)]
	[DataRow(-2, -5, 3)]
	[DataRow(2, 5, -3)]
	public void Multiplication_ReturnsCorrectProduct(double x, double y, double scalar)
	{
		Vector v1 = new(x, y);
		Vector result = v1 * scalar;
		Assert.AreEqual(new(x * scalar, y * scalar), result);
	}

	[TestMethod]
	public void Multiplication_ReturnsZeroVector_WhenMultiplyingByZero()
	{
		Vector v1 = new(2, 5);
		Vector result = v1 * 0;
		Assert.AreEqual(Vector.Zero, result);
	}

	[DataTestMethod]
	[DataRow(20, 15, 5)]
	[DataRow(-20, -15, 5)]
	[DataRow(20, 15, -5)]
	public void Division_ReturnsCorrectQuotient(double x, double y, double scalar)
	{
		Vector v1 = new(x, y);
		Vector result = v1 / scalar;
		Assert.AreEqual(new(x / scalar, y / scalar), result);
	}

	[TestMethod]
	public void Division_ByZero_ThrowsError()
	{
		Point v1 = new(20, 15);
		Assert.ThrowsException<DivideByZeroException>(() => v1 / 0);
	}
	#endregion

	#region Method Tests
	[TestMethod]
	public void Normalize_ReturnsNormalizedVector()
	{
		Vector v1 = new(4, 3);
		Assert.AreEqual(new(0.8, 0.6), v1.ToNormalized());
	}

	[TestMethod]
	public void Normalize_LeavesUnitVectorsUnchanged()
	{
		Vector v1 = new(1, 0);
		Vector v2 = new(0, 1);

		Assert.AreEqual(v1, v1.ToNormalized());
		Assert.AreEqual(v2, v2.ToNormalized());
	}

	[TestMethod]
	public void Normalize_LeavesZeroVectorUnchanged()
	{
		Vector v1 = Vector.Zero;
		Assert.AreEqual(v1, v1.ToNormalized());
	}

	[TestMethod]
	public void Normalize_IsEquivalentToNormalized_ForSameVector()
	{
		Vector v1 = new(4, 7);
		Vector v2 = v1.ToNormalized();

		v1.Normalize();

		Assert.AreEqual(v1, v2);
	}

	[DataTestMethod]
	[DataRow(3, 5, -2, 3, 9)]
	[DataRow(4, 3, 3, -4, 0)]
	public void DotProduct_CalculatesCorrectResult(double x1, double y1, double x2, double y2, double expected)
	{
		Vector v1 = new(x1, y1);
		Vector v2 = new(x2, y2);
		double result = v1.Dot(v2);
		Assert.AreEqual(expected, result);
	}

	[TestMethod]
	public void DotProduct_ReturnsZero_WithZeroVector()
	{
		Vector v1 = new(6, -8.5);
		Vector v2 = Vector.Zero;
		double result = v1.Dot(v2);
		Assert.AreEqual(0, result);
	}

	[TestMethod]
	public void DotProduct_OperatorWorksProperly()
	{
		Vector v1 = new(3, 5);
		Vector v2 = new(-2, 3);
		double result = v1 * v2;
		Assert.AreEqual(9, result);
	}

	[TestMethod]
	public void AngleBetween_CalculatesCorrectAngle_InRadians()
	{
		Vector v1 = new(5, 0);
		Vector v2 = new(5, 5);
		double angle = v1.AngleBetween(v2, AngleUnit.Radians);
		Assert.AreEqual(Math.PI / 4, angle, 1e-6);
	}

	[TestMethod]
	public void AngleBetween_CalculatesCorrectAngle_InDegrees()
	{
		Vector v1 = new(5, 0);
		Vector v2 = new(5, 5);
		double angle = v1.AngleBetween(v2, AngleUnit.Degrees);
		Assert.AreEqual(45, angle, 1e-6);
	}

	[TestMethod]
	public void AngleBetween_ReturnsZero_WhenVectorsAreEqual()
	{
		Vector v1 = new(5, 3);
		Vector v2 = new(5, 3);
		double angle = v1.AngleBetween(v2);
		Assert.AreEqual(0, angle);
	}

	[TestMethod]
	public void AngleBetween_ReturnsZero_WhenVectorsAreParallel()
	{
		Vector v1 = new(6, 2);
		Vector v2 = new(9, 3);
		double angle = v1.AngleBetween(v2);
		Assert.AreEqual(0, angle, 1e-6);
	}

	[TestMethod]
	public void AngleBetween_ReturnsHalfPiRadians_WhenVectorsArePerpendicular()
	{
		Vector v1 = new(4, 3);
		Vector v2 = new(3, -4);
		double angle = v1.AngleBetween(v2);
		Assert.AreEqual(Math.PI / 2, angle, 1e-6);
	}

	[TestMethod]
	public void AngleBetween_ReturnsPiRadians_WhenVectorsAreNegativesOfEachOther()
	{
		Vector v1 = new(5, 3);
		Vector v2 = -v1;
		double angle = v1.AngleBetween(v2);
		Assert.AreEqual(Math.PI, angle, 1e-6);
	}

	[TestMethod]
	public void AngleBetween_ReturnsPiRadians_WhenVectorsPointInOppositeDirections()
	{
		Vector v1 = new(6, 2);
		Vector v2 = new(-9, -3);
		double angle = v1.AngleBetween(v2);
		Assert.AreEqual(Math.PI, angle, 1e-6);
	}

	[TestMethod]
	public void AngleBetween_ThrowsError_WithZeroVector()
	{
		Vector v1 = new(5, 2);
		Vector v2 = Vector.Zero;
		Assert.ThrowsException<InvalidOperationException>(() => v1.AngleBetween(v2));
		Assert.ThrowsException<InvalidOperationException>(() => v2.AngleBetween(v1));
	}

	[TestMethod]
	public void AngleBetween_ThrowsError_WithTwoZeroVectors()
	{
		Vector v1 = Vector.Zero;
		Assert.ThrowsException<InvalidOperationException>(() => v1.AngleBetween(v1));
	}

	[TestMethod]
	public void Rotate_CalculatesCorrectAngle_InRadians()
	{
		Vector v1 = new(5, -2);
		Vector rotated = v1.RotatedBy(Math.PI / 2, AngleUnit.Radians);
		Assert.AreEqual(new(2, 5), rotated);
	}

	[TestMethod]
	public void Rotate_CalculatesCorrectAngle_InDegrees()
	{
		Vector v1 = new(5, -2);
		Vector rotated = v1.RotatedBy(90, AngleUnit.Degrees);
		Assert.AreEqual(new(2, 5), rotated);
	}

	[TestMethod]
	public void Rotate_LeavesVectorUnchanged_WhenRotatingByZero()
	{
		Vector v1 = new(5, -2);
		Vector rotated = v1.RotatedBy(0);
		Assert.AreEqual(v1, rotated);
	}

	[TestMethod]
	public void Rotate_LeavesVectorUnchanged_WhenRotatingByTauRadians()
	{
		Vector v1 = new(5, -2);
		Vector rotated = v1.RotatedBy(Math.Tau, AngleUnit.Radians);
		Assert.AreEqual(v1, rotated);
	}

	[TestMethod]
	public void Rotate_InvertsVector_WhenRotatingByPiRadians()
	{
		Vector v1 = new(5, -2);
		Vector rotated = v1.RotatedBy(Math.PI, AngleUnit.Radians);
		Assert.AreEqual(-v1, rotated);
	}
	#endregion
}