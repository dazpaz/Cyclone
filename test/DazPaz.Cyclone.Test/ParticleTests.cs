using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DazPaz.Cyclone.Test
{
	[TestClass]
	public class ParticleTests
	{
		private static readonly Vector3 TestPosition = new Vector3(6.5, 7.6, 8.5);
		private static readonly Vector3 TestVelocity = new Vector3(2.7, 3.7, 0.7);

		[TestMethod]
		public void Construction_CanCreateAParticleWithNoParameters_AndAParticleWithDefaultValuesIsCreated()
		{
			var particle = new Particle();

			Assert.IsTrue(particle.Velocity.Equals(Vector3.ZeroVector));
			Assert.IsTrue(particle.Position.Equals(Vector3.ZeroVector));
			Assert.AreEqual(10.0, particle.Gravity);
			Assert.AreEqual(1.0, particle.Damping);
			Assert.AreEqual(0.0, particle.InverseMass);
			Assert.IsTrue(particle.IsInfiniteMass);
		}

		[TestMethod]
		public void Construction_CanCreateAParticleWithAnInitialPosition_AndItsPositionIsInitialised()
		{
			var particle = new Particle(TestPosition);

			Assert.IsTrue(particle.Position.Equals(TestPosition));
			Assert.IsTrue(particle.Velocity.Equals(Vector3.ZeroVector));
		}

		[TestMethod]
		public void Construction_CanCreateAParticleWithAnInitialPositionAndVelocity_AndItsPositionAndVelocityAreInitialised()
		{
			var particle = new Particle(TestPosition, TestVelocity);

			Assert.IsTrue(particle.Position.Equals(TestPosition));
			Assert.IsTrue(particle.Velocity.Equals(TestVelocity));
		}

		[TestMethod]
		public void Construction_CanCreateAParticleWithPositionVelocityInverseMassAndDamping_AndItCreated()
		{
			var particle = new Particle(TestPosition, TestVelocity, 5.0, 0.995);

			Assert.IsTrue(particle.Position.Equals(TestPosition));
			Assert.IsTrue(particle.Velocity.Equals(TestVelocity));
			Assert.AreEqual(5.0, particle.InverseMass);
			Assert.IsFalse(particle.IsInfiniteMass);
			Assert.AreEqual(0.995, particle.Damping);
		}

		[TestMethod]
		public void Construction_CanSetDefaultGravity_AndThisGravityValueIsUsedWhenCreatingParticles()
		{
			try
			{
				Particle.DefaultGravity = 15.5;
				var particle = new Particle();

				Assert.AreEqual(15.5, particle.Gravity);
			}
			finally
			{
				Particle.DefaultGravity = Particle.EarthsGravity;
			}
		}

		[TestMethod]
		public void ParticlesMass_HavingSetTheInverseMassOfAParticle_TheParticlesMassCanBeObtained()
		{
			var particle = new Particle(TestPosition, TestVelocity, 5.0);
			Assert.AreEqual(0.2, particle.Mass);
		}

		[TestMethod]
		public void ParticlesMass_CanUpdateTheMassOfAParticle_AndItsMassIsUpdated()
		{
			var particle = new Particle(TestPosition, TestVelocity, 5.0);

			particle.Mass = 0.65;

			Assert.AreEqual(0.65, particle.Mass);
		}

		[TestMethod]
		public void ParticlesMass_IfAParticleHasInfiniteMass_GettingItsMassThrowsAnException()
		{
			try
			{
				var particle = new Particle();
				var mass = particle.Mass;
				Assert.Fail("Exception was not thrown");
			}
			catch (InvalidMassException ex)
			{
				Assert.AreEqual("Mass is infinite", ex.Message);
			}
		}

		[TestMethod]
		public void ParticlesMass_TryingToSetAParticlesMassToZeroCausesAnExceptionToBeThrown()
		{
			try
			{
				var particle = new Particle();
				particle.Mass = 0.0;
				Assert.Fail("Exception was not thrown");
			}
			catch (InvalidMassException ex)
			{
				Assert.AreEqual("Cannot set mass of a particle to zero", ex.Message);
			}
		}

		[TestMethod]
		public void Integrate_WithJustGravityAndNoOtherForces_IntegratingToUpdateTheParticle_UpdatesTheParticlesPosition()
		{
			var particle = new Particle(TestPosition, TestVelocity);

			particle.Integrate(0.5);

			Assert.IsTrue(particle.Position.Equals(new Vector3(7.85, 8.2, 8.85)));
		}

		[TestMethod]
		public void Integrate_WithJustGravityAndNoOtherForces_IntegratingToUpdateTheParticle_UpdatesTheParticlesVelocity()
		{
			var particle = new Particle(TestPosition);

			particle.Integrate(0.5);

			Assert.IsTrue(particle.Velocity.Equals(new Vector3(0.0, -5.0, 0.0)));
		}

		[TestMethod]
		public void Integrate_IfTheMassOfTheParticleIsInfinite_ForcesOtherThanGravityHaveNoEffect()
		{
			var particleNoForces = new Particle();
			var particleForces = new Particle();

			particleForces.AddForce(new Vector3(3.0, 4.0, 5.0));

			particleNoForces.Integrate(1.0);
			particleForces.Integrate(1.0);

			Assert.IsTrue(particleForces.Position.Equals(particleNoForces.Position));
		}

		[TestMethod]
		public void Integrate_CanAddForcesTogether_AndWithCombinedForcesAddedToGravity_WhenIntegrating_ParticlesPositionIsUpdated()
		{
			var particleNoForces = new Particle(Vector3.ZeroVector, Vector3.ZeroVector, 0.5);
			var particleForces = new Particle(Vector3.ZeroVector, Vector3.ZeroVector, 0.5);

			for (var i = 0; i < 5; i++)
			{
				particleForces.AddForce(new Vector3(0.5, 1.0, 1.5));
			}

			particleNoForces.Integrate(1.0);
			particleForces.Integrate(1.0);

			Assert.IsTrue(particleNoForces.Position.Equals(new Vector3(0.0, -5.0, 0.0)));
			Assert.IsTrue(particleForces.Position.Equals(new Vector3(0.625, -3.75, 1.875)));
		}

		[TestMethod]
		public void Integrate_CanAddForcesTogether_AndWithCombinedForcesAddedToGravity_WhenIntegrating_ParticlesVelocityIsUpdated()
		{
			var particleNoForces = new Particle(Vector3.ZeroVector, Vector3.ZeroVector, 0.5);
			var particleWithForces = new Particle(Vector3.ZeroVector, Vector3.ZeroVector, 0.5);

			for (var i = 0; i < 5; i++)
			{
				particleWithForces.AddForce(new Vector3(0.5, 1.0, 1.5));
			}

			particleNoForces.Integrate(1.0);
			particleWithForces.Integrate(1.0);

			Assert.IsTrue(particleNoForces.Velocity.Equals(new Vector3(0.0, -10.0, 0.0)));
			Assert.IsTrue(particleWithForces.Velocity.Equals(new Vector3(1.25, -7.5, 3.75)));
		}

		[TestMethod]
		public void Integrate_AddingDampingToAParticleCausesItsVelocityToBeReducedComparedWithNoDamping()
		{
			var particleNoDamping = new Particle(Vector3.ZeroVector, Vector3.ZeroVector);
			var particleWithDamping = new Particle(Vector3.ZeroVector, Vector3.ZeroVector, 0.0, 0.9);

			particleNoDamping.Integrate(1.0);
			particleWithDamping.Integrate(1.0);

			Assert.AreEqual(-10, particleNoDamping.Velocity.Y);
			Assert.AreEqual(-9, particleWithDamping.Velocity.Y);
		}

		[TestMethod]
		public void Integrate_TryingToIntegrateWithDurationOfZeroCausesAnExceptionToBeThrown()
		{
			try
			{
				var particle = new Particle();
				particle.Integrate(0.0);
				Assert.Fail("Exception was not thrown");
			}
			catch (InvalidDurationException ex)
			{
				Assert.AreEqual("Duration can not be zero", ex.Message);
			}
		}
	}
}
