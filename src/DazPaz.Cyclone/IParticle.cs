namespace DazPaz.Cyclone
{
	public interface IParticle
	{
		Vector3 Position { get; set; }
		Vector3 Velocity { get; set; }
		double Gravity { get; set; }
		double Damping { get; set; }
		double InverseMass { get; set; }
		double Mass { get; set; }
		bool IsInfiniteMass { get; }

		void AddForce(Vector3 force);
		void Integrate(float duration);
	}
}
