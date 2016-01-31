using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DazPaz.Cyclone.Test
{
	[TestClass]
	public class BuoyancySpringForceGeneratorTests
	{
		private const double LiquidHeight = 10.0;
		private const double Volume = 0.3;
		private const double MaxDepth = 4.0;
		private const double Duration = 0.1;
		private static readonly Vector3 FullBuoyancy = new Vector3(0.0, 300.0, 0.0);

		[TestMethod]
		public void UpdateForce_IfTheObjectIsOutOfTheLiquid_NoBuoyancyForceIsGenerated()
		{
			var mockParticle = new Mock<IParticle>(MockBehavior.Strict);
			mockParticle.Setup(p => p.Position).Returns(new Vector3(0.0, 14.0, 0.0));
			var forceGenerator = new BuoyancySpringForceGenerator(MaxDepth, Volume, LiquidHeight);

			forceGenerator.UpdateForce(mockParticle.Object, Duration);

			mockParticle.VerifyAll();
		}

		[TestMethod]
		public void UpdateForce_IfTheObjectIsFullySubmerged_FullBuoyancyForceIsCaclculated()
		{
			var mockParticle = new Mock<IParticle>(MockBehavior.Strict);
			mockParticle.Setup(p => p.Position).Returns(new Vector3(0.0, 6.0, 0.0));
			mockParticle.Setup(p => p.AddForce(FullBuoyancy));
			var forceGenerator = new BuoyancySpringForceGenerator(MaxDepth, Volume, LiquidHeight);

			forceGenerator.UpdateForce(mockParticle.Object, Duration);

			mockParticle.VerifyAll();
		}

		[TestMethod]
		public void UpdateForce_TheBuoyancyForceGenerated_DependsOnTheDensityOfTheLiquid()
		{
			var mockParticle = new Mock<IParticle>(MockBehavior.Strict);
			mockParticle.Setup(p => p.Position).Returns(new Vector3(0.0, 6.0, 0.0));
			mockParticle.Setup(p => p.AddForce(FullBuoyancy / 10.0));
			var forceGenerator = new BuoyancySpringForceGenerator(MaxDepth, Volume, LiquidHeight, 100.0);

			forceGenerator.UpdateForce(mockParticle.Object, Duration);

			mockParticle.VerifyAll();
		}

		[TestMethod]
		public void UpdateForce_IfTheObjectIsHalfSubmerged_TheBuoyancyForceIsHalfOfWhenFullySubmerged()
		{
			var mockParticle = new Mock<IParticle>(MockBehavior.Strict);
			mockParticle.Setup(p => p.Position).Returns(new Vector3(0.0, 8.0, 0.0));
			mockParticle.Setup(p => p.AddForce(FullBuoyancy /2.0));
			var forceGenerator = new BuoyancySpringForceGenerator(MaxDepth, Volume, LiquidHeight);

			forceGenerator.UpdateForce(mockParticle.Object, Duration);

			mockParticle.VerifyAll();
		}
	}
}
