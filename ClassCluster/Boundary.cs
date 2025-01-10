namespace ClassCluster;

/// <summary>
/// Represents a boundary for bounded containers.
/// </summary>
/// <typeparam name="T">The type of boundary.</typeparam>
public readonly struct Boundary<T>
{
	/// <summary>
	/// The shape and position of the boundary.
	/// </summary>
	public T Value { get; init;  }
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

	public static implicit operator Boundary<T>(T obj) => new(obj);
}