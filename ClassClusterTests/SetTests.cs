namespace ClassCluster.Tests;

[TestClass]
public class SetTests
{
	#region Constructor Tests
	[TestMethod]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0028:Simplify collection initialization")]
	public void Constructor_InitializesEmptySet_WithNoArguments()
	{
		Set<double> s1 = new();

		Assert.AreEqual(0, s1.Count);
	}

	[TestMethod]
	public void Constructor_InitializesSet()
	{
		Set<double> s1 = new(4, 5.5, -7, 1);

		Assert.AreEqual(4, s1.Count);
	}

	[TestMethod]
	public void Constructor_RemovesDuplicateElements()
	{
		Set<double> s1 = new(1, 5, 5, 8, 5, 2, 5, 2);

		Assert.AreEqual(4, s1.Count);
	}

	[TestMethod]
	public void Constructor_InitializesSet_ThroughArray()
	{
		Set<double> s1 = [4, 5.5, -7, 1];

		Assert.AreEqual(4, s1.Count);
	}

	[TestMethod]
	public void Constructor_InitializesSet_ThroughAnotherSet()
	{
		Set<double> s1 = [4, 5.5, -7, 1];
		Set<double> s2 = new(s1);

		Assert.AreEqual(4, s2.Count);
	}
	#endregion

	#region Property Tests
	[TestMethod]
	public void EmptySet_HasCorrectProperties()
	{
		Set<double> s1 = [];
		Assert.AreEqual(0, s1.Count);
		Assert.IsTrue(s1.IsEmpty);
	}

	[TestMethod]
	public void NonEmptySet_IsNotEmpty()
	{
		Set<double> s1 = [1, 2, 3, 4, 5];
		Assert.IsFalse(s1.IsEmpty);
	}

	[TestMethod]
	public void Min_ReturnsSmallestValue()
	{
		Set<double> s1 = [2, 4, 1.5, -4, 7, 0];

		double result = s1.Min();

		Assert.AreEqual(-4, result);
	}

	[TestMethod]
	public void Min_ThrowsError_ForEmptySet()
	{
		Assert.ThrowsException<InvalidOperationException>(() => Set<double>.Empty.Max());
	}

	[TestMethod]
	public void Max_ReturnsLargestValue()
	{
		Set<double> s1 = [2, 4, 1.5, -4, 7, 0];

		double result = s1.Max();

		Assert.AreEqual(7, result);
	}

	[TestMethod]
	public void Max_ThrowsError_ForEmptySet()
	{
		Assert.ThrowsException<InvalidOperationException>(() => Set<double>.Empty.Min());
	}

	[TestMethod]
	public void Range_ReturnsCorrectValue_ForTwoValues()
	{
		Set<double> s1 = [6, -7.5];

		double result = s1.Range();

		Assert.AreEqual(13.5, result);
	}

	[TestMethod]
	public void Range_ReturnsCorrectValue_ForMoreThanTwoValues()
	{
		Set<double> s1 = [2, 3, 5, 7, 11];

		double result = s1.Range();

		Assert.AreEqual(9, result);
	}

	[TestMethod]
	public void Range_ReturnsZero_ForSingleValue()
	{
		Set<double> s1 = [4.5];

		double result = s1.Range();

		Assert.AreEqual(0, result);
	}

	[TestMethod]
	public void Range_ThrowsError_ForEmptySet()
	{
		Assert.ThrowsException<InvalidOperationException>(() => Set<double>.Empty.Range());
	}

	[TestMethod]
	public void Sum_CalculatesCorrectSum()
	{
		Set<double> s1 = [1, 2, 3, 4, 5];

		double result = s1.Sum();

		Assert.AreEqual(15, result);
	}

	[TestMethod]
	public void Sum_ReturnsZero_ForEmptySet()
	{
		Set<double> s1 = [];

		double result = s1.Sum();

		Assert.AreEqual(0, result);
	}

	[TestMethod]
	public void Average_CalculatesCorrectAverage()
	{
		Set<double> s1 = [2, 3, 4, 5];

		double result = s1.Average();

		Assert.AreEqual(3.5, result);
	}

	[TestMethod]
	public void Average_ReturnsNaN_ForEmptySet()
	{
		Set<double> s1 = [];

		double result = s1.Average();

		Assert.IsTrue(double.IsNaN(result));
	}
	#endregion

