namespace ClassCluster.Tests;

[TestClass]
public class BoundaryTests
{
	#region Constructor Tests
	[TestMethod]
	public void Constructor_InitializesValues()
	{
		Boundary<double> b1 = new(5, true);
		Boundary<double> b2 = new(8, false);

		Assert.AreEqual(5, b1.Value);
		Assert.IsTrue(b1.Closed);
		Assert.AreEqual(8, b2.Value);
		Assert.IsFalse(b2.Closed);
	}

	[TestMethod]
	public void Constructor_InitializesValue_WithSingleArgument()
	{
		Boundary<double> b1 = new(5);

		Assert.AreEqual(5, b1.Value);
	}

	[TestMethod]
	public void Constructor_InitializesValue_FromDirectValue()
	{
		Boundary<double> b1 = 5;

		Assert.AreEqual(5, b1.Value);
	}

	[TestMethod]
	public void Constructor_ThrowsError_WhenValueIsInfiniteAndClosed()
	{
		Assert.ThrowsException<ArgumentException>(() => _ = new Boundary<double>(double.PositiveInfinity, true));
		Assert.ThrowsException<ArgumentException>(() => _ = new Boundary<double>(double.NegativeInfinity, true));
		Assert.ThrowsException<ArgumentException>(() => _ = new Boundary<float>(float.PositiveInfinity, true));
		Assert.ThrowsException<ArgumentException>(() => _ = new Boundary<float>(float.NegativeInfinity, true));
	}

	[TestMethod]
	public void Constructor_DoesNotThrowError_WhenValueIsInfiniteAndOpen()
	{
		_ = new Boundary<double>(double.PositiveInfinity, false);
		_ = new Boundary<double>(double.NegativeInfinity, false);
		_ = new Boundary<float>(float.PositiveInfinity, false);
		_ = new Boundary<float>(float.NegativeInfinity, false);
	}
	#endregion
}
