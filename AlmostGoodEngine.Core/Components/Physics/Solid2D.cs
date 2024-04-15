using AlmostGoodEngine.Core.ECS;
using AlmostGoodEngine.Core.Entities;
using AlmostGoodEngine.Core.Utils;
using AlmostGoodEngine.Extended;
using AlmostGoodEngine.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace AlmostGoodEngine.Core.Components.Physics
{
	public class Solid2D : Entity
	{
		private float _remainderX { get; set; }
		private float _remainderY { get; set; }

		public Collider Collider { get; set; }
		public bool Collidable { get; set; } = true;
		private bool previouslyCollidable = true;

		public void Move(float x, float y)
		{
			_remainderX += x;
			_remainderY += y;
			int moveX = BetterMath.Roundi(_remainderX);
			int moveY = BetterMath.Roundi(_remainderY);
			if (moveX != 0 || moveY != 0)
			{
				List<Actor2D> riding = Scene.GetAllRidingActors(this);
				previouslyCollidable = Collidable;
				Collidable = false;

				if (moveX != 0)
				{
					_remainderX -= moveX;
					Position += Vector3.Right * moveX;
					foreach (Actor2D actor in Scene.GetActors())
					{
						if (PhysicsManager.OverlapCheck(Collider, actor.Collider))
						{
							if (moveX > 0)
							{
								// Push the actor right
								actor.MoveX(Collider.Right - actor.Collider.Left, actor.Squish);
							}
							else
							{
								// Push the actor left
								actor.MoveX(Collider.Left - actor.Collider.Right, actor.Squish);
							}
						}
						else if (riding.Contains(actor))
						{
							// Carry right/left
							actor.MoveX(moveX);
						}
					}
				}

				if (moveY != 0)
				{
					_remainderY -= moveY;
					Position += Vector3.Up * moveY;
					foreach (Actor2D actor in Scene.GetActors())
					{
						if (PhysicsManager.OverlapCheck(Collider, actor.Collider))
						{
							if (moveY < 0)
							{
								// Push the actor up
								actor.MoveY(Collider.Top - actor.Collider.Bottom, actor.Squish);
							}
							else
							{
								// Push the actor down
								actor.MoveY(Collider.Bottom - actor.Collider.Top, actor.Squish);
							}
						}
						else if (riding.Contains(actor))
						{
							// Carry up/down
							actor.MoveY(moveY);
						}
					}
				}

				Collidable = previouslyCollidable;
			}
		}

		public override void BeforeUpdate(GameTime gameTime)
		{
			base.BeforeUpdate(gameTime);
			Collider.Position = Position.ToVector2();
		}

		public override void DrawDebug(GameTime gameTime, SpriteBatch spriteBatch)
		{
			base.DrawDebug(gameTime, spriteBatch);

			Debug.FillRectangle(spriteBatch, Collider.Bounds, Color.Blue * 0.5f);
		}
	}
}
