namespace ClassCluster.Interfaces;

/// <summary>
/// Represents a bounded 2D object that encloses a space and has area.
/// </summary>
internal interface IFigure2D<TSelf> : IObject2D<TSelf> where TSelf : IFigure2D<TSelf>
{
	/// <summary>
	/// The area of the <see cref="IFigure2D"/>.
	/// </summary>
	double Area { get; }
	/// <summary>
	/// The perimeter of the <see cref="IFigure2D"/>.
	/// </summary>
	double Perimeter { get; }
	/// <summary>
	/// Calculates a point's signed distance from the <see cref="IFigure2D"/>.
	/// Outside values are positive and inside values are negative.
	/// </summary>
	/// <param name="p">The point to calculate distance from.</param>
	/// <returns>A double representing the signed distance.</returns>
	double Distance(Point p);
	/// <summary>
	/// Finds the relative position of a point to the <see cref="IFigure2D"/>.
	/// </summary>
	/// <param name="p">The point to locate relative to the <see cref="IFigure2D"/>.</param>
	/// <returns>If the point is outside, on the boundary, or inside the <see cref="IFigure2D"/>.</returns>
	Position Locate(Point p);
}