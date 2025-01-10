using ClassCluster.Interfaces;
using System.Collections;

namespace ClassCluster;

/// <summary>
/// Represents a set of numbers, which is unordered and can only have one of the same value.
/// </summary>
public class Set : IEnumerable<double>
{
	/// <summary>
	/// An empty set.
	/// </summary>n
	public static Set Empty => [];
	private HashSet<double> num;

	public Set()
	{
		num = [];
	}
	public Set(params double[] n) : this((IEnumerable<double>)n) { }
	public Set(IEnumerable<double> n)
	{
		num = new(n);
	}
	public Set(HashSet<double> s)
	{
		num = s;
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
	public bool Contains(double value)
	{
		return num.Contains(value);
	}
	/// <summary>
	/// Checks if the provided set is a subset of this set.
	/// </summary>
	/// <param name="other">The set that may be a subset of the current one.</param>
	/// <returns>true if <paramref name="other"/> is a subset of this set, otherwise false.</returns>
	public bool Subset(Set other)
	{
		foreach (double n in other)
		{
			if (!Contains(n)) return false;
		}
		return true;
	}
	/// <summary>
	/// Checks if the provided set is a proper subset of this set.
	/// </summary>
	/// <param name="other">The set that may be a proper subset of the current one.</param>
	/// <returns>true if <paramref name="other"/> is a proper subset of this set, otherwise false.</returns>
	public bool ProperSubset(Set other)
	{
		if (Count == other.Count) return false;
		foreach (double n in other)
		{
			if (!Contains(n)) return false;
		}
		return true;
	}

	/// <summary>
	/// Finds the smallest value in the set.
	/// </summary>
	/// <returns>The lowest value.</returns>
	public double Min()
	{
		if (Count == 0) return double.NaN;
		double lowest = double.PositiveInfinity;
		foreach (double n in this)
		{
			if (n < lowest) lowest = n;
		}
		return lowest;
	}
	/// <summary>
	/// Finds the largest value in the set.
	/// </summary>
	/// <returns>The highest value.</returns>
	public double Max()
	{
		if (Count == 0) return double.NaN;
		double lowest = double.NegativeInfinity;
		foreach (double n in this)
		{
			if (n > lowest) lowest = n;
		}
		return lowest;
	}
	/// <summary>
	/// Creates an union of two sets.
	/// </summary>
	/// <param name="other">The set to unionize with.</param>
	public Set Union(Set other)
	{
		Set final = Clone();
		final.Add(other);
		return final;
	}
	/// <summary>
	/// Creates an union of two sets.
	/// </summary>
	/// <param name="s1">The first set.</param>
	/// <param name="s2">The second set.</param>
	public static Set Union(Set s1, Set s2)
	{
		Set final = s1.Clone();
		final.Add(s2);
		return final;
	}
	/// <summary>
	/// Creates a difference of two sets.
	/// </summary>
	/// <param name="other">The set to remove.</param>
	public Set Difference(Set other)
	{
		Set final = Clone();
		final.Remove(other);
		return final;
	}
	/// <summary>
	/// Creates a difference of two sets.
	/// </summary>
	/// <param name="s1">The first set.</param>
	/// <param name="s2">The second set.</param>
	public static Set Difference(Set s1, Set s2)
	{
		Set final = s1.Clone();
		final.Remove(s2);
		return final;
	}
	/// <summary>
	/// Creates an intersection of two sets.
	/// </summary>
	/// <param name="other">The set to intersect with.</param>
	public Set Intersection(Set other)
	{
		Set final = Clone();
		final.Keep(other);
		return final;
	}
	/// <summary>
	/// Creates an intersection of two sets.
	/// </summary>
	/// <param name="s1">The first set.</param>
	/// <param name="s2">The second set.</param>
	public static Set Intersection(Set s1, Set s2)
	{
		Set final = s1.Clone();
		final.Keep(s2);
		return final;
	}

	/// <summary>
	/// Adds values to the set.
	/// </summary>
	/// <param name="values">The values to append.</param>
	public void Add(params IEnumerable<double> values)
	{
		foreach (double n in values)
		{
			if (!Contains(n)) num.Add(n);
		}
	}
	/// <summary>
	/// Removes the values from the set. The set doesn't get changed if it doesn't contain the value.
	/// </summary>
	/// <param name="value">The value to remove.</param>
	public void Remove(params IEnumerable<double> values)
	{
		foreach (double n in values)
		{
			if (Contains(n)) num.Remove(n);
		}
	}
	/// <summary>
	/// Removes values from the set that aren't in the other.
	/// </summary>
	/// <param name="values">The values to keep.</param>
	public void Keep(params IEnumerable<double> values)
	{
		Set final = [];
		foreach (double n in this)
		{
			if (values.Contains(n)) final.Add(n);
		}
		num = final.num;
	}

	/// <summary>
	/// Clones the set with the same numbers.
	/// </summary>
	/// <returns>A copy of the set.</returns>
	public Set Clone()
	{
		Set set = (Set)MemberwiseClone();
		set.num = new HashSet<double>(num);
		return set;
	}

	public override string ToString()
	{
		if (num.Count == 0) return "{}";

		List<double> sortedList = new(num);
		sortedList.Sort();
		string s = string.Join(", ", sortedList);

		return $"{{{s}}}";
	}
	public override bool Equals(object? obj)
	{
		if (obj == null || GetType() != obj.GetType()) return false; // must be a set

		Set otherSet = (Set)obj;
		if (num.Count != otherSet.num.Count) return false; // must have same amount of elements

		foreach (double n in this)
		{
			if (!otherSet.Contains(n)) return false;
		}
		return true;
	}
	public override int GetHashCode()
	{
		int hash = 17;
		foreach (double number in num) hash = hash * 31 + number.GetHashCode();
		return hash;
	}

	public static bool operator true(Set s) => s.Count > 0;
	public static bool operator false(Set s) => s.Count == 0;
	public static bool operator ==(Set left, Set right)
	{
		if (left is null) return right is null;
		return left.Equals(right);
	}
	public static bool operator !=(Set left, Set right)
	{
		return !(left == right);
	}

	public static Set operator +(Set s, double value)
	{
		Set addedSet = s.Clone();
		addedSet.Add(value);
		return addedSet;
	}
	public static Set operator +(Set s1, Set s2) => Union(s1, s2);
	public static Set operator -(Set s, double value)
	{
		Set subSet = s.Clone();
		subSet.Remove(value);
		return subSet;
	}
	public static Set operator -(Set s1, Set s2) => Difference(s1, s2);
	public static Set operator *(Set s1, Set s2) => Intersection(s1, s2);
	public static int operator ~(Set s) => s.Count;

	public IEnumerator<double> GetEnumerator()
	{
		return num.GetEnumerator();
	}
	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}