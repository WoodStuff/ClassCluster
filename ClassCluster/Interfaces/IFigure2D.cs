namespace ClassCluster.Interfaces;

/// <summary>
/// Represents a bounded 2D object that encloses a space and has area.
/// </summary>
internal interface IFigure2D : IObject2D
{
	double Area { get; }
	double Perimeter { get; }
	double Distance(Point p);
	Position Locate(Point p);
}