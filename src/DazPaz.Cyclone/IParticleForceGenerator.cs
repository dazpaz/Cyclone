namespace DazPaz.Cyclone
{
	public interface IParticleForceGenerator
	{
		void UpdateForce(IParticle particle, double duration);
	}
}
