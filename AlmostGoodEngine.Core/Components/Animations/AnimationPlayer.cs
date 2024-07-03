using AlmostGoodEngine.Animations;
using AlmostGoodEngine.Core.ECS;
using Microsoft.Xna.Framework;

namespace AlmostGoodEngine.Core.Components.Animations
{
	public class AnimationPlayer : Component
	{
		public AnimationTree AnimationTree { get; set; }

		public override void AnimationsUpdate(GameTime gameTime)
		{
			base.Update(gameTime);

			AnimationTree.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
		}
	}
}
