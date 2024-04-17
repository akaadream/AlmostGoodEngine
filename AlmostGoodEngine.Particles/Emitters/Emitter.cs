using Microsoft.Xna.Framework;

namespace AlmostGoodEngine.Particles.Emitters
{
	public abstract class Emitter
	{
		public virtual Rectangle Bounds()
		{
			return Rectangle.Empty;
		}

		public virtual Vector2 Next()
		{
			return Vector2.Zero;
		}
	}
}
