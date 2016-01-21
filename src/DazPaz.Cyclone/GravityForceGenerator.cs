namespace DazPaz.Cyclone
{
	public class GravityForceGenerator : IParticleForceGenerator
	{
		#region Properties

		private Vector3 Gravity { get; set; }

		#endregion

		#region Constructors

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