	#region Containment Tests
	[TestMethod]
	public void Contains_ReturnsTrue_ForElementInSet()
	{
		Set<double> s1 = [4, 5.5, -7, 1];

		Assert.IsTrue(s1.Contains(4));
		Assert.IsTrue(s1.Contains(5.5));
		Assert.IsTrue(s1.Contains(-7));
		Assert.IsTrue(s1.Contains(1));
	}

	[TestMethod]
	public void Contains_ReturnsFalse_ForElementNotInSet()
	{
		Set<double> s1 = [4, 5.5, -7, 1];

		Assert.IsFalse(s1.Contains(6));
		Assert.IsFalse(s1.Contains(-4));
		Assert.IsFalse(s1.Contains(5.49));
		Assert.IsFalse(s1.Contains(0));
	}

	[TestMethod]
	public void Contains_ReturnsFalse_ForEmptySet()
	{
		Set<double> s1 = [];

		Assert.IsFalse(s1.Contains(-1));
		Assert.IsFalse(s1.Contains(0));
		Assert.IsFalse(s1.Contains(1));
	}

	[TestMethod]
	public void Subset_ReturnsTrue_WithSubsetOfOriginalSet()
	{
		Set<double> s1 = [1, 2, 3, 4, 5];
		Set<double> s2 = [2, 3, 4];

		Assert.IsTrue(s1.Subset(s2));
	}

	[TestMethod]
	public void Subset_ReturnsTrue_ForIdenticalSets()
	{
		Set<double> s1 = [1, 2, 3, 4, 5];
		Set<double> s2 = [1, 2, 3, 4, 5];

		Assert.IsTrue(s1.Subset(s2));
	}

	[TestMethod]
	public void Subset_ReturnsFalse_WithSetWithoutCommonElements()
	{
		Set<double> s1 = [1, 2, 3, 4, 5];
		Set<double> s2 = [6, 7, 8];

		Assert.IsFalse(s1.Subset(s2));
	}

	[TestMethod]
	public void Subset_ReturnsFalse_WithIntersectingSets()
	{
		Set<double> s1 = [1, 2, 3, 4, 5];
		Set<double> s2 = [3, 4, 5, 6, 7];

		Assert.IsFalse(s1.Subset(s2));
	}

	[TestMethod]
	public void Subset_ReturnsFalse_WithSupersetOfOriginalSet()
	{
		Set<double> s1 = [2, 3, 4];
		Set<double> s2 = [1, 2, 3, 4, 5];

		Assert.IsFalse(s1.Subset(s2));
	}

	[TestMethod]
	public void ProperSubset_ReturnsTrue_WithSubsetOfOriginalSet()
	{
		Set<double> s1 = [1, 2, 3, 4, 5];
		Set<double> s2 = [2, 3, 4];

		Assert.IsTrue(s1.ProperSubset(s2));
	}

	[TestMethod]
	public void ProperSubset_ReturnsFalse_ForIdenticalSets()
	{
		Set<double> s1 = [1, 2, 3, 4, 5];
		Set<double> s2 = [1, 2, 3, 4, 5];

		Assert.IsFalse(s1.ProperSubset(s2));
	}

	[TestMethod]
	public void ProperSubset_ReturnsFalse_WithIntersectingSets()
	{
		Set<double> s1 = [1, 2, 3, 4, 5];
		Set<double> s2 = [3, 4, 5, 6, 7];

		Assert.IsFalse(s1.ProperSubset(s2));
	}
	#endregion

	#region Equality Tests
	[TestMethod]
	public void Equality_ReturnsTrue_ForSetsWithSameElementsInSameOrder()
	{
		Set<double> s1 = [-3, 2, 8, 4.5, -5.2];
		Set<double> s2 = [-3, 2, 8, 4.5, -5.2];

		Assert.AreEqual(s1, s2);
	}

	[TestMethod]
	public void Equality_ReturnsTrue_ForSetsWithSameElementsInDifferentOrder()
	{
		Set<double> s1 = [-3, 2, 8, 4.5, -5.2];
		Set<double> s2 = [2, 8, -3, -5.2, 4.5];

		Assert.AreEqual(s1, s2);
	}

