namespace ClassCluster;

/// <summary>
/// Represents a boundary for bounded containers.
/// </summary>
/// <typeparam name="T">The type of boundary.</typeparam>
public readonly struct Boundary<T> where T : notnull
{
	/// <summary>
	/// The shape and position of the boundary.
	/// </summary>
	public T Value { get; init; }
	/// <summary>
	/// If the values in this boundary are to be counted as belonging to a set.
	/// </summary>
	public bool Closed { get; init; }
	
	/// <summary>
	/// Creates a new boundary.
	/// </summary>
	/// <param name="value">The form of the boundary.</param>
	/// <param name="closed">If the boundary is closed.</param>
	/// <exception cref="ArgumentException">Thrown when the boundary is infinite and is closed.</exception>
	public Boundary(T value, bool closed = true)
	{
		if (closed && Utils.IsInfinity(value))
			throw new ArgumentException("Boundary cannot be closed while having an infinite value");
		Value = value;
		Closed = closed;
	}

	public override string? ToString() => Value.ToString();
	public override bool Equals(object? obj)
	{
		if (obj == null) return false;
		if (obj is T val) return Value.Equals(val);
		if (GetType() != obj.GetType()) return false;

		Boundary<T> other = (Boundary<T>)obj;
		return Closed == other.Closed && Value.Equals(other.Value);
	}
	public override int GetHashCode() => HashCode.Combine(Value, Closed);

	public static implicit operator Boundary<T>(T obj) => new(obj);

	public static bool operator ==(Boundary<T> left, Boundary<T> right) => left.Equals(right);
	public static bool operator ==(Boundary<T> left, T right) => left.Equals(right);
	public static bool operator ==(T left, Boundary<T> right) => left.Equals(right);
	public static bool operator !=(Boundary<T> left, Boundary<T> right) => !(left == right);
	public static bool operator !=(Boundary<T> left, T right) => !(left == right);
	public static bool operator !=(T left, Boundary<T> right) => !(left == right);
}