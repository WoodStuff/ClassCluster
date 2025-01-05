namespace ClassCluster.Tests;

[TestClass]
public class VectorTests
{
	#region Constructor Tests
	[TestMethod]
	public void Constructor_InitializesProperties()
	{
		Vector v1 = new(1, 2.5);
		Assert.AreEqual(1, v1.X);
		Assert.AreEqual(2.5, v1.Y);
	}

	[TestMethod]
	public void PointConstruction_InitializesProperties()
	{
		Point p1 = new(1, 2.5);
		Vector v1 = (Vector)p1;
		Assert.AreEqual(1, v1.X);
		Assert.AreEqual(2.5, v1.Y);
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
	[TestMethod]
	public void Addition_ReturnsCorrectSum()
	{
		Vector v1 = new(1, 1.5);
		Vector v2 = new(3.2, 6);
		Assert.AreEqual(new(4.2, 7.5), v1 + v2);
	}

	[TestMethod]
	public void Addition_HandlesNegativeCoordinatesProperly()
	{
		Vector v1 = new(1, -7.2);
		Vector v2 = new(-4.5, 8);
		Assert.AreEqual(new(-3.5, 0.8), v1 + v2);
	}

	[TestMethod]
	public void Addition_LeavesVectorUnchanged_WithZeroVector()
	{
		Vector v1 = new(5, 8);
		Assert.AreEqual(v1, v1 + Vector.Zero);
		Assert.AreEqual(v1, Vector.Zero + v1);
	}

	[TestMethod]
	public void Subtraction_ReturnsCorrectDifference()
	{
		Vector v1 = new(5, 7);
		Vector v2 = new(1.5, 4);
		Assert.AreEqual(new(3.5, 3), v1 - v2);
	}

	[TestMethod]
	public void Subtraction_HandlesNegativeCoordinatesProperly()
	{
		Vector v1 = new(5.5, 6);
		Vector v2 = new(7, -2.2);
		Assert.AreEqual(new(-1.5, 8.2), v1 - v2);
	}

	[TestMethod]
	public void Subtraction_ReturnsInverseOfAddition()
	{
		Vector v1 = new(2, 4);
		Vector v2 = new(5, 3);
		Vector result = v1 + v2 - v2;
		Assert.AreEqual(v1, result);
	}

	[TestMethod]
	public void Inverse_WorksProperly()
	{
		Vector v1 = new(5.5, -2);
		Vector result = -v1;
		Assert.AreEqual(new(-5.5, 2), result);
	}

	[TestMethod]
	public void Inverse_LeavesZeroVectorUnchanged()
	{
		Vector v1 = Vector.Zero;
		Vector result = -v1;
		Assert.AreEqual(v1, result);
	}

	[TestMethod]
	public void Multiplication_ReturnsCorrectProduct()
	{
		Vector v1 = new(2, 5);
		Vector result = v1 * 3;
		Assert.AreEqual(new(6, 15), result);
	}

	[TestMethod]
	public void Multiplication_HandlesNegativeCoordinatesProperly()
	{
		Vector v1 = new(-2, -5);
		Vector result = v1 * 3;
		Assert.AreEqual(new(-6, -15), result);
	}

	[TestMethod]
	public void Multiplication_HandlesNegativeScalarProperly()
	{
		Vector v1 = new(2, 5);
		Vector result = v1 * -3;
		Assert.AreEqual(new(-6, -15), result);
	}

	[TestMethod]
	public void Multiplication_ReturnsZeroVector_WhenMultiplyingByZero()
	{
		Vector v1 = new(2, 5);
		Vector result = v1 * 0;
		Assert.AreEqual(Vector.Zero, result);
	}

	[TestMethod]
	public void Division_ReturnsCorrectQuotient()
	{
		Vector v1 = new(20, 15);
		Vector result = v1 / 5;
		Assert.AreEqual(new(4, 3), result);
	}

	[TestMethod]
	public void Division_HandlesNegativeCoordinatesProperly()
	{
		Vector v1 = new(-20, -15);
		Vector result = v1 / 5;
		Assert.AreEqual(new(-4, -3), result);
	}

	[TestMethod]
	public void Division_HandlesNegativeScalarProperly()
	{
		Vector v1 = new(20, 15);
		Vector result = v1 / -5;
		Assert.AreEqual(new(-4, -3), result);
	}

	[TestMethod]
	public void Division_ByZero_ThrowsError()
	{
		Point v1 = new(20, 15);
		Assert.ThrowsException<DivideByZeroException>(() => _ = v1 / 0);
	}
	#endregion

	#region Method Tests
	[TestMethod]
	public void Magnitude_ReturnsCorrectMagnitude()
	{
		Vector v1 = new(3, 4);
		double magnitude = v1.Magnitude;
		Assert.AreEqual(5, magnitude);
	}

	[TestMethod]
	public void Magnitude_ReturnsCorrectMagnitude_AtPositiveCoordinates()
	{
		Vector v1 = new(3, 4);
		double magnitude = v1.Magnitude;
		Assert.AreEqual(5, magnitude);
	}

	[TestMethod]
	public void Magnitude_ReturnsCorrectMagnitude_AtNegativeCoordinates()
	{
		Vector v1 = new(-3, -4);
		double magnitude = v1.Magnitude;
		Assert.AreEqual(5, magnitude);
	}

	[TestMethod]
	public void Magnitude_ReturnsCorrectDistance_WhenPointIsOnAxis()
	{
		Vector v1 = new(0, 5);
		double magnitude = v1.Magnitude;
		Assert.AreEqual(5, magnitude);
	}

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

	[TestMethod]
	public void DotProduct_CalculatesCorrectResult()
	{
		Vector v1 = new(3, 5);
		Vector v2 = new(-2, 3);
		double result = v1.Dot(v2);
		Assert.AreEqual(9, result);
	}

	[TestMethod]
	public void DotProduct_ReturnsZero_ForPerpendicularVectors()
	{
		Vector v1 = new(4, 3);
		Vector v2 = new(3, -4);
		double result = v1.Dot(v2);
		Assert.AreEqual(0, result);
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
		double angle = v1.AngleBetween(v2, Angles.Radians);
		Assert.AreEqual(Math.PI / 4, angle, 1e-6);
	}

	[TestMethod]
	public void AngleBetween_CalculatesCorrectAngle_InDegrees()
	{
		Vector v1 = new(5, 0);
		Vector v2 = new(5, 5);
		double angle = v1.AngleBetween(v2, Angles.Degrees);
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
		Assert.ThrowsException<InvalidOperationException>(() => _ = v1.AngleBetween(v2));
		Assert.ThrowsException<InvalidOperationException>(() => _ = v2.AngleBetween(v1));
	}

	[TestMethod]
	public void AngleBetween_ThrowsError_WithTwoZeroVectors()
	{
		Vector v1 = Vector.Zero;
		Assert.ThrowsException<InvalidOperationException>(() => _ = v1.AngleBetween(v1));
	}

	[TestMethod]
	public void Rotate_CalculatesCorrectAngle_InRadians()
	{
		Vector v1 = new(5, -2);
		Vector rotated = v1.RotatedBy(Math.PI / 2, Angles.Radians);
		Assert.AreEqual(new(2, 5), rotated);
	}

	[TestMethod]
	public void Rotate_CalculatesCorrectAngle_InDegrees()
	{
		Vector v1 = new(5, -2);
		Vector rotated = v1.RotatedBy(90, Angles.Degrees);
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
		Vector rotated = v1.RotatedBy(Math.Tau, Angles.Radians);
		Assert.AreEqual(v1, rotated);
	}

	[TestMethod]
	public void Rotate_InvertsVector_WhenRotatingByPiRadians()
	{
		Vector v1 = new(5, -2);
		Vector rotated = v1.RotatedBy(Math.PI, Angles.Radians);
		Assert.AreEqual(-v1, rotated);
	}
	#endregion
}