	[TestMethod]
	public void Equality_ReturnsFalse_ForSetsWithDifferentLengths()
	{
		Set<double> s1 = [-3, 2, 8, 4.5];
		Set<double> s2 = [-3, 2, 8, 4.5, -5.2];

		Assert.AreNotEqual(s1, s2);
	}

	[TestMethod]
	public void Equality_ReturnsFalse_ForSetsWithDifferentElements()
	{
		Set<double> s1 = [-3, 2, 8, 4.5, -5];
		Set<double> s2 = [-3, 2, 8, 4.5, -5.2];

		Assert.AreNotEqual(s1, s2);
	}
	#endregion

	#region Element Control Method Tests
	[TestMethod]
	public void Add_AddsNumberToSet()
	{
		Set<double> s1 = [2, 3];
		s1.Add(8);

		Assert.IsTrue(s1.Contains(8));
		Assert.AreEqual(3, s1.Count);
	}

	[TestMethod]
	public void Add_DoesNotRemoveElements()
	{
		Set<double> s1 = [2, 3];
		s1.Add(8);

		Assert.IsTrue(s1.Contains(2));
		Assert.IsTrue(s1.Contains(3));
	}

	[TestMethod]
	public void Add_LeavesSetUnchanged_WhenElementIsAlreadyPresent()
	{
		Set<double> s1 = [2, 3, 8];
		Assert.IsTrue(s1.Contains(8));
		Assert.AreEqual(3, s1.Count);

		s1.Add(8);
		Assert.IsTrue(s1.Contains(8));
		Assert.AreEqual(3, s1.Count);
	}

	[TestMethod]
	public void Add_AddsMultipleNumbersToSet()
	{
		Set<double> s1 = [2, 3];
		s1.Add(4, 5, 6);

		Assert.IsTrue(s1.Contains(4));
		Assert.IsTrue(s1.Contains(5));
		Assert.IsTrue(s1.Contains(6));
		Assert.AreEqual(5, s1.Count);
	}

	[TestMethod]
	public void Add_AddsMultipleNumbersToSet_WithMixedDuplicates()
	{
		Set<double> s1 = [1, 2, 4, 6];
		s1.Add(3, 4, 5, 6, 5);

		Assert.IsTrue(s1.Contains(3));
		Assert.IsTrue(s1.Contains(5));
		Assert.AreEqual(6, s1.Count);
	}

	[TestMethod]
	public void Add_HandlesSetArgumentProperly()
	{
		Set<double> s1 = [1, 2, 4, 6];
		Set<double> s2 = [3, 4, 5, 6];
		s1.Add(s2);

		Assert.IsTrue(s1.Contains(3));
		Assert.IsTrue(s1.Contains(5));
		Assert.AreEqual(6, s1.Count);
	}

	[TestMethod]
	public void Remove_RemovesNumberFromSet()
	{
		Set<double> s1 = [2, 3, 8];
		s1.Remove(8);

		Assert.IsFalse(s1.Contains(8));
		Assert.AreEqual(2, s1.Count);
	}

	[TestMethod]
	public void Remove_LeavesSetUnchanged_WhenElementIsNotInSet()
	{
		Set<double> s1 = [2, 3];
		Assert.IsFalse(s1.Contains(8));
		Assert.AreEqual(2, s1.Count);

		s1.Remove(8);
		Assert.IsFalse(s1.Contains(8));
		Assert.AreEqual(2, s1.Count);
	}

	[TestMethod]
	public void Remove_RemovesMultipleNumbersFromSet()
	{
		Set<double> s1 = [2, 3, 4, 5, 6];
		s1.Remove(4, 5, 6);

		Assert.IsFalse(s1.Contains(4));
		Assert.IsFalse(s1.Contains(5));
		Assert.IsFalse(s1.Contains(6));
		Assert.AreEqual(2, s1.Count);
	}

	[TestMethod]
	public void Remove_RemovesMultipleNumbersFromSet_WithMixedDuplicates()
	{
		Set<double> s1 = [1, 2, 4, 6];
		s1.Remove(3, 4, 5, 6, 4);

		Assert.IsFalse(s1.Contains(4));
		Assert.IsFalse(s1.Contains(6));
		Assert.AreEqual(2, s1.Count);
	}

