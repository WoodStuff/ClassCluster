using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClassCluster.Tests;

[TestClass]
public class SetTests
{
	#region Constructor Tests
	[TestMethod]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0028:Simplify collection initialization")]
	public void Constructor_InitializesEmptySet_WithNoArguments()
    {
        Set s1 = new();

        Assert.AreEqual(0, s1.Count);
    }

	[TestMethod]
	public void Constructor_InitializesSet()
	{
		Set s1 = new(4, 5.5, -7, 1);

		Assert.AreEqual(4, s1.Count);
	}

	[TestMethod]
	public void Constructor_RemovesDuplicateElements()
	{
		Set s1 = new(1, 5, 5, 8, 5, 2, 5, 2);

		Assert.AreEqual(4, s1.Count);
	}

	[TestMethod]
	public void Constructor_InitializesSet_ThroughArray()
	{
		Set s1 = [4, 5.5, -7, 1];

		Assert.AreEqual(4, s1.Count);
	}

	[TestMethod]
	public void Constructor_InitializesSet_ThroughAnotherSet()
	{
		Set s1 = [4, 5.5, -7, 1];
		Set s2 = new(s1);

		Assert.AreEqual(4, s2.Count);
	}
	#endregion

	#region Containment Tests
	[TestMethod]
	public void Contains_ReturnsTrue_ForElementInSet()
	{
		Set s1 = [4, 5.5, -7, 1];

		Assert.IsTrue(s1.Contains(4));
		Assert.IsTrue(s1.Contains(5.5));
		Assert.IsTrue(s1.Contains(-7));
		Assert.IsTrue(s1.Contains(1));
	}

	[TestMethod]
	public void Contains_ReturnsFalse_ForElementNotInSet()
	{
		Set s1 = [4, 5.5, -7, 1];

		Assert.IsFalse(s1.Contains(6));
		Assert.IsFalse(s1.Contains(-4));
		Assert.IsFalse(s1.Contains(5.49));
		Assert.IsFalse(s1.Contains(0));
	}

	[TestMethod]
	public void Contains_ReturnsFalse_ForEmptySet()
	{
		Set s1 = [];

		Assert.IsFalse(s1.Contains(-1));
		Assert.IsFalse(s1.Contains(0));
		Assert.IsFalse(s1.Contains(1));
	}
	#endregion

	#region Equality Tests
	[TestMethod]
	public void Equality_ReturnsTrue_ForSetsWithSameElementsInSameOrder()
	{
		Set s1 = [-3, 2, 8, 4.5, -5.2];
		Set s2 = [-3, 2, 8, 4.5, -5.2];

		Assert.AreEqual(s1, s2);
	}

	[TestMethod]
	public void Equality_ReturnsTrue_ForSetsWithSameElementsInDifferentOrder()
	{
		Set s1 = [-3, 2, 8, 4.5, -5.2];
		Set s2 = [2, 8, -3, -5.2, 4.5];

		Assert.AreEqual(s1, s2);
	}

	[TestMethod]
	public void Equality_ReturnsTrue_ForSetsWithDifferentLengths()
	{
		Set s1 = [-3, 2, 8, 4.5];
		Set s2 = [-3, 2, 8, 4.5, -5.2];

		Assert.AreNotEqual(s1, s2);
	}

	[TestMethod]
	public void Equality_ReturnsTrue_ForSetsWithDifferentElements()
	{
		Set s1 = [-3, 2, 8, 4.5, -5];
		Set s2 = [-3, 2, 8, 4.5, -5.2];

		Assert.AreNotEqual(s1, s2);
	}
	#endregion

	[TestMethod]
	public void Add_AddsNumberToSet()
	{
		Set s1 = [2, 3];
		s1.Add(8);

		Assert.IsTrue(s1.Contains(8));
		Assert.AreEqual(3, s1.Count);
	}

	[TestMethod]
	public void Add_DoesNotRemoveElements()
	{
		Set s1 = [2, 3];
		s1.Add(8);

		Assert.IsTrue(s1.Contains(2));
		Assert.IsTrue(s1.Contains(3));
	}

	[TestMethod]
	public void Add_LeavesSetUnchanged_WhenElementIsAlreadyPresent()
	{
		Set s1 = [2, 3, 8];
		Assert.IsTrue(s1.Contains(8));
		Assert.AreEqual(3, s1.Count);

		s1.Add(8);
		Assert.IsTrue(s1.Contains(8));
		Assert.AreEqual(3, s1.Count);
	}
	
	[TestMethod]
	public void Add_AddsMultipleNumbersToSet()
	{
		Set s1 = [2, 3];
		s1.Add(4, 5, 6);

		Assert.IsTrue(s1.Contains(4));
		Assert.IsTrue(s1.Contains(5));
		Assert.IsTrue(s1.Contains(6));
		Assert.AreEqual(5, s1.Count);
	}

	[TestMethod]
	public void Add_AddsMultipleNumbersToSet_WithMixedDuplicates()
	{
		Set s1 = [1, 2, 4, 6];
		s1.Add(3, 4, 5, 6, 5);

		Assert.IsTrue(s1.Contains(3));
		Assert.IsTrue(s1.Contains(5));
		Assert.AreEqual(6, s1.Count);
	}

	[TestMethod]
	public void Add_HandlesSetArgumentProperly()
	{
		Set s1 = [1, 2, 4, 6];
		Set s2 = [3, 4, 5, 6, 5];
		s1.Add(s2);

		Assert.IsTrue(s1.Contains(3));
		Assert.IsTrue(s1.Contains(5));
		Assert.AreEqual(6, s1.Count);
	}

	[TestMethod]
	public void Remove_RemovesNumberFromSet()
	{
		Set s1 = [2, 3, 8];
		s1.Remove(8);

		Assert.IsFalse(s1.Contains(8));
		Assert.AreEqual(2, s1.Count);
	}

	[TestMethod]
	public void Remove_LeavesSetUnchanged_WhenElementIsNotInSet()
	{
		Set s1 = [2, 3];
		Assert.IsFalse(s1.Contains(8));
		Assert.AreEqual(2, s1.Count);
		
		s1.Remove(8);
		Assert.IsFalse(s1.Contains(8));
		Assert.AreEqual(2, s1.Count);
	}

	[TestMethod]
	public void Remove_RemovesMultipleNumbersFromSet()
	{
		Set s1 = [2, 3, 4, 5, 6];
		s1.Remove(4, 5, 6);

		Assert.IsFalse(s1.Contains(4));
		Assert.IsFalse(s1.Contains(5));
		Assert.IsFalse(s1.Contains(6));
		Assert.AreEqual(2, s1.Count);
	}

	[TestMethod]
	public void Remove_RemovesMultipleNumbersFromSet_WithMixedDuplicates()
	{
		Set s1 = [1, 2, 4, 6];
		s1.Remove(3, 4, 5, 6, 4);

		Assert.IsFalse(s1.Contains(4));
		Assert.IsFalse(s1.Contains(6));
		Assert.AreEqual(2, s1.Count);
	}

	[TestMethod]
	public void Remove_HandlesSetArgumentProperly()
	{
		Set s1 = [1, 2, 4, 6];
		Set s2 = [3, 4, 5, 6, 4];
		s1.Remove(s2);

		Assert.IsFalse(s1.Contains(4));
		Assert.IsFalse(s1.Contains(6));
		Assert.AreEqual(2, s1.Count);
	}
}
