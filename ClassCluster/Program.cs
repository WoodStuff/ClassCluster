global using System;
global using System.Collections.Generic;
global using System.Linq;

namespace ClassCluster;

class Program
{
	public static void Main()
	{
		Vector v1 = new(0, 0);
		Vector v2 = new(0, 0);
		double angle = Vector.AngleBetween(v1, v2);
		Console.WriteLine(angle);
	}
}