	[TestMethod]
	public void Remove_HandlesSetArgumentProperly()
	{
		Set<double> s1 = [1, 2, 4, 6];
		Set<double> s2 = [3, 4, 5, 6];
		s1.Remove(s2);

		Assert.IsFalse(s1.Contains(4));
		Assert.IsFalse(s1.Contains(6));
		Assert.AreEqual(2, s1.Count);
	}

	[TestMethod]
	public void Keep_RemovesNumbersFromSet()
	{
		Set<double> s1 = [1, 2, 3, 4, 5];
		s1.Keep(2);

		Assert.AreEqual(s1, [2]);
	}

	[TestMethod]
	public void Keep_KeepsMultipleNumbers()
	{
		Set<double> s1 = [1, 2, 3, 4, 5];
		s1.Keep(2, 3, 5);

		Assert.AreEqual(s1, [2, 3, 5]);
	}

	[TestMethod]
	public void Keep_EmptiesSet_WhenKeepingNumbersNotInSet()
	{
		Set<double> s1 = [1, 2, 3, 4, 5];
		s1.Keep(6, 7, 8);

		Assert.AreEqual(s1, []);
	}

	[TestMethod]
	public void Keep_KeepsMultipleNumbers_WithMixedDuplicates()
	{
		Set<double> s1 = [1, 2, 3, 4, 5];
		s1.Keep(2, 3, 4, 6, 4);

		Assert.AreEqual(s1, [2, 3, 4]);
	}

	[TestMethod]
	public void Keep_HandlesSetArgumentProperly()
	{
		Set<double> s1 = [1, 2, 3, 4];
		Set<double> s2 = [3, 4, 5, 6];
		s1.Keep(s2);

		Assert.AreEqual(s1, [3, 4]);
	}
	#endregion

	#region Set Combining Method Tests
	[TestMethod]
	public void Union_ReturnsCombinedSets()
	{
		Set<double> s1 = [1, 2, 3, 4, 5];
		Set<double> s2 = [6, 7, 8];

		Set<double> result = s1.Union(s2);
		Set<double> expected = [1, 2, 3, 4, 5, 6, 7, 8];

		Assert.AreEqual(expected, result);
	}

	[TestMethod]
	public void Union_OmitsDuplicates()
	{
		Set<double> s1 = [1, 2, 3, 4, 5];
		Set<double> s2 = [3, 4, 5, 6, 7];

		Set<double> result = s1.Union(s2);
		Set<double> expected = [1, 2, 3, 4, 5, 6, 7];

		Assert.AreEqual(expected, result);
	}

	[TestMethod]
	public void Union_ReturnsOriginalSet_WithEmptySet()
	{
		Set<double> s1 = [1, 2, 3, 4, 5];
		Set<double> s2 = [];

		Set<double> result = s1.Union(s2);

		Assert.AreEqual(s1, result);
	}

	[TestMethod]
	public void Union_ReturnsOriginalSet_WithSubset()
	{
		Set<double> s1 = [1, 2, 3, 4, 5];
		Set<double> s2 = [2, 3, 5];

		Set<double> result = s1.Union(s2);

		Assert.AreEqual(s1, result);
	}

	[TestMethod]
	public void Union_ReturnsOriginalSet_WithSelf()
	{
		Set<double> s1 = [1, 2, 3, 4, 5];

		Set<double> result = s1.Union(s1);

		Assert.AreEqual(s1, result);
	}

	[TestMethod]
	public void Union_ReturnsEmptySet_WithTwoEmptySets()
	{
		Set<double> s1 = [];
		Set<double> s2 = [];

		Set<double> result = s1.Union(s2);

		Assert.AreEqual([], result);
	}

	[TestMethod]
	public void Difference_ReturnsDifferenceOfSets()
	{
		Set<double> s1 = [1, 2, 3, 4, 5];
		Set<double> s2 = [2, 3, 5];

		Set<double> result = s1.Difference(s2);
		Set<double> expected = [1, 4];

		Assert.AreEqual(expected, result);
	}

	[TestMethod]
	public void Difference_OmitsElementsNotInSet()
	{
		Set<double> s1 = [1, 2, 3, 4, 5];
		Set<double> s2 = [2, 3, 5, 7, 11];

		Set<double> result = s1.Difference(s2);
		Set<double> expected = [1, 4];

		Assert.AreEqual(expected, result);
	}

