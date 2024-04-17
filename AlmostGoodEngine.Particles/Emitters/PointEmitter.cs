using Microsoft.Xna.Framework;

namespace AlmostGoodEngine.Particles.Emitters
{
	public class PointEmitter(Vector2 position) : Emitter
	{
		public Vector2 Position { get; set; } = position;

		public override Vector2 Next()
		{
			return Position;
		}

		public override Rectangle Bounds()
		{
			return new Rectangle((int)Position.X, (int)Position.Y, 1, 1);
		}
	}
}
