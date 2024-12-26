namespace ClassCluster
{
	/// <summary>
	/// Represents a 2D vector.
	/// </summary>
	public class Vector(double x, double y)
	{
		/// <summary>
		/// The zero vector [0, 0];
		/// </summary>
		public static Vector Zero => new(0, 0);
		/// <summary>
		/// The X position of the vector.
		/// </summary>
		public double X { get; set; } = x;
		/// <summary>
		/// The Y position of the vector.
		/// </summary>
		public double Y { get; set; } = y;

		/// <summary>
		/// Calculates the vector's magnitude, or length.
		/// </summary>
		/// <returns>A double representing the magnitude.</returns>
		public double Magnitude => Math.Sqrt(X * X + Y * Y);

		/// <summary>
		/// Normalizes the vector.
		/// Shortens the vector to have a length of 1 while maintaining the direction and returns it.
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
		/// </summary>
		public void Normalize()
		{
			double length = Magnitude;
			if (length != 0)
			{
				X /= length;
				Y /= length;
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
			X = rotated.X;
			Y = rotated.Y;
		}

		public override string ToString()
		{
			return $"[{X}, {Y}]";
		}
		public override bool Equals(object? obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}

			Vector other = (Vector)obj;
			return Math.Abs(X - other.X) < 0.000001 && Math.Abs(Y - other.Y) < 0.000001;
		}
		public override int GetHashCode()
		{
			return HashCode.Combine(X, Y);
		}

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
		public static double operator *(Vector v1, Vector v2) => Dot(v1, v2);
		public static Vector operator /(Vector v, double scalar)
		{
			if (scalar == 0) throw new DivideByZeroException();
			return new(v.X / scalar, v.Y / scalar);
		}
	}
}