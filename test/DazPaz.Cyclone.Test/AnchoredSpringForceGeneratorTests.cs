using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DazPaz.Cyclone.Test
{
	[TestClass]
	public class AnchoredSpringForceGeneratorTests
	{
		private Mock<IParticle> MockParticle { get; set; }
		private Mock<IParticle> MockAnchor { get; set; }

		[TestInitialize]
		public void TestInitialise()
		{
			MockParticle = new Mock<IParticle>(MockBehavior.Strict);
			MockParticle.Setup(p => p.Position).Returns(new Vector3(7.0, 6.0, 5.0));

			MockAnchor = new Mock<IParticle>(MockBehavior.Strict);
			MockAnchor.Setup(p => p.Position).Returns(new Vector3(1.0, 3.0, 11.0));
		}

		[TestCleanup]
		public void TestCleanup()
		{
			MockParticle.VerifyAll();
			MockAnchor.VerifyAll();
		}

		[TestMethod]
		public void UpdateForce_WhenAnAnchoredSpringIsExpanded_TheForceGeneratorAppliesAForceToTheEndOfTheSpring()
		{
			MockParticle.Setup(p => p.AddForce(new Vector3(-4.5, -2.25, 4.5)));
			var forceGenerator = new SpringForceGenerator(MockAnchor.Object, 1.5, 4.5);

			forceGenerator.UpdateForce(MockParticle.Object, 1.0);

		}

		[TestMethod]
		public void UpdateForce_WhenAnAnchoredSpringIsCompressed_TheForceGeneratorAppliesAForceToTheEndOfTheSpring()
		{
			MockParticle.Setup(p => p.AddForce(new Vector3(4.5, 2.25, -4.5)));
			var forceGenerator = new SpringForceGenerator(MockAnchor.Object, 1.5, 13.5);

			forceGenerator.UpdateForce(MockParticle.Object, 1.0);
		}

		[TestMethod]
		public void UpdateForce_WhenAnAnchoredBungeeIsExpanded_TheForceGeneratorAppliesAForceToTheEndOfTheBungee()
		{
			MockParticle.Setup(p => p.AddForce(new Vector3(-4.5, -2.25, 4.5)));
			var forceGenerator = new SpringForceGenerator(MockAnchor.Object, 1.5, 4.5, true);

			forceGenerator.UpdateForce(MockParticle.Object, 1.0);
		}

		[TestMethod]
		public void UpdateForce_WhenAnAnchoredBungeeIsCompressed_NoForceIsAppliedToTheEndOfTheBungee()
		{
			var forceGenerator = new SpringForceGenerator(MockAnchor.Object, 1.5, 13.5, true);

			forceGenerator.UpdateForce(MockParticle.Object, 1.0);
		}
	}
}
