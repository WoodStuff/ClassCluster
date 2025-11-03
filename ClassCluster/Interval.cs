namespace ClassCluster;

/// <summary>
/// Represents a simple numeric interval, which is a continuous set of numbers.
/// </summary>
public class Interval
{
	/// <summary>
	/// The start of the interval.
	/// </summary>
	public Boundary<double> Start { get; set; }
	/// <summary>
	/// The end of the interval.
	/// </summary>
	public Boundary<double> End { get; set; }

	public Interval(Boundary<double> start, Boundary<double> end)
	{
		Start = start;
		End = end;
	}
	public Interval(double start, double end) : this(new Boundary<double>(start), new Boundary<double>(end)) { }
	public Interval(double start, double end, bool startClosed, bool endClosed) : this(new Boundary<double>(start, startClosed), new Boundary<double>(end, endClosed)) { }

	/// <summary>
	/// Checks if a number is contained in this interval.
	/// </summary>
	/// <param name="number">The number to check.</param>
	/// <returns>If the number is between <see cref="Start"/> and <see cref="End"/>.</returns>
	public bool Contains(double number)
	{
		bool startCondition = Start.Closed ? number >= Start.Value : number > Start.Value;
		bool endCondition = End.Closed ? number <= End.Value : number < End.Value;
		return startCondition && endCondition;
	}

	public override string ToString() => $"{(Start.Closed ? '[' : '(')}{Start}, {End}{(End.Closed ? ']' : ')')}";
    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

		Interval other = (Interval)obj;
		return Start == other.Start && End == other.End;
    }
	public override int GetHashCode() => HashCode.Combine(Start, End);
}