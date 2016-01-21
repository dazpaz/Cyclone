using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DazPaz.Cyclone.Test
{
	[TestClass]
	public class ParticleForceRegistryTests
	{
		private const double Duration = 1.5;

		[TestMethod]
		public void UpdateForces_TheParticleForceRegistryUpdatesTheForcesOfAllItemsInItsRegistry()
		{
			var forceRegistry = new ParticleForceRegistry();
			var mockParticles = new List<Mock<IParticle>>();
			var mockGenerators = new List<Mock<IParticleForceGenerator>>();

			for ( var i = 0; i < 3; i++)
			{
				var mockParticle = new Mock<IParticle>(MockBehavior.Strict);
				var mockGenerator = new Mock<IParticleForceGenerator>(MockBehavior.Strict);
				mockGenerator.Setup(g => g.UpdateForce(mockParticle.Object, Duration));

				mockParticles.Add(mockParticle);
				mockGenerators.Add(mockGenerator);

				var registration = new ParticleForceRegistration
				{
					Generator = mockGenerator.Object,
					Particle = mockParticle.Object
				};

				forceRegistry.Add(registration);
			}

			forceRegistry.UpdateForces(1.5);

			Assert.AreEqual(3, forceRegistry.Count);
			foreach (var particle in mockParticles)
			{
				particle.VerifyAll();
			}

			foreach (var generator in mockGenerators)
			{
				generator.VerifyAll();
			}
		}
	}
}
