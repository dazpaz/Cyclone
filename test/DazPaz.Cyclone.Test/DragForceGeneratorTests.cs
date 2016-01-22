using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DazPaz.Cyclone.Test
{
	[TestClass]
	public class DragForceGeneratorTests
	{
		private const double TestK1 = 0.25;
		private const double TestK2 = 0.1;
		private const double TestDuration = 1.0;

		[TestMethod]
		public void DragForce_DragForceGeneratorCanAddADragForceToParticle()
		{
			var velocity = new Vector3(0.0, 3.0, 4.0);
			var expectedDragForce = new Vector3(0.0, -2.25, -3.0);

			var mockParticle = new Mock<IParticle>(MockBehavior.Strict);
			mockParticle.Setup(p => p.Velocity).Returns(velocity);
			mockParticle.Setup(p => p.AddForce(expectedDragForce));

			var forceGenerator = new DragForceGenerator(TestK1, TestK2);

			forceGenerator.UpdateForce(mockParticle.Object, TestDuration);

			mockParticle.VerifyAll();
		}
	}
}
