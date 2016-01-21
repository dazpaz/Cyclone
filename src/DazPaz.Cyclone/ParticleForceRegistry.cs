using System.Collections.Generic;

namespace DazPaz.Cyclone
{
	public struct ParticleForceRegistration
	{
		public IParticle Particle;
		public IParticleForceGenerator Generator;
	}

	interface IParticleForceRegistry
	{
		void UpdateForces(double duration);
	}

	public class ParticleForceRegistry : List<ParticleForceRegistration>, IParticleForceRegistry
	{
		public void UpdateForces(double duration)
		{
			foreach (ParticleForceRegistration registration in this)
			{
				registration.Generator.UpdateForce(registration.Particle, duration);
			}
		}
	}
}
