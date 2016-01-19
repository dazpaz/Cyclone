using System;

namespace DazPaz.Cyclone
{
	public class Vector3
	{
		#region Properties

		public double X { get; set; }
		public double Y { get; set; }
		public double Z { get; set; }

		public static Vector3 ZeroVector
		{
			get { return new Vector3(); }
		}

		public double Magnitude
		{
			get { return Math.Sqrt(SquareMagnitude); }
		}

		public double SquareMagnitude
		{
			get { return (X * X) + (Y * Y) + (Z * Z); }
		}

		public Vector3 Normal
		{
			get { return GetNormal(); }
		}

		public Vector3 Inverse
		{
			get { return new Vector3(-X, -Y, -Z); }
		}

		#endregion

		#region Constructors

		public Vector3()
		{
		}

		public Vector3(double x, double y, double z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public Vector3(Vector3 vector)
		{
			X = vector.X;
			Y = vector.Y;
			Z = vector.Z;
		}

		#endregion

		#region Overriden from Object 
		public override bool Equals(object obj)
		{
			var other = obj as Vector3;

			return other != null
				&& other.X == X
				&& other.Y == Y
				&& other.Z == Z;
		}

		public override int GetHashCode()
		{
			var text = String.Format("({0},{1},{2})", X, Y, Z);
			return text.GetHashCode();
		}

		#endregion

		#region Addition and subtraction

		public static Vector3 operator +(Vector3 v1, Vector3 v2)
		{
			return new Vector3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
		}

		public void Add(Vector3 vector)
		{
			X += vector.X;
			Y += vector.Y;
			Z += vector.Z;
		}

		public void AddScaledVector(Vector3 vector, double scale)
		{
			X += vector.X * scale;
			Y += vector.Y * scale;
			Z += vector.Z * scale;
		}

		public static Vector3 operator -(Vector3 v1, Vector3 v2)
		{
			return new Vector3(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
		}

		public void Subtract(Vector3 vector)
		{
			X -= vector.X;
			Y -= vector.Y;
			Z -= vector.Z;
		}

		#endregion

		#region Scalar multiplication and division

		public static Vector3 operator *(Vector3 vector, double scale)
		{
			return new Vector3(vector.X * scale, vector.Y * scale, vector.Z * scale);
		}

		public static Vector3 operator *(double scale, Vector3 vector)
		{
			return new Vector3(vector.X * scale, vector.Y * scale, vector.Z * scale);
		}

		public void Multiply(double scalar)
		{
			X *= scalar;
			Y *= scalar;
			Z *= scalar;
		}

		public static Vector3 operator /(Vector3 vector, double scale)
		{
			return new Vector3(vector.X / scale, vector.Y / scale, vector.Z / scale);
		}

		#endregion

		#region Dot product

		public static double operator *(Vector3 v1, Vector3 v2)
		{
			return (v1.X * v2.X) + (v1.Y * v2.Y) + (v1.Z * v2.Z);
		}

		public double DotProduct(Vector3 vector)
		{
			return (X * vector.X) + (Y * vector.Y) + (Z * vector.Z);
		}

		#endregion

		#region Component product

		public Vector3 ComponentProduct(Vector3 vector)
		{
			return new Vector3(X * vector.X, Y * vector.Y, Z * vector.Z);
		}

		public void SetComponentProduct(Vector3 vector)
		{
			X *= vector.X;
			Y *= vector.Y;
			Z *= vector.Z;
		}

		#endregion

		#region Cross product

		public Vector3 CrossProduct(Vector3 vector)
		{
			return new Vector3(
				(Y * vector.Z) - (Z * vector.Y),
				(Z * vector.X) - (X * vector.Z),
				(X * vector.Y) - (Y * vector.X));
		}

		public void SetCrossProduct(Vector3 vector)
		{
			var crossProduct = CrossProduct(vector);
			X = crossProduct.X;
			Y = crossProduct.Y;
			Z = crossProduct.Z;
		}

		#endregion

		#region Invert and Set Zero

		public void Invert()
		{
			X = -X;
			Y = -Y;
			Z = -Z;
		}

		public void SetZero()
		{
			X = 0.0;
			Y = 0.0;
			Z = 0.0;
		}

		#endregion

		#region Normal

		public void Normalise()
		{
			var magnitude = Magnitude;
			if (magnitude == 0) throw new MagnitudeZeroException("Cannot normalise a zero length vector");

			X /= magnitude;
			Y /= magnitude;
			Z /= magnitude;
		}

		private Vector3 GetNormal()
		{
			var magnitude = Magnitude;
			if (magnitude == 0) throw new MagnitudeZeroException("Cannot get the normal of a zero length vector");

			return this / magnitude;
		}

		#endregion
	}
}
