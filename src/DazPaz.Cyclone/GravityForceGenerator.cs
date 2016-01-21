namespace DazPaz.Cyclone
{
	public class GravityForceGenerator : IParticleForceGenerator
	{
		#region properties

		private Vector3 Gravity { get; set; }

		#endregion

		#region constructors

		public GravityForceGenerator(double gravity)
		{
			Gravity = new Vector3(0.0, -gravity, 0.0);
		}

		#endregion

		#region IParticleForceGenerator Members

		public void UpdateForce(IParticle particle, double duration)
		{
			if (particle.IsInfiniteMass == false)
			{
				particle.AddForce(Gravity * particle.Mass);
			}
		}

		#endregion
	}
}
