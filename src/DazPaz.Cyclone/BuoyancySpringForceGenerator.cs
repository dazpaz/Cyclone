namespace DazPaz.Cyclone
{
	public class BuoyancySpringForceGenerator : IParticleForceGenerator
	{
		#region Properties

		private const double DefaultLiquidDensity = 1000.0f;

		private double MaximumDepth { get; set; }
		private double Volume { get; set; }
		private double LiquidHeight { get; set; }
		private double LiquidDensity { get; set; }

		#endregion

		#region Constructors

		public BuoyancySpringForceGenerator(double maximumDepth, double volume, double liquidHeight,
			double liquidDensity = DefaultLiquidDensity)
		{
			MaximumDepth = maximumDepth;
			Volume = volume;
			LiquidHeight = liquidHeight;
			LiquidDensity = liquidDensity;
		}

		#endregion

		#region IParticleForceGenerator Members

		public void UpdateForce(IParticle particle, double duration)
		{
			double particleHeight = particle.Position.Y;

			// Check if we are out of the water - if so, return as there is no buoyancy force to add
			if (particleHeight >= LiquidHeight + MaximumDepth)
			{
				return;
			}

			Vector3 force = new Vector3();

			// Fully submerged buoyancy force
			force.Y = LiquidDensity * Volume;

			// If partially submerged, adjust the buoyancy force based on how deep the object is
			if (particleHeight > LiquidHeight - MaximumDepth)
			{
				var amountSubmerged = (LiquidHeight + MaximumDepth - particleHeight ) / (2 * MaximumDepth);
				force.Y *= amountSubmerged;
			}
		
			particle.AddForce(force);
		}

		#endregion
	}
}
