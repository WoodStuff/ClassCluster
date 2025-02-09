﻿namespace ClassCluster;

/// <summary>
/// Represents a 2D vector.
/// </summary>
public class Vector
{
	private const double Tolerance = 1e-6;

	private double _x;
	private double _y;

	/// <summary>
	/// The zero vector [0, 0];
	/// </summary>
	public static Vector Zero => new(0, 0);
	/// <summary>
	/// The unit vector [1, 0];
	/// </summary>
	public static Vector UnitX => new(1, 0);
	/// <summary>
	/// The unit vector [0, 1];
	/// </summary>
	public static Vector UnitY => new(0, 1);

	/// <summary>
	/// The X position of the vector.
	/// </summary>
	public double X => _x;
	/// <summary>
	/// The Y position of the vector.
	/// </summary>
	public double Y => _y;

	public Vector(double x, double y)
	{
		_x = x;
		_y = y;
	}
	/// <summary>
	/// Calculates the vector's magnitude, or length.
	/// </summary>
	/// <returns>A double representing the magnitude.</returns>
	public double Magnitude => Math.Sqrt(X * X + Y * Y);
	/// <summary>
	/// Calculates the vector's angle, from 0 to <see cref="Math.Tau"/>.
	/// </summary>
	public double Theta
	{
		get
		{
			double angle = Math.Atan2(Y, X);
			if (angle < 0) angle += Math.Tau;
			return angle;
		}
	}

	/// <summary>
	/// Normalizes the vector.
	/// Shortens the vector to have a length of 1 while maintaining the direction and returns it.
	/// Returns the zero vector if the given vector is the zero vector.
	/// </summary>
	/// <returns>The normalized vector.</returns>
	public Vector ToNormalized()
	{
		double length = Magnitude;
		if (length != 0)
		{
			return new(X / length, Y / length);
		}
		return this;
	}
	/// <summary>
	/// Calculates the dot product between this vector and the input vector.
	/// </summary>
	/// <param name="other">The point to calculate the dot product with.</param>
	/// <returns>The dot product.</returns>
	public double Dot(Vector other)
	{
		return X * other.X + Y * other.Y;
	}
	/// <summary>
	/// Calculates the dot product between two vectors.
	/// </summary>
	/// <param name="v1">The first vector.</param>
	/// <param name="v2">The second vector.</param>
	/// <returns>The dot product.</returns>
	public static double Dot(Vector v1, Vector v2)
	{
		return v1.Dot(v2);
	}
	/// <summary>
	/// Calculates the angle between this and the input vector in the desired angle unit.
	/// </summary>
	/// <param name="other">The other vector.</param>
	/// <param name="type">The angle unit. Defaults to radians.</param>
	/// <returns>The angle between the two vectors.</returns>
	public double AngleBetween(Vector other, Angles type = Angles.Radians)
	{
		if (Magnitude == 0 || other.Magnitude == 0) throw new InvalidOperationException("Cannot calculate angle with a zero vector.");
		double dotProduct = this * other;
		double magnitudesProduct = Magnitude * other.Magnitude;
		double angle = Math.Acos(dotProduct / magnitudesProduct);
		if (type != Angles.Radians) angle = Utils.ConvertAngle(Angles.Radians, angle, type);
		return angle;
	}
	/// <summary>
	/// Calculates the angle between two vectors in the desired angle unit.
	/// </summary>
	/// <param name="v1">The first vector.</param>
	/// <param name="v2">The second vector.</param>
	/// <param name="type">The angle unit. Defaults to radians.</param>
	/// <returns>The angle between the two vectors.</returns>
	public static double AngleBetween(Vector v1, Vector v2, Angles type = Angles.Radians)
	{
		return v1.AngleBetween(v2, type);
	}
	/// <summary>
	/// Rotates the vector counterclockwise and returns it.
	/// </summary>
	/// <param name="angle">The angle to rotate by.</param>
	/// <param name="type">The angle unit. Defaults to radians.</param>
	/// <returns>The rotated vector.</returns>
	public Vector RotatedBy(double angle, Angles type = Angles.Radians)
	{
		if (type != Angles.Radians) angle = Utils.ConvertAngle(type, angle, Angles.Radians);
		double x = Math.Round(X * Math.Cos(angle) - Y * Math.Sin(angle), 6);
		double y = Math.Round(X * Math.Sin(angle) + Y * Math.Cos(angle), 6);
		return new(x, y);
	}

	/// <summary>
	/// Normalizes the vector.
	/// Shortens the vector to have a length of 1 while maintaining the direction.
	/// Returns the zero vector if the given zero vector is the origin.
	/// </summary>
	public void Normalize()
	{
		double length = Magnitude;
		if (length != 0)
		{
			_x /= length;
			_y /= length;
		}
	}
	/// <summary>
	/// Rotates the vector counterclockwise.
	/// </summary>
	/// <param name="angle">The angle to rotate by.</param>
	/// <param name="type">The angle unit. Defaults to radians.</param>
	public void Rotate(double angle, Angles type = Angles.Radians)
	{
		Vector rotated = RotatedBy(angle, type);
		_x = rotated.X;
		_y = rotated.Y;
	}

	public override string ToString() => $"[{X}, {Y}]";
	public override bool Equals(object? obj)
	{
		if (obj == null || GetType() != obj.GetType())
		{
			return false;
		}

		Vector other = (Vector)obj;
		return Math.Abs(X - other.X) < Tolerance && Math.Abs(Y - other.Y) < Tolerance;
	}
	public override int GetHashCode() => HashCode.Combine(X, Y);

	public static explicit operator Vector(Point p) => new(p.X, p.Y);

	public static bool operator ==(Vector left, Vector right)
	{
		if (left is null) return right is null;
		return left.Equals(right);
	}
	public static bool operator !=(Vector left, Vector right)
	{
		return !(left == right);
	}

	public static Vector operator +(Vector left, Vector right) => new(left.X + right.X, left.Y + right.Y);
	public static Vector operator -(Vector left, Vector right) => new(left.X - right.X, left.Y - right.Y);
	public static Vector operator -(Vector v) => new(-v.X, -v.Y);

	public static Vector operator *(Vector v, double scalar) => new(v.X * scalar, v.Y * scalar);
	public static Vector operator *(double scalar, Vector v) => v * scalar;
	public static double operator *(Vector v1, Vector v2) => Dot(v1, v2);
	public static Vector operator /(Vector v, double scalar)
	{
		if (scalar == 0) throw new DivideByZeroException("Attempted to divide a vector by zero.");
		return new(v.X / scalar, v.Y / scalar);
	}
}