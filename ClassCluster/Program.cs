global using System;
global using System.Collections.Generic;
global using System.Linq;

namespace ClassCluster;

class Program
{
	public static void Main()
	{
		Boundary<double> b1 = 5;

		Console.WriteLine(b1.Value);
		Console.WriteLine(b1.Closed);
	}
}