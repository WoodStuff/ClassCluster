using Microsoft.VisualStudio.TestTools.UnitTesting;

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
		Assert.ThrowsException<ArgumentException>(() => _ = new Circle(0), "Circle with radius 0 was accepted.");
		Assert.ThrowsException<ArgumentException>(() => _ = new Circle(-2), "Circle with negative radius was accepted.");
	}
	#endregion
}