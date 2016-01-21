using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DazPaz.Cyclone.Test
{
	[TestClass]
	public class GravityForceGeneratorTests
	{
		private const double TestMass = 1.5;
		private const double TestDuration = 1.0;
		private const double TestGravity = 4.0;
		private Vector3 GravityForce = new Vector3(0.0, -6.0, 0);

		[TestMethod]
		public void GravityForce_IfTheParticleHasInfiniteMass_TheForceGeneratorWillNotAddTheForceToTheParticle()
		{
			var mockParticle = GetMockParticle(true);
			var forceGenerator = new GravityForceGenerator(TestGravity);

			forceGenerator.UpdateForce(mockParticle.Object, TestDuration);

			mockParticle.VerifyAll();
		}

		[TestMethod]
		public void GravityForce_IfTheParticleHasFiniteMass_TheForceGeneratorWillAddTheGravityForceToTheParticle()
		{
			var mockParticle = GetMockParticle(false);
			mockParticle.Setup(p => p.Mass).Returns(TestMass);
			mockParticle.Setup(p => p.AddForce(GravityForce));

			var forceGenerator = new GravityForceGenerator(TestGravity);
			forceGenerator.UpdateForce(mockParticle.Object, 1.0);
			mockParticle.VerifyAll();
		}

		private Mock<IParticle> GetMockParticle(bool infiniteMass)
		{
			var mockParticle = new Mock<IParticle>(MockBehavior.Strict);
			mockParticle.Setup(p => p.IsInfiniteMass).Returns(infiniteMass);
			return mockParticle;
		}
	}
}