	[TestMethod]
	public void Difference_ReturnsOriginalSet_WithEmptySet()
	{
		Set<double> s1 = [1, 2, 3, 4, 5];
		Set<double> s2 = [];

		Set<double> result = s1.Difference(s2);

		Assert.AreEqual(s1, result);
	}

	[TestMethod]
	public void Difference_ReturnsOriginalSet_WithSetWithoutCommonElements()
	{
		Set<double> s1 = [1, 2, 3, 4, 5];
		Set<double> s2 = [6, 7, 8];

		Set<double> result = s1.Difference(s2);

		Assert.AreEqual(s1, result);
	}

	[TestMethod]
	public void Difference_ReturnsEmptySet_WithSelf()
	{
		Set<double> s1 = [1, 2, 3, 4, 5];

		Set<double> result = s1.Difference(s1);

		Assert.AreEqual([], result);

	}

	[TestMethod]
	public void Difference_ReturnsEmptySet_WithTwoEmptySets()
	{
		Set<double> s1 = [];
		Set<double> s2 = [];

		Set<double> result = s1.Difference(s2);

		Assert.AreEqual([], result);
	}

	[TestMethod]
	public void Intersection_ReturnsIntersectionOfSets()
	{
		Set<double> s1 = [1, 2, 3, 4, 5];
		Set<double> s2 = [3, 4, 5, 6, 7];

		Set<double> result = s1.Intersection(s2);
		Set<double> expected = [3, 4, 5];

		Assert.AreEqual(expected, result);
	}

	[TestMethod]
	public void Intersection_ReturnsOriginalSet_WithSuperset()
	{
		Set<double> s1 = [2, 3, 4];
		Set<double> s2 = [1, 2, 3, 4, 5];

		Set<double> result = s1.Intersection(s2);

		Assert.AreEqual(s1, result);
	}

	[TestMethod]
	public void Intersection_ReturnsOriginalSet_WithSelf()
	{
		Set<double> s1 = [1, 2, 3, 4, 5];

		Set<double> result = s1.Intersection(s1);

		Assert.AreEqual(s1, result);
	}

	[TestMethod]
	public void Intersection_ReturnsEmptySet_WithSetWithoutCommonElements()
	{
		Set<double> s1 = [1, 2, 3, 4, 5];
		Set<double> s2 = [];

		Set<double> result = s1.Intersection(s2);

		Assert.AreEqual([], result);
	}

	[TestMethod]
	public void Intersection_ReturnsEmptySet_WithEmptySet()
	{
		Set<double> s1 = [1, 2, 3, 4, 5];
		Set<double> s2 = [];

		Set<double> result = s1.Intersection(s2);

		Assert.AreEqual([], result);
	}

	[TestMethod]
	public void Intersection_ReturnsEmptySet_WithTwoEmptySets()
	{
		Set<double> s1 = [];
		Set<double> s2 = [];

		Set<double> result = s1.Intersection(s2);

		Assert.AreEqual([], result);
	}
	#endregion

	#region Set Generation Method Tests
	[TestMethod]
	public void FromRange_GeneratesCorrectSet_WithoutStep()
	{
		Set<double> s1 = Set<double>.FromRange(3, 7);
		Set<double> expected = [3, 4, 5, 6, 7];

		Assert.AreEqual(expected, s1);
	}

	[TestMethod]
	public void FromRange_GeneratesCorrectSet_WithStep()
	{
		Set<double> s1 = Set<double>.FromRange(3, 11, 2);
		Set<double> expected = [3, 5, 7, 9, 11];

		Assert.AreEqual(expected, s1);
	}

	[TestMethod]
	public void FromRange_GeneratesSingleton_WhenStartIsEqualToEnd()
	{
		Set<double> s1 = Set<double>.FromRange(7, 7);
		Set<double> expected = [7];

		Assert.AreEqual(1, s1.Count, "Generated Set<double> did not have only one element.");
		Assert.AreEqual(expected, s1);
	}

	[TestMethod]
	public void FromRange_ExcludesEnd_WhenStepSkipsOverEnd()
	{
		Set<double> s1 = Set<double>.FromRange(3, 10, 2);
		Set<double> expected = [3, 5, 7, 9];

		Assert.IsFalse(s1.Contains(10), "Set<double> contained end element when step did not align with it.");
		Assert.AreEqual(expected, s1);
	}

