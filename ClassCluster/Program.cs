global using System;
global using System.Collections.Generic;
global using System.Linq;

namespace ClassCluster;

class Program
{
	public static void Main()
	{
		Line l1 = new((1, 1), (2, 3));
		l1.P2 = (1, 1);
		Console.WriteLine(l1.Slope);
	}
}