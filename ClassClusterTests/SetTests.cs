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

	#region Property Tests
	[TestMethod]
	public void EmptySet_HasCorrectProperties()
	{
		Set s1 = [];
		Assert.AreEqual(0, s1.Count);
		Assert.IsTrue(s1.IsEmpty);
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

	[TestMethod]
	public void Subset_ReturnsTrue_WithSubsetOfOriginalSet()
	{
		Set s1 = [1, 2, 3, 4, 5];
		Set s2 = [2, 3, 4];

		Assert.IsTrue(s1.Subset(s2));
	}

	[TestMethod]
	public void Subset_ReturnsTrue_ForIdenticalSets()
	{
		Set s1 = [1, 2, 3, 4, 5];
		Set s2 = [1, 2, 3, 4, 5];

		Assert.IsTrue(s1.Subset(s2));
	}

	[TestMethod]
	public void Subset_ReturnsFalse_WithSetWithoutCommonElements()
	{
		Set s1 = [1, 2, 3, 4, 5];
		Set s2 = [6, 7, 8];

		Assert.IsFalse(s1.Subset(s2));
	}

	[TestMethod]
	public void Subset_ReturnsFalse_WithIntersectingSets()
	{
		Set s1 = [1, 2, 3, 4, 5];
		Set s2 = [3, 4, 5, 6, 7];

		Assert.IsFalse(s1.Subset(s2));
	}

	[TestMethod]
	public void Subset_ReturnsFalse_WithSupersetOfOriginalSet()
	{
		Set s1 = [2, 3, 4];
		Set s2 = [1, 2, 3, 4, 5];

		Assert.IsFalse(s1.Subset(s2));
	}

	[TestMethod]
	public void ProperSubset_ReturnsTrue_WithSubsetOfOriginalSet()
	{
		Set s1 = [1, 2, 3, 4, 5];
		Set s2 = [2, 3, 4];

		Assert.IsTrue(s1.ProperSubset(s2));
	}

	[TestMethod]
	public void ProperSubset_ReturnsFalse_ForIdenticalSets()
	{
		Set s1 = [1, 2, 3, 4, 5];
		Set s2 = [1, 2, 3, 4, 5];

		Assert.IsFalse(s1.ProperSubset(s2));
	}

	[TestMethod]
	public void ProperSubset_ReturnsFalse_WithIntersectingSets()
	{
		Set s1 = [1, 2, 3, 4, 5];
		Set s2 = [3, 4, 5, 6, 7];

		Assert.IsFalse(s1.ProperSubset(s2));
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

	#region Element Control Method Tests
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
		Set s2 = [3, 4, 5, 6];
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
		Set s2 = [3, 4, 5, 6];
		s1.Remove(s2);

		Assert.IsFalse(s1.Contains(4));
		Assert.IsFalse(s1.Contains(6));
		Assert.AreEqual(2, s1.Count);
	}

	[TestMethod]
	public void Keep_RemovesNumbersFromSet()
	{
		Set s1 = [1, 2, 3, 4, 5];
		s1.Keep(2);

		Assert.AreEqual(s1, [2]);
	}

	[TestMethod]
	public void Keep_KeepsMultipleNumbers()
	{
		Set s1 = [1, 2, 3, 4, 5];
		s1.Keep(2, 3, 5);

		Assert.AreEqual(s1, [2, 3, 5]);
	}

	[TestMethod]
	public void Keep_EmptiesSet_WhenKeepingNumbersNotInSet()
	{
		Set s1 = [1, 2, 3, 4, 5];
		s1.Keep(6, 7, 8);

		Assert.AreEqual(s1, []);
	}

	[TestMethod]
	public void Keep_KeepsMultipleNumbers_WithMixedDuplicates()
	{
		Set s1 = [1, 2, 3, 4, 5];
		s1.Keep(2, 3, 4, 6, 4);

		Assert.AreEqual(s1, [2, 3, 4]);
	}

	[TestMethod]
	public void Keep_HandlesSetArgumentProperly()
	{
		Set s1 = [1, 2, 3, 4];
		Set s2 = [3, 4, 5, 6];
		s1.Keep(s2);

		Assert.AreEqual(s1, [3, 4]);
	}
	#endregion

	#region Set Combining Method Tests
	[TestMethod]
	public void Union_ReturnsCombinedSets()
	{
		Set s1 = [1, 2, 3, 4, 5];
		Set s2 = [6, 7, 8];

		Set result = s1.Union(s2);
		Set expected = [1, 2, 3, 4, 5, 6, 7, 8];

		Assert.AreEqual(expected, result);
	}

	[TestMethod]
	public void Union_OmitsDuplicates()
	{
		Set s1 = [1, 2, 3, 4, 5];
		Set s2 = [3, 4, 5, 6, 7];

		Set result = s1.Union(s2);
		Set expected = [1, 2, 3, 4, 5, 6, 7];

		Assert.AreEqual(expected, result);
	}

	[TestMethod]
	public void Union_ReturnsOriginalSet_WithEmptySet()
	{
		Set s1 = [1, 2, 3, 4, 5];
		Set s2 = [];

		Set result = s1.Union(s2);

		Assert.AreEqual(s1, result);
	}

	[TestMethod]
	public void Union_ReturnsOriginalSet_WithSubset()
	{
		Set s1 = [1, 2, 3, 4, 5];
		Set s2 = [2, 3, 5];

		Set result = s1.Union(s2);

		Assert.AreEqual(s1, result);
	}

	[TestMethod]
	public void Union_ReturnsOriginalSet_WithSelf()
	{
		Set s1 = [1, 2, 3, 4, 5];

		Set result = s1.Union(s1);

		Assert.AreEqual(s1, result);
	}

	[TestMethod]
	public void Union_ReturnsEmptySet_WithTwoEmptySets()
	{
		Set s1 = [];
		Set s2 = [];

		Set result = s1.Union(s2);

		Assert.AreEqual([], result);
	}

	[TestMethod]
	public void Difference_ReturnsDifferenceOfSets()
	{
		Set s1 = [1, 2, 3, 4, 5];
		Set s2 = [2, 3, 5];

		Set result = s1.Difference(s2);
		Set expected = [1, 4];

		Assert.AreEqual(expected, result);
	}

	[TestMethod]
	public void Difference_OmitsElementsNotInSet()
	{
		Set s1 = [1, 2, 3, 4, 5];
		Set s2 = [2, 3, 5, 7, 11];

		Set result = s1.Difference(s2);
		Set expected = [1, 4];

		Assert.AreEqual(expected, result);
	}

	[TestMethod]
	public void Difference_ReturnsOriginalSet_WithEmptySet()
	{
		Set s1 = [1, 2, 3, 4, 5];
		Set s2 = [];

		Set result = s1.Difference(s2);

		Assert.AreEqual(s1, result);
	}

	[TestMethod]
	public void Difference_ReturnsOriginalSet_WithSetWithoutCommonElements()
	{
		Set s1 = [1, 2, 3, 4, 5];
		Set s2 = [6, 7, 8];

		Set result = s1.Difference(s2);

		Assert.AreEqual(s1, result);
	}

	[TestMethod]
	public void Difference_ReturnsEmptySet_WithSelf()
	{
		Set s1 = [1, 2, 3, 4, 5];

		Set result = s1.Difference(s1);

		Assert.AreEqual([], result);

	}

	[TestMethod]
	public void Difference_ReturnsEmptySet_WithTwoEmptySets()
	{
		Set s1 = [];
		Set s2 = [];

		Set result = s1.Difference(s2);

		Assert.AreEqual([], result);
	}

	[TestMethod]
	public void Intersection_ReturnsIntersectionOfSets()
	{
		Set s1 = [1, 2, 3, 4, 5];
		Set s2 = [3, 4, 5, 6, 7];

		Set result = s1.Intersection(s2);
		Set expected = [3, 4, 5];

		Assert.AreEqual(expected, result);
	}

	[TestMethod]
	public void Intersection_ReturnsOriginalSet_WithSuperset()
	{
		Set s1 = [2, 3, 4];
		Set s2 = [1, 2, 3, 4, 5];
		
		Set result = s1.Intersection(s2);

		Assert.AreEqual(s1, result);
	}

	[TestMethod]
	public void Intersection_ReturnsOriginalSet_WithSelf()
	{
		Set s1 = [1, 2, 3, 4, 5];

		Set result = s1.Intersection(s1);
		
		Assert.AreEqual(s1, result);
	}

	[TestMethod]
	public void Intersection_ReturnsEmptySet_WithSetWithoutCommonElements()
	{
		Set s1 = [1, 2, 3, 4, 5];
		Set s2 = [];

		Set result = s1.Intersection(s2);

		Assert.AreEqual([], result);
	}

	[TestMethod]
	public void Intersection_ReturnsEmptySet_WithEmptySet()
	{
		Set s1 = [1, 2, 3, 4, 5];
		Set s2 = [];

		Set result = s1.Intersection(s2);

		Assert.AreEqual([], result);
	}

	[TestMethod]
	public void Intersection_ReturnsEmptySet_WithTwoEmptySets()
	{
		Set s1 = [];
		Set s2 = [];

		Set result = s1.Intersection(s2);

		Assert.AreEqual([], result);
	}
	#endregion

	#region Operator Tests
	[TestMethod]
	public void AdditionOperator_WithDouble_AddsNumberToSet()
	{
		Set s1 = [2, 3];
		Set s2 = s1 + 8;

		Assert.AreEqual([2, 3, 8], s2);
	}

	[TestMethod]
	public void AdditionOperator_WithSet_CombinesSets()
	{
		Set s1 = [1, 2, 3, 4, 5];
		Set s2 = [3, 4, 5, 6, 7];

		Set result = s1 + s2;
		Set expected = [1, 2, 3, 4, 5, 6, 7];

		Assert.AreEqual(expected, result);
	}

	[TestMethod]
	public void SubtractionOperator_WithDouble_RemovesNumberFromSet()
	{
		Set s1 = [2, 3, 8];
		Set s2 = s1 - 8;

		Assert.AreEqual([2, 3], s2);
	}

	[TestMethod]
	public void SubtractionOperator_WithDouble_ReturnsDifferenceOfSets()
	{
		Set s1 = [1, 2, 3, 4, 5];
		Set s2 = [2, 3, 5, 7, 11];

		Set result = s1 - s2;
		Set expected = [1, 4];

		Assert.AreEqual(expected, result);
	}

	[TestMethod]
	public void MultiplicationOperator_ReturnsIntersectionOfSets()
	{
		Set s1 = [1, 2, 3, 4, 5];
		Set s2 = [3, 4, 5, 6, 7];

		Set result = s1 * s2;
		Set expected = [3, 4, 5];

		Assert.AreEqual(expected, result);
	}

	[TestMethod]
	public void CountOperator_ReturnsElementCount()
	{
		Set s1 = [2, 3, 5, 7, 11];

		int count = ~s1;

		Assert.AreEqual(5, count);
	}

	[TestMethod]
	public void CountOperator_ReturnsZero_ForEmptySet()
	{
		Set s1 = [];

		int count = ~s1;

		Assert.AreEqual(0, count);
	}
	#endregion

	#region Element Finding Method Tests
	[TestMethod]
	public void Min_ReturnsSmallestValue()
	{
		Set s1 = [2, 4, 1.5, -4, 7, 0];

		double result = s1.Min();

		Assert.AreEqual(-4, result);
	}

	[TestMethod]
	public void Min_ReturnsNaN_ForEmptySet()
	{
		Set s1 = [];

		double result = s1.Min();

		Assert.IsTrue(double.IsNaN(result));
	}

	[TestMethod]
	public void Max_ReturnsLargestValue()
	{
		Set s1 = [2, 4, 1.5, -4, 7, 0];

		double result = s1.Max();

		Assert.AreEqual(7, result);
	}

	[TestMethod]
	public void Max_ReturnsNaN_ForEmptySet()
	{
		Set s1 = [];

		double result = s1.Max();

		Assert.IsTrue(double.IsNaN(result));
	}
	#endregion

	#region Iteration Tests
	[TestMethod]
	public void Iteration_LoopsThroughEachElement()
	{
		Set s1 = [2, 3, 5, 7, 11];

		int i = 0;
		foreach (double element in s1)
		{
			Assert.IsTrue(s1.Contains(element), "Foreach loop iterated through an element not in set.");
			i++;
		}

		Assert.AreEqual(s1.Count, i, "Foreach loop did not loop through each element in set.");
	}
	#endregion
}