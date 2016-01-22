using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DazPaz.Cyclone.Test
{
	[TestClass]
	public class Vector3Tests
	{
		private const double Zero = 0.0;
		private const double X = 3.0;
		private const double Y = 4.0;
		private const double Z = 5.0;
		private const double RoundingTolerance = 1E-14;
		private const double MagnitudeOfNormalVector = 1.0;
		private const double ExpectedSquareMagnitude = 50.0;
		private static readonly double ExpectedMagnitude = Math.Sqrt(ExpectedSquareMagnitude);

		[TestMethod]
		public void ZeroVector_CanRequestAZeroVector_AndANewZeroVectorIsReturned()
		{
			var zeroVector = Vector3.ZeroVector;
			VerifyVector(zeroVector, Zero, Zero, Zero);
		}

		[TestMethod]
		public void VectorProperties_CanGetTheMagnitudeForAVector_AndItIsReturned()
		{
			var vector = CreateTestVector();
			Assert.AreEqual(ExpectedMagnitude, vector.Magnitude);
		}

		[TestMethod]
		public void VectorProperties_CanGetTheSquareMagnitudeForAVector_AndItIsReturned()
		{
			var vector = CreateTestVector();
			Assert.AreEqual(ExpectedSquareMagnitude, vector.SquareMagnitude);
		}

		[TestMethod]
		public void VectorProperties_CanGetTheNormalFromAVector_AndANewVectorIsReturnedThatIsTheNormalOfTheVector()
		{
			var vector = CreateTestVector();
			var normal = vector.Normal;

			VerifyVector(normal, X / ExpectedMagnitude, Y / ExpectedMagnitude, Z / ExpectedMagnitude);
			VerifyNormalHasMagnitudeOfOne(normal.Magnitude);
		}

		[TestMethod]
		public void VectorProperties_CanGetTheInverseFromAVector_AndANewVectorIsReturnedThatIsTheInverse()
		{
			var vector = CreateTestVector();
			var inverse = vector.Inverse;

			VerifyVector(inverse, -X, -Y, -Z);
		}

		[TestMethod]
		public void Constructor_CanCreateAVectorFromAnotherVector_AndTheNewVectorIsConstructed()
		{
			var vector = CreateTestVector();
			var newVector = new Vector3(vector);
			VerifyVector(newVector, X, Y, Z);
		}

		[TestMethod]
		public void Constructor_CanCreateAVectorWithSpecificSetOfCoordinates_AndTheNewVectorIsConstructed()
		{
			var vector = new Vector3(X, Y, Z);
			VerifyVector(vector, X, Y, Z);
		}

		[TestMethod]
		public void Constructor_CanCreateAVectorWithNoParameters_AndTheNewZeroVectorIsConstructed()
		{
			var vector = new Vector3();
			VerifyVector(vector, Zero, Zero, Zero);
		}

		[TestMethod]
		public void VectorAddition_CanAddTwoVectorsUsingThePlusOperator_AndTheVectorsAreAddedToProduceANewVector()
		{
			var vector1 = CreateTestVector();
			var vector2 = new Vector3(1.5, 2.5, 3.5);

			var vector = vector1 + vector2;

			VerifyVector(vector, 4.5, 6.5, 8.5);
		}

		[TestMethod]
		public void VectorAddition_CanAddAVectorToAnExistingVector_AndTheVectorIsAddedToTheExistingVector()
		{
			var vector = CreateTestVector();
			var vector2 = new Vector3(1.5, 2.5, 3.5);

			vector.Add(vector2);

			VerifyVector(vector, 4.5, 6.5, 8.5);
		}

		[TestMethod]
		public void VectorAddition_CanAddAScaledVectorToAnExistingVector_AndTheScaledVectorIsAddedToExistingVector()
		{
			var vector = CreateTestVector();
			var vector2 = new Vector3(1.5, 2.5, 3.5);

			vector.AddScaledVector(vector2, 4.0);

			VerifyVector(vector, 9.0, 14.0, 19.0);
		}

		[TestMethod]
		public void VectorSubtraction_CanSubtractOneVectorFromAnotherUsingTheSubtractOperator_AndSubtractionProducesNewVector()
		{
			var vector1 = CreateTestVector();
			var vector2 = new Vector3(1.5, 2.0, 2.5);

			var vector = vector1 - vector2;

			VerifyVector(vector, 1.5, 2.0, 2.5);
		}

		[TestMethod]
		public void VectorSubtraction_CanSubtractAVectorFromAnExistingVector_AndTheVectorIsSubtractedFromTheExistingVector()
		{
			var vector = CreateTestVector();
			var vector2 = new Vector3(1.5, 2.5, 3.5);

			vector.Subtract(vector2);

			VerifyVector(vector, 1.5, 1.5, 1.5);
		}

		[TestMethod]
		public void ScalarMultiplication_CanMultiplyVectorAndScalarWithMultiplyOperator_AndNewMultipliedVectorIsReturned()
		{
			var vector = CreateTestVector();
			var multipliedVector = vector * 3.0;

			VerifyVector(multipliedVector, 9.0, 12.0, 15.0);
		}

		[TestMethod]
		public void ScalarMultiplication_CanMultiplyScalarAndVectorWithMultiplyOperator_AndNewMultipliedVectorIsReturned()
		{
			var vector = CreateTestVector();
			var multipliedVector = 3.0 * vector;

			VerifyVector(multipliedVector, 9.0, 12.0, 15.0);
		}

		[TestMethod]
		public void ScalarMultiplication_CanMultiplyVectorByScalar_AndVectorIsMultipliedByTheScalar()
		{
			var vector = CreateTestVector();
			vector.Multiply(3.0);

			VerifyVector(vector, 9.0, 12.0, 15.0);
		}

		[TestMethod]
		public void ScalarDivision_CanDivideVectorByScalarUsingDivisionOperator_AndNewVectorIsReturned()
		{
			var vector = CreateTestVector();
			var dividedVector = vector / 2.0;

			VerifyVector(dividedVector, 1.5, 2.0, 2.5);
		}

		[TestMethod]
		public void DotProduct_CanGetDotProductUsingMultipyOperator_AndDotProductIsReturned()
		{
			var vector1 = CreateTestVector();
			var vector2 = new Vector3(1.5, 2.5, 3.5);

			var dotProduct = vector1 * vector2;

			Assert.AreEqual(32, dotProduct);
		}

		[TestMethod]
		public void DotProduct_CanGetDotProductFromTwoVectors_AndDotProductIsReturned()
		{
			var vector1 = CreateTestVector();
			var vector2 = new Vector3(1.5, 2.5, 3.5);

			var dotProduct = vector1.DotProduct(vector2);

			Assert.AreEqual(32, dotProduct);
		}

		[TestMethod]
		public void ComponentProduct_CanProduceANewVectorWhichIsTheComponentProductOfTwoVectors()
		{
			var vector1 = CreateTestVector();
			var vector2 = new Vector3(1.5, 2.5, 3.5);

			var componentProduct = vector1.ComponentProduct(vector2);

			VerifyVector(componentProduct, 4.5, 10.0, 17.5);
		}

		[TestMethod]
		public void ComponentProduct_CanUpdateAVectorToBeTheComponentProductOfItselfAndAnotherVector()
		{
			var vector1 = CreateTestVector();
			var vector2 = new Vector3(1.5, 2.5, 3.5);

			vector1.SetComponentProduct(vector2);

			VerifyVector(vector1, 4.5, 10.0, 17.5);
		}

		[TestMethod]
		public void CrossProduct_CanProduceANewVectorThatIsTheCrossProductOfTwoVectors()
		{
			var vector1 = CreateTestVector();
			var vector2 = new Vector3(1.5, 2.5, 3.5);

			var crossProduct = vector1.CrossProduct(vector2);

			VerifyVector(crossProduct, 1.5, -3.0, 1.5);
		}

		[TestMethod]
		public void CrossProduct_CanUpdateAVectorToBeTheCrossProductOfItselfAndAnotherVector()
		{
			var vector1 = CreateTestVector();
			var vector2 = new Vector3(1.5, 2.5, 3.5);

			vector1.SetCrossProduct(vector2);

			VerifyVector(vector1, 1.5, -3.0, 1.5);
		}

		[TestMethod]
		public void Invert_CanInvertAVector_AndTheVectorIsSetToItsInverse()
		{
			var vector = CreateTestVector();
			vector.Invert();

			VerifyVector(vector, -X, -Y, -Z);
		}

		[TestMethod]
		public void SetZero_CanSetTheThreeCoordinatesOfVectorToZero()
		{
			var vector = CreateTestVector();
			vector.SetZero();

			VerifyVector(vector, Zero, Zero, Zero);
		}

		[TestMethod]
		public void Normalise_CanNormaliseAVector_AndTheVectorIsSetToItsNormal()
		{
			var vector = CreateTestVector();
			vector.Normalise();

			VerifyVector(vector, X / ExpectedMagnitude, Y / ExpectedMagnitude, Z / ExpectedMagnitude);
			VerifyNormalHasMagnitudeOfOne(vector.Magnitude);
		}

		[TestMethod]
		public void Normalise_TryingToNormaliseAVectorWithZeroLength_ThrowsAnException()
		{
			var vector = Vector3.ZeroVector;
			try
			{
				vector.Normalise();
				Assert.Fail("Exception was not thrown");
			}
			catch (MagnitudeZeroException ex)
			{
				Assert.AreEqual("Cannot normalise a zero length vector", ex.Message);
			}
		}

		[TestMethod]
		public void Normalise_TryingGetTheNormalOfAVectorWithZeroLength_ThrowsAnException()
		{
			var vector = Vector3.ZeroVector;
			try
			{
				var normal = vector.Normal;
				Assert.Fail("Exception was not thrown");
			}
			catch (MagnitudeZeroException ex)
			{
				Assert.AreEqual("Cannot get the normal of a zero length vector", ex.Message);
			}
		}

		[TestMethod]
		public void Equals_CanDetermineIfTwoVectorsHaveEqualValues_AndTrueIsReturnedIfTheyAre()
		{
			var vector = new Vector3(1.0, 2.0, 3.0);
			var other = new Vector3(1.0, 2.0, 3.0);

			Assert.IsTrue(vector.Equals(other));
		}

		[TestMethod]
		public void HashCode_CanDetermineTheHashCodeForAVector()
		{
			var vector = new Vector3(1.0, 2.0, 3.0);
			var hashCode = vector.GetHashCode();
			Assert.AreEqual(118590513, hashCode);
		}

		[TestMethod]
		public void Equals_CanDetermineIfTwoVectorsHaveEqualValues_AndFalseIsReturnedIfTheyAreNot()
		{
			var vector = new Vector3(1.0, 2.0, 3.0);
			var differentZ = new Vector3(1.0, 2.0, 3.1);
			var differentY = new Vector3(1.0, 2.1, 3.0);
			var differentX = new Vector3(1.1, 2.0, 3.0);

			Assert.IsFalse(vector.Equals(differentZ));
			Assert.IsFalse(vector.Equals(differentY));
			Assert.IsFalse(vector.Equals(differentX));
			Assert.IsFalse(vector.Equals("String"));
		}

		#region private methods

		private Vector3 CreateTestVector()
		{
			return new Vector3(X, Y, Z);
		}

		private void VerifyVector(Vector3 vector, double xValue, double yValue, double zValue)
		{
			Assert.AreEqual(xValue, vector.X);
			Assert.AreEqual(yValue, vector.Y);
			Assert.AreEqual(zValue, vector.Z);
		}

		private void VerifyNormalHasMagnitudeOfOne(double magnitude)
		{
			var difference = Math.Abs(MagnitudeOfNormalVector - magnitude);
			Assert.IsTrue(difference <= RoundingTolerance);
		}

		#endregion
	}
}
