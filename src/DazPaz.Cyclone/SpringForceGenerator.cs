namespace DazPaz.Cyclone
{
	public class SpringForceGenerator : IParticleForceGenerator
	{
		#region Properties

		private IParticle OtherParticle { get; set; }
		private double SpringConstant { get; set; }
		private double RestLength { get; set; }
		private bool IsBungeeSpring { get; set; }

		#endregion

		#region Constructors

		public SpringForceGenerator(IParticle otherParticle, double springConstant, double restLength,
			bool isBungeeSpring = false)
		{
			OtherParticle = otherParticle;
			SpringConstant = springConstant;
			RestLength = restLength;
			IsBungeeSpring = isBungeeSpring;
		}

		#endregion

		#region IParticleForceGenerator Members

		public void UpdateForce(IParticle particle, double duration)
		{
			var springVector = particle.Position - OtherParticle.Position;
			var springExtension = springVector.Magnitude - RestLength;

			if (((springExtension < 0.0f) && IsBungeeSpring)) return;

			var springForce = (SpringConstant * springExtension) * springVector.Normal.Inverse;
			particle.AddForce(springForce);
		}

		#endregion
	}
}
