namespace DazPaz.Cyclone
{
	public class AnchoredSpringForceGenerator : IParticleForceGenerator
	{
		#region Properties

		private Vector3 Anchor { get; set; }
		private double SpringConstant { get; set; }
		private double RestLength { get; set; }
		private bool IsBungeeSpring { get; set; }

		#endregion

		#region Constructors

		public AnchoredSpringForceGenerator(Vector3 anchor, double springConstant, double restLength,
			bool isBungeeSpring = false)
		{
			Anchor = anchor;
			SpringConstant = springConstant;
			RestLength = restLength;
			IsBungeeSpring = isBungeeSpring;
		}

		#endregion

		#region IParticleForceGenerator Members

		public void UpdateForce(IParticle particle, double duration)
		{
			var springVector = particle.Position - Anchor;
			var springExtension = springVector.Magnitude - RestLength;

			if (((springExtension < 0.0f) && (IsBungeeSpring == true)) == false)
			{
				var springForce = (SpringConstant * springExtension) * springVector.Normal.Inverse;
				particle.AddForce(springForce);
			}
		}

		#endregion
	}
}
