using System;

namespace DazPaz.Cyclone
{
	public class Particle : IParticle
	{
		#region constants

		private const float EarthsGravity = 10.0f;

		#endregion

		#region properties

		public static double DefaultGravity { get; set; }

		private Vector3 AccumulatedForce { get; set; }
		private Vector3 GravityAcceleration { get; set; }

		public Vector3 Position { get; set; }
		public Vector3 Velocity { get; set; }
		public double Gravity { get; set; }
		public double Damping { get; set; }
		public double InverseMass { get; set; }

		public double Mass
		{
			set { SetInverseMass(value); }
			get { return GetMass(); }
		}

		public bool IsInfiniteMass
		{
			get { return InverseMass == 0.0; }
		}

		#endregion

		#region constructors

		static Particle()
		{
			DefaultGravity = EarthsGravity;
		}

		public Particle() : this(Vector3.ZeroVector, Vector3.ZeroVector)
		{
		}

		public Particle(Vector3 position) : this(position, Vector3.ZeroVector)
		{
		}

		public Particle(Vector3 position, Vector3 velocity, double inverseMass = 0.0, double damping = 1.0)
		{
			Position = new Vector3(position);
			Velocity = new Vector3(velocity);

			Damping = damping;
			InverseMass = inverseMass;

			Gravity = DefaultGravity;
			GravityAcceleration = new Vector3(0, -Gravity, 0);

			AccumulatedForce = Vector3.ZeroVector;
		}

		#endregion

		#region public methods

		public void AddForce(Vector3 force)
		{
			AccumulatedForce += force;
		}

		public void Integrate(float duration)
		{
			if (duration == 0.0)  throw new InvalidDurationException("Duration can not be zero");

			// Start by using gravity - this is already an acceleration so don't convert from a force
			Vector3 resultingAcceleration = new Vector3(GravityAcceleration);

			// Work out the acceleration from the force
			resultingAcceleration.AddScaledVector(AccumulatedForce, InverseMass);

			// Update position - for the moment we are using full equation - make accn bit configurable?
			Position.AddScaledVector(Velocity, duration);
			Position.AddScaledVector(resultingAcceleration, (duration * duration) / 2.0f);

			// Update the velocity
			Velocity.AddScaledVector(resultingAcceleration, duration);

			// Impose drag if there is damping - we are using the full equation for now - make configurable?
			if (Damping != 1.0f)
			{
				Velocity *= (float)Math.Pow(Damping, duration);
			}

			// Clear the forces
			ClearAccumulatedForce();
		}
		#endregion

		#region private methods

		private void SetInverseMass(double mass)
		{
			if (mass == 0.0) throw new InvalidMassException("Can not set mass of a particle to zero");
			InverseMass = 1 / mass;
		}

		private double GetMass()
		{
			if (InverseMass == 0.0) throw new InvalidMassException("Mass is infinite");
			return 1 / InverseMass;
		}

		private void SetGravity(double gravity)
		{
			Gravity = gravity;
			GravityAcceleration.Y = -gravity;
		}

		private void ClearAccumulatedForce()
		{
			AccumulatedForce.SetZero();
		}

		#endregion
	}
}
