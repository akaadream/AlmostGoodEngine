using Microsoft.Xna.Framework;

namespace AlmostGoodEngine.Particles.Emitters
{
	public abstract class Emitter
	{
		public virtual Vector3 Next()
		{
			return Vector3.Zero;
		}
	}
}