	[TestMethod]
	public void FromRange_ThrowsError_WhenStepIsNotPositive()
	{
		Assert.ThrowsException<ArgumentException>(() => Set<double>.FromRange(1, 5, 0), "Set<double> with step 0 was accepted.");
		Assert.ThrowsException<ArgumentException>(() => Set<double>.FromRange(1, 5, -2), "Set<double> with negative was accepted.");
	}

	[TestMethod]
	public void FromRange_ThrowsError_WhenStartIsGreaterThanEnd()
	{
		Assert.ThrowsException<ArgumentException>(() => Set<double>.FromRange(5, 1));
	}
	#endregion

	#region Operator Tests
	[TestMethod]
	public void AdditionOperator_WithDouble_AddsNumberToSet()
	{
		Set<double> s1 = [2, 3];
		Set<double> s2 = s1 + 8;

		Assert.AreEqual([2, 3, 8], s2);
	}

	[TestMethod]
	public void AdditionOperator_WithSet_CombinesSets()
	{
		Set<double> s1 = [1, 2, 3, 4, 5];
		Set<double> s2 = [3, 4, 5, 6, 7];

		Set<double> result = s1 + s2;
		Set<double> expected = [1, 2, 3, 4, 5, 6, 7];

		Assert.AreEqual(expected, result);
	}

	[TestMethod]
	public void SubtractionOperator_WithDouble_RemovesNumberFromSet()
	{
		Set<double> s1 = [2, 3, 8];
		Set<double> s2 = s1 - 8;

		Assert.AreEqual([2, 3], s2);
	}

	[TestMethod]
	public void SubtractionOperator_WithDouble_ReturnsDifferenceOfSets()
	{
		Set<double> s1 = [1, 2, 3, 4, 5];
		Set<double> s2 = [2, 3, 5, 7, 11];

		Set<double> result = s1 - s2;
		Set<double> expected = [1, 4];

		Assert.AreEqual(expected, result);
	}

	[TestMethod]
	public void MultiplicationOperator_ReturnsIntersectionOfSets()
	{
		Set<double> s1 = [1, 2, 3, 4, 5];
		Set<double> s2 = [3, 4, 5, 6, 7];

		Set<double> result = s1 * s2;
		Set<double> expected = [3, 4, 5];

		Assert.AreEqual(expected, result);
	}

	[TestMethod]
	public void CountOperator_ReturnsElementCount()
	{
		Set<double> s1 = [2, 3, 5, 7, 11];

		int count = ~s1;

		Assert.AreEqual(5, count);
	}

	[TestMethod]
	public void CountOperator_ReturnsZero_ForEmptySet()
	{
		Set<double> s1 = [];

		int count = ~s1;

		Assert.AreEqual(0, count);
	}

	[TestMethod]
	public void Boolean_ReturnsTrue_ForNonEmptySet()
	{
		Set<double> s1 = [1, 2, 3, 4, 5];

		if (s1) { /* passed */ }
		else { Assert.Fail("Non-empty Set<double> returned false."); }
	}

	[TestMethod]
	public void Boolean_ReturnsFalse_ForEmptySet()
	{
		Set<double> s1 = [];

		if (s1) { Assert.Fail("Empty Set<double> returned false."); }
	}
	#endregion

	#region Iteration Tests
	[TestMethod]
	public void Iteration_LoopsThroughEachElement()
	{
		Set<double> s1 = [2, 3, 5, 7, 11];

		int i = 0;
		foreach (double element in s1)
		{
			Assert.IsTrue(s1.Contains(element), "Foreach loop iterated through an element not in set.");
			i++;
		}

		Assert.AreEqual(s1.Count, i, "Foreach loop did not loop through each element in set.");
	}

	[TestMethod]
	public void Iteration_LoopsFromSmallestToLargest()
	{
		Set<double> s1 = [5, -2, 4.2, 0, -26.5];

		double previous = double.NegativeInfinity;
		foreach (double element in s1)
		{
			Assert.IsTrue(element > previous, "Foreach loop did not loop through elements in correct order.");
			previous = element;
		}
	}
	#endregion
}