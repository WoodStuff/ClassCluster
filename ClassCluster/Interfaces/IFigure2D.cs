﻿namespace ClassCluster.Interfaces;

/// <summary>
/// Represents a bounded 2D object that encloses a space and has area.
/// </summary>
internal interface IFigure2D : IObject2D
{
	double Area { get; }
}