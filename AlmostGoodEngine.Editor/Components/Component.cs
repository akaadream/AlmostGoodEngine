using System.Numerics;

namespace AlmostGoodEngine.Editor.Components
{
	public abstract class Component
	{
		public virtual Vector2 Draw()
		{
			return Vector2.Zero;
		}
	}
}
