using System.Numerics;
using Vector = ClassCluster.Geometry.Vector;

namespace ClassCluster.Interfaces;

/// <summary>
/// Represents a 2D geometrical object.
/// </summary>
internal interface IObject2D<TSelf> : IAdditionOperators<TSelf, Vector, TSelf>, ISubtractionOperators<TSelf, Vector, TSelf> where TSelf : IObject2D<TSelf>
{
	
}