namespace ClassCluster;

public class Circle
{
	public Point Center { get; set; }
	public double Radius { get; set; }

	public Circle(Point center, double radius)
	{
		if (radius <= 0) throw new ArgumentException("Radius must be positive");
		Center = center;
		Radius = radius;
	}
	public Circle(double radius)
	{
		if (radius <= 0) throw new ArgumentException("Radius must be positive");
		Center = Point.Origin;
		Radius = radius;
	}
}