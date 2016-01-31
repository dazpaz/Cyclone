using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DazPaz.Cyclone.Test
{
	[TestClass]
	public class FakeSpringForceGeneratorsTests
	{
		private const double SpringConstant = 2.0;
		private const double Damping = 0.9;
		private const double Duration = 0.1;
		private static readonly Vector3 Anchor = new Vector3(10.0, 10.0, 10.0);
		private static readonly Vector3 Position = new Vector3(3.0, 5.0, 7.0);
		private static readonly Vector3 Velocity = new Vector3(1.0, 2.0, 3.0);

		[TestMethod]
		public void UpdateForce_IfTheParticleHasInfiniteMass_ThenNoForceIsGenerated()
		{
			var mockParticle = new Mock<IParticle>(MockBehavior.Strict);
			mockParticle.Setup(p => p.IsInfiniteMass).Returns(true);
			var forceGenerator = new FakeSpringForceGenerator(Anchor, SpringConstant, Damping);

			forceGenerator.UpdateForce(mockParticle.Object, Duration);

			mockParticle.VerifyAll();
		}

		[TestMethod]
		public void UpdateForce_IfTheGammaValueIsZero_ThenNoForceIsGenerated()
		{
			var mockParticle = new Mock<IParticle>(MockBehavior.Strict);
			mockParticle.Setup(p => p.IsInfiniteMass).Returns(false);
			mockParticle.Setup(p => p.Position).Returns(Position);

			//The values for the spring constant and damping give a Gamma of zero
			var forceGenerator = new FakeSpringForceGenerator(Anchor, 0.25, 1.0);

			forceGenerator.UpdateForce(mockParticle.Object, Duration);

			mockParticle.VerifyAll();
		}

		[TestMethod]
		public void UpdateForce_FakedSpringCalculatesAForce()
		{
			var mockParticle = new Mock<IParticle>(MockBehavior.Strict);
			mockParticle.Setup(p => p.IsInfiniteMass).Returns(false);
			mockParticle.Setup(p => p.Position).Returns(Position);
			mockParticle.Setup(p => p.Velocity).Returns(Velocity);
			mockParticle.Setup(p => p.Mass).Returns(0.1);
			mockParticle.Setup(p => p.AddForce(It.IsAny<Vector3>()));

			var forceGenerator = new FakeSpringForceGenerator(Anchor, SpringConstant, Damping);

			forceGenerator.UpdateForce(mockParticle.Object, Duration);

			mockParticle.VerifyAll();
		}
	}
}
