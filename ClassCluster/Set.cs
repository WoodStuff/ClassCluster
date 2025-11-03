using System.Linq;
using System.Numerics;

namespace ClassCluster;

/// <summary>
/// Represents a set of numbers, which is unordered and can only have one of the same value.
/// </summary>
public class Set<T> : ICollection<T> where T : notnull
{
	/// <summary>
	/// An empty set.
	/// </summary>n
	public static Set<T> Empty => [];
	private HashSet<T> num;

	public Set() => num = [];
	public Set(params T[] n) : this((IEnumerable<T>)n) { }
	public Set(IEnumerable<T> n) => num = [.. n];
	public Set(HashSet<T> s) => num = s;

	/// <summary>
	/// Creates a set containing all numbers in a range.
	/// </summary>
	/// <param name="start">The value to start at, inclusive.</param>
	/// <param name="end">The value to end at, inclusive depending on the step.</param>
	/// <param name="step">The difference between each value in the set. Must be positive.</param>
	/// <returns>A set containing numbers starting at <paramref name="start"/> and ending at <paramref name="end"/>, with the specified <paramref name="step"/> between each value.</returns>
	public static Set<double> FromRange(double start, double end, double step = 1)
	{
		if (start > end) throw new ArgumentException("Start cannot be bigger than end.");
		if (step <= 0) throw new ArgumentException("Step must be positive.");

		Set<double> set = [];
		for (double i = start; i <= end; i += step)
		{
			set.Add(i);
		}
		return set;
	}

	/// <summary>
	/// The number of elements in the set.
	/// </summary>
	public int Count => num.Count;
	/// <summary>
	/// Checks if the set is empty.
	/// </summary>
	public bool IsEmpty => Count == 0;

	/// <summary>
	/// Checks if the set contains a number.
	/// </summary>
	/// <param name="value">The value to check for.</param>
	/// <returns>If the set includes the number.</returns>
	public bool Contains(T value) => num.Contains(value);
	/// <summary>
	/// Checks if the provided set is a subset of this set.
	/// </summary>
	/// <param name="other">The set that may be a subset of the current one.</param>
	/// <returns>true if <paramref name="other"/> is a subset of this set, otherwise false.</returns>
	public bool Subset(Set<T> other)
	{
		foreach (T value in other)
		{
			if (!Contains(value)) return false;
		}
		return true;
	}
	/// <summary>
	/// Checks if the provided set is a proper subset of this set.
	/// </summary>
	/// <param name="other">The set that may be a proper subset of the current one.</param>
	/// <returns>true if <paramref name="other"/> is a proper subset of this set, otherwise false.</returns>
	public bool ProperSubset(Set<T> other)
	{
		if (Count == other.Count) return false;
		foreach (T value in other)
		{
			if (!Contains(value)) return false;
		}
		return true;
	}

	/// <summary>
	/// Creates an union of two sets.
	/// </summary>
	/// <param name="other">The set to unionize with.</param>
	public Set<T> Union(Set<T> other)
	{
		Set<T> final = Clone();
		final.Add(other);
		return final;
	}
	/// <summary>
	/// Creates an union of two sets.
	/// </summary>
	/// <param name="s1">The first set.</param>
	/// <param name="s2">The second set.</param>
	public static Set<T> Union(Set<T> s1, Set<T> s2)
	{
		Set<T> final = s1.Clone();
		final.Add(s2);
		return final;
	}
	/// <summary>
	/// Creates a difference of two sets.
	/// </summary>
	/// <param name="other">The set to remove.</param>
	public Set<T> Difference(Set<T> other)
	{
		Set<T> final = Clone();
		final.Remove(other);
		return final;
	}
	/// <summary>
	/// Creates a difference of two sets.
	/// </summary>
	/// <param name="s1">The first set.</param>
	/// <param name="s2">The second set.</param>
	public static Set<T> Difference(Set<T> s1, Set<T> s2)
	{
		Set<T> final = s1.Clone();
		final.Remove(s2);
		return final;
	}
	/// <summary>
	/// Creates an intersection of two sets.
	/// </summary>
	/// <param name="other">The set to intersect with.</param>
	public Set<T> Intersection(Set<T> other)
	{
		Set<T> final = Clone();
		final.Keep(other);
		return final;
	}
	/// <summary>
	/// Creates an intersection of two sets.
	/// </summary>
	/// <param name="s1">The first set.</param>
	/// <param name="s2">The second set.</param>
	public static Set<T> Intersection(Set<T> s1, Set<T> s2)
	{
		Set<T> final = s1.Clone();
		final.Keep(s2);
		return final;
	}

	/// <summary>
	/// Adds values to the set.
	/// </summary>
	/// <param name="values">The values to append.</param>
	public void Add(params IEnumerable<T> values)
	{
		foreach (T value in values)
		{
			if (!Contains(value)) num.Add(value);
		}
	}
	/// <summary>
	/// Removes the values from the set. The set doesn't get changed if it doesn't contain the value.
	/// </summary>
	/// <param name="value">The value to remove.</param>
	public void Remove(params IEnumerable<T> values)
	{
		foreach (T value in values)
		{
			if (Contains(value)) num.Remove(value);
		}
	}
	/// <summary>
	/// Removes values from the set that aren't in the other.
	/// </summary>
	/// <param name="values">The values to keep.</param>
	public void Keep(params IEnumerable<T> values)
	{
		Set<T> final = [];
		foreach (T value in this)
		{
			if (values.Contains(value)) final.Add(value);
		}
		num = final.num;
	}

	public void Filter(Predicate<T> predicate)
	{
		foreach (T value in num)
		{
			if (!predicate(value)) num.Remove(value);
		}
	}

