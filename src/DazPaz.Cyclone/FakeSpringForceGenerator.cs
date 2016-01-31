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
			if (particle.IsInfiniteMass == true) return;

			// Calculate relative position of the particle from the anchor
			Vector3 positionFromAnchor = particle.Position - Anchor;

			// Calculate the value for Gamma
			var gamma = 0.5 * Math.Sqrt((4 * SpringConstant) - (Damping * Damping));
			
			if (gamma == 0.0) return;

			// Calculate the value for the C constant
			var c = (positionFromAnchor * (Damping / (2.0 * gamma))) + (particle.Velocity * (1.0 / gamma));

			// Calculate the target position (in two parts)
			var tagetPosition = (positionFromAnchor * Math.Cos(gamma * duration)) + (c * Math.Sin(gamma*duration));
			tagetPosition *= Math.Exp(0.5 * duration * Damping);

			// Calculate the acceleration needed (and hence the force)
			var acceleration = ((tagetPosition - positionFromAnchor) * (1.0 / (duration * duration))) -
				(particle.Velocity * duration);

			// Add the force to the particle
			particle.AddForce(acceleration * particle.Mass);
		}

		#endregion
	}
}
