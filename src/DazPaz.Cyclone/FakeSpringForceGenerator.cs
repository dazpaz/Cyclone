using System;

namespace DazPaz.Cyclone
{
	public class FakeSpringForceGenerator : IParticleForceGenerator
	{
		#region Properties

		private Vector3 Anchor { get; set; }
		private double SpringConstant { get; set; }
		private double Damping { get; set; }

		#endregion

		#region Constructors

		public FakeSpringForceGenerator(Vector3 anchor, double springConstant, double damping)
		{
			Anchor = anchor;
			SpringConstant = springConstant;
			Damping = damping;
		}

		#endregion

		#region IParticleForceGenerator Members

		public void UpdateForce(IParticle particle, double duration)
		{
			if (particle.IsInfiniteMass == true)
			{
				return;
			}

			// Calculate relative position of the particle from the anchor
			Vector3 positionFromAnchor = particle.Position - Anchor;

			// Calculate the value for Gamma
			float gamma = 0.5f * (float)Math.Sqrt((4 * SpringConstant) - (Damping * Damping));
			if (gamma == 0.0f)
			{
				return;
			}

			// Calculate the calue for the C constant
			Vector3 c = (positionFromAnchor * (Damping / (2.0f * gamma))) +
				(particle.Velocity * (1.0f / gamma));

			// Calculate the target position (in two parts)
			Vector3 tagetPosition = (positionFromAnchor * (float)Math.Cos(gamma * duration)) +
				(c * (float)Math.Sin(gamma * duration));
			tagetPosition *= (float)Math.Exp(0.5f * duration * Damping);

			// Calculate the acceleration needed (and hence the force)
			Vector3 acceleration = ((tagetPosition - positionFromAnchor) * (1.0f / (duration * duration))) -
				(particle.Velocity * duration);

			// Add the force to the particle
			particle.AddForce(acceleration * particle.Mass);
		}

		#endregion
	}
}