	/// <summary>
	/// Removes all elements from the set.
	/// </summary>
	public void Clear() => num.Clear();

	// interface members
	void ICollection<T>.Add(T value) => num.Add(value);
	bool ICollection<T>.Remove(T value) => num.Remove(value);
	bool ICollection<T>.IsReadOnly => false;
	void ICollection<T>.CopyTo(T[] array, int arrayIndex) => SortedNum.ToHashSet().CopyTo(array, arrayIndex);

	/// <summary>
	/// Clones the set with the same numbers.
	/// </summary>
	/// <returns>A copy of the set.</returns>
	public Set<T> Clone()
	{
		Set<T> set = (Set<T>)MemberwiseClone();
		set.num = [.. num];
		return set;
	}

	public override string ToString()
	{
		if (IsEmpty) return "{ }";

		string s = string.Join(", ", SortedNum);

		return $"{{ {s} }}";
	}
	public override bool Equals(object? obj)
	{
		if (obj == null || GetType() != obj.GetType()) return false; // must be a set

		Set<T> otherSet = (Set<T>)obj;
		if (num.Count != otherSet.num.Count) return false; // must have same amount of elements

		foreach (T value in this)
		{
			if (!otherSet.Contains(value)) return false;
		}
		return true;
	}
	public override int GetHashCode()
	{
		int hash = 17;
		foreach (T value in this) hash = hash * 31 + value.GetHashCode();
		return hash;
	}

	public static bool operator true(Set<T> s) => s.Count > 0;
	public static bool operator false(Set<T> s) => s.Count == 0;
	public static bool operator ==(Set<T> left, Set<T> right)
	{
		if (left is null) return right is null;
		return left.Equals(right);
	}
	public static bool operator !=(Set<T> left, Set<T> right) => !(left == right);

	public static Set<T> operator +(Set<T> s, T value)
	{
		Set<T> addedSet = s.Clone();
		addedSet.Add(value);
		return addedSet;
	}
	public static Set<T> operator +(Set<T> s1, Set<T> s2) => Union(s1, s2);
	public static Set<T> operator -(Set<T> s, T value)
	{
		Set<T> subSet = s.Clone();
		subSet.Remove(value);
		return subSet;
	}
	public static Set<T> operator -(Set<T> s1, Set<T> s2) => Difference(s1, s2);
	public static Set<T> operator *(Set<T> s1, Set<T> s2) => Intersection(s1, s2);
	/// <summary>
	/// Returns the element count of the set.
	/// </summary>
	public static int operator ~(Set<T> s) => s.Count;

	public IEnumerator<T> GetEnumerator() => SortedNum.GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	private static bool IsComparable() => typeof(IComparable).IsAssignableFrom(typeof(T));
	private HashSet<T> SortedNum => IsComparable() ? [.. num.OrderBy(x => x)] : num;
}

/// <summary>
/// Set methods for specific types.
/// </summary>
public static class SetExtensions
{
	#region Numeric Methods
	/// <summary>
	/// Returns the smallest value in the set.
	/// </summary>
	public static T Min<T>(this Set<T> s) where T : INumber<T> => s.IsEmpty ? throw new InvalidOperationException("Set is empty") : s.First();
	/// <summary>
	/// Returns the largest value in the set.
	/// </summary>
	public static T Max<T>(this Set<T> s) where T : INumber<T> => s.IsEmpty ? throw new InvalidOperationException("Set is empty") : s.Last();
	/// <summary>
	/// Calculates the range of the set - the difference between the largest and smallest values.
	/// </summary>
	public static T Range<T>(this Set<T> s) where T : INumber<T> => s.Max() - s.Min();
	/// <summary>
	/// Adds up all the values in the set.
	/// </summary>
	public static T Sum<T>(this Set<T> s) where T : INumber<T>
	{
		T sum = T.Zero;
		foreach (T value in s)
		{
			sum += value;
		}
		return sum;
	}
	/// <summary>
	/// Calculates the average value in the set.
	/// </summary>
	public static double Average<T>(this Set<T> s) where T : INumber<T> => Convert.ToDouble(s.Sum()) / s.Count;
	#endregion

	#region Point Methods
	/// <summary>
	/// Adds up all the points in the set.
	/// </summary>
	public static Point Sum(this Set<Point> s)
	{
		Point sum = Point.Origin;
		foreach (var point in s)
		{
			sum += point;
		}
		return sum;
	}
	/// <summary>
	/// Calculates the average point in the set.
	/// </summary>
	public static Point Average(this Set<Point> s) => s.Sum() / s.Count;
	#endregion

	#region Vector Methods
	/// <summary>
	/// Adds up all the vectors in the set.
	/// </summary>
	public static Vector Sum(this Set<Vector> s)
	{
		Vector sum = Vector.Zero;
		foreach (var vector in s)
		{
			sum += vector;
		}
		return sum;
	}
	/// <summary>
	/// Calculates the average vector in the set.
	/// </summary>
	public static Vector Average(this Set<Vector> s) => s.Sum() / s.Count;
	#endregion

	#region Nested Set Methods
	/// <summary>
	/// Calculates the total amount of elements in a two-dimensional set.
	/// </summary>
	public static int TotalCount<T>(this Set<Set<T>> s) where T : notnull
	{
		int sum = 0;
		foreach (var set in s)
		{
			sum += set.Count;
		}
		return sum;
	}
	/// <summary>
	/// Flattens a two-dimensional set.
	/// </summary>
	/// <returns>A new set containing all of the values in the nested sets.</returns>
	public static Set<T> Flatten<T>(this Set<Set<T>> s) where T : notnull
	{
		Set<T> flattened = [];
		foreach (var nestedSet in s)
		{
			flattened.Add(nestedSet);
		}
		return flattened;
	}
	#endregion
}