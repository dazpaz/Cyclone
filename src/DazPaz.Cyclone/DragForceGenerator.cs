using System;

namespace DazPaz.Cyclone
{
	public class DragForceGenerator : IParticleForceGenerator
	{
		#region properties

		private double K1 { get; set; }
		private double K2 { get; set; }

		#endregion

		#region constructors

		public DragForceGenerator(double k1, double k2)
		{
			K1 = k1;
			K2 = k2;
		}

		#endregion

		#region IParticleForceGenerator Members
		
		public void UpdateForce(IParticle particle, double duration)
		{
			var speedSquared = particle.Velocity.SquareMagnitude;
			var speed = Math.Sqrt(speedSquared);

			var dragMagnitude = (K1 * speed) + (K2 * speedSquared);

			var force = new Vector3(particle.Velocity.Normal.Inverse * dragMagnitude);

			particle.AddForce(force);
		}

		#endregion
	}
}
