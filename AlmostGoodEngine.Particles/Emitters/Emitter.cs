using Microsoft.Xna.Framework;

namespace AlmostGoodEngine.Particles.Emitters
{
	public abstract class Emitter
	{
		public virtual Rectangle Bounds()
		{
			return Rectangle.Empty;
		}

		public virtual Vector3 Next()
		{
			return Vector3.Zero;
		}
	}
}
