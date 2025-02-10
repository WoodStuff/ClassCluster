using ClassCluster;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace ClassCluster.Tests;

[TestClass]
public class CircleTests
{
	#region Constructor Tests
	[TestMethod]
	public void Constructor_InitializesProperties()
	{
		Circle c1 = new((-4, 5), 2);

		Assert.AreEqual((-4, 5), c1.Center);
		Assert.AreEqual(2, c1.Radius);
	}

	[TestMethod]
	public void Constructor_FromOnlyRadius_InitializesProperties()
	{
		Circle c1 = new(2);

		Assert.AreEqual(Point.Origin, c1.Center);
		Assert.AreEqual(2, c1.Radius);
	}

	[TestMethod]
	public void Constructor_ThrowsError_WhenRadiusIsNotPositive()
	{
		Assert.ThrowsException<ArgumentException>(() => new Circle(0), "Circle with radius 0 was accepted.");
		Assert.ThrowsException<ArgumentException>(() => new Circle(-2), "Circle with negative radius was accepted.");
	}
	#endregion

	#region Property Tests
	[TestMethod]
	public void Properties_AreChangeable()
	{
		Circle c1 = new((-4, 5), 2)
		{
			Center = (6, 2),
			Radius = 5
		};

		Assert.AreEqual((6, 2), c1.Center);
		Assert.AreEqual(5, c1.Radius);
	}

	[TestMethod]
	public void Radius_ThrowsError_WhenChangedToNonPositiveValue()
	{
		Circle c1 = new((-4, 5), 2);

		Assert.ThrowsException<ArgumentException>(() => c1.Radius = 0, "Circle with radius 0 was accepted.");
		Assert.ThrowsException<ArgumentException>(() => c1.Radius = -2, "Circle with negative radius was accepted.");
	}

	[TestMethod]
	public void Diameter_ReturnsCorrectValue()
	{
		Circle c1 = new(5);

		Assert.AreEqual(10, c1.Diameter);
	}

	[TestMethod]
	public void Diameter_SetsRadiusCorrectly()
	{
		Circle c1 = new(1)
		{
			Diameter = 10
		};

		Assert.AreEqual(5, c1.Radius);
	}

	[TestMethod]
	public void Diameter_ThrowsError_WhenChangedToNonPositiveValue()
	{
		Circle c1 = new(1);

		Assert.ThrowsException<ArgumentException>(() => c1.Diameter = 0, "Circle with diameter 0 was accepted.");
		Assert.ThrowsException<ArgumentException>(() => c1.Diameter = -2, "Circle with negative diameter was accepted.");
	}

	[TestMethod]
	public void Circumference_ReturnsCorrectValue()
	{
		Circle c1 = new(5);

		Assert.AreEqual(5 * Math.Tau, c1.Circumference, 1e-6);
	}

	[TestMethod]
	public void Area_ReturnsCorrectValue()
	{
		Circle c1 = new(5);

		Assert.AreEqual(25 * Math.PI, c1.Area, 1e-6);
	}
	#endregion

	#region Equality Tests
	[TestMethod]
	public void Equality_ReturnsTrue_ForEqualCentersAndRadii()
	{
		Circle c1 = new((4, 1), 3);
		Circle c2 = new((4, 1), 3);

		Assert.AreEqual(c1, c2);
	}

	[TestMethod]
	public void Equality_ReturnsFalse_ForDifferentCenters()
	{
		Circle c1 = new((4, 2), 3);
		Circle c2 = new((4, 1), 3);

		Assert.AreNotEqual(c1, c2);
	}

	[TestMethod]
	public void Equality_ReturnsFalse_ForDifferentRadii()
	{
		Circle c1 = new((4, 1), 4);
		Circle c2 = new((4, 1), 3);

		Assert.AreNotEqual(c1, c2);
	}

	[TestMethod]
	public void Equality_ReturnsFalse_ForDifferentCentersAndRadii()
	{
		Circle c1 = new((4, 2), 4);
		Circle c2 = new((4, 1), 3);

		Assert.AreNotEqual(c1, c2);
	}

	[TestMethod]
	public void Equality_ReturnsFalse_ForNull()
	{
		Circle c1 = new((4, 1), 3);

		Assert.AreNotEqual(null, c1);
	}
	#endregion

	#region Operator Tests
	[TestMethod]
	public void Addition_WithVector_MovesCircle()
	{
		Circle c1 = new((4, 1), 3);
		Vector v1 = new(1, 2);

		Circle result = c1 + v1;

		Assert.AreEqual(new((5, 3), 3), result);
	}

	[TestMethod]
	public void Addition_WithVector_IncreasesRadius()
	{
		Circle c1 = new((4, 1), 3);

		Circle result = c1 + 1.2;

		Assert.AreEqual(new((4, 1), 4.2), result);
	}

	[TestMethod]
	public void Subtraction_WithVector_MovesCircle()
	{
		Circle c1 = new((4, 1), 3);
		Vector v1 = new(1, 2);

		Circle result = c1 - v1;

		Assert.AreEqual(new((3, -1), 3), result);
	}

	[TestMethod]
	public void Subtraction_WithVector_DecreasesRadius()
	{
		Circle c1 = new((4, 1), 3);

		Circle result = c1 - 1.2;

		Assert.AreEqual(new((4, 1), 1.8), result);
	}
	#endregion

	#region Method Tests
	[TestMethod]
	public void Distance_CalculatesCorrectValue_ForPointOutsideCircle()
	{
		Circle c1 = new((4, 1), 3);
		Point p1 = (-1, 1);

		Assert.AreEqual(2, c1.Distance(p1));
	}

	[TestMethod]
	public void Distance_CalculatesCorrectValue_ForPointInsideCircle()
	{
		Circle c1 = new((4, 1), 3);
		Point p1 = (4, 2);

		Assert.AreEqual(-2, c1.Distance(p1));
	}

	[TestMethod]
	public void Distance_ReturnsZero_ForPointOnCircle()
	{
		Circle c1 = new((4, 1), 3);
		Point p1 = (7, 1);

		Assert.AreEqual(0, c1.Distance(p1));
	}

	[TestMethod]
	public void Distance_ReturnsNegativeRadius_ForCenter()
	{
		Circle c1 = new((4, 1), 3);
		Point p1 = c1.Center;

		Assert.AreEqual(-c1.Radius, c1.Distance(p1));
	}

	[TestMethod]
	public void LocatePoint_ReturnsOutside_ForPointOutsideCircle()
	{
		Circle c1 = new((4, 1), 3);
		Point p1 = (-1, 1);

		Assert.AreEqual(Position.Outside, c1.Locate(p1));
	}

	[TestMethod]
	public void LocatePoint_ReturnsInside_ForPointInsideCircle()
	{
		Circle c1 = new((4, 1), 3);
		Point p1 = (4, 2);

		Assert.AreEqual(Position.Inside, c1.Locate(p1));
	}

	[TestMethod]
	public void LocatePoint_ReturnsInside_ForCenter()
	{
		Circle c1 = new((4, 1), 3);
		Point p1 = c1.Center;

		Assert.AreEqual(Position.Inside, c1.Locate(p1));
	}

	[TestMethod]
	public void LocatePoint_ReturnsOn_ForPointOnCircle()
	{
		Circle c1 = new((4, 1), 3);
		Point p1 = (7, 1);

		Assert.AreEqual(Position.On, c1.Locate(p1));
	}
	#endregion
}