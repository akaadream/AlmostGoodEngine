using AlmostGoodEngine.Core.Components.Physics;
using AlmostGoodEngine.Core.ECS;
using AlmostGoodEngine.Core.Utils;
using AlmostGoodEngine.Extended;
using AlmostGoodEngine.Physics;
using Microsoft.Xna.Framework;
using System;

namespace AlmostGoodEngine.Core.Entities
{
	/// <summary>
	/// Class inspired of the Towerfall / Celeste game physics: https://www.maddymakesgames.com/articles/celeste_and_towerfall_physics/index.html
	/// </summary>
	public class Actor2D : Entity
	{
		private float _remainderX = 0f;
		private float _remainderY = 0f;

		public Collider Collider { get; set; }

		/// <summary>
		/// Move the given x and y
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="onCollide"></param>
		public void Move(float x, float y, Action onCollide = null)
		{
			MoveX(x, onCollide);
			MoveY(y, onCollide);
		}

		/// <summary>
		/// Move on X axis the given amount
		/// </summary>
		/// <param name="amount"></param>
		/// <param name="onCollide"></param>
		public void MoveX(float amount, Action onCollide = null)
		{
			_remainderX += amount;
			int move = BetterMath.Roundi(amount);
            if (move != 0)
            {
				_remainderX -= move;
				int sign = Math.Sign(move);

				// There is still a move to do
				while (move != 0)
				{
					// Check collisions
					if (!PhysicsManager.CollideAt(Scene.GetColliders(), Position.ToVector2() + new Vector2(sign, 0)))
					{
						// No collider beside the actor
						Position += Vector3.Right * sign;
						move -= sign;
					}
					else
					{
						// We hurt a collider
						onCollide?.Invoke();
						// Stop any further movement
						break;
					}
				}
			}
		}

		/// <summary>
		/// Move on Y axis the given amount
		/// </summary>
		/// <param name="amount"></param>
		/// <param name="onCollide"></param>
		public void MoveY(float amount, Action onCollide = null)
		{
			_remainderY += amount;
			int move = BetterMath.Roundi(amount);
			if (move != 0)
			{
				_remainderY -= move;
				int sign = Math.Sign(move);

				// There is still a move to do
				while (move != 0)
				{
					// Check collisions
					if (!PhysicsManager.CollideAt(Scene.GetColliders(), Position.ToVector2() + new Vector2(0, sign)))
					{
						// No collider beside the actor
						Position += Vector3.Up * sign;
						move -= sign;
					}
					else
					{
						// We hurt a collider
						onCollide?.Invoke();
						// Stop any further movement
						break;
					}
				}
			}
		}

		/// <summary>
		/// Return true if the actor is riding the given solid
		/// You should implement your own version of riding check
		/// </summary>
		/// <param name="solid"></param>
		/// <returns></returns>
		public virtual bool IsRiding(Solid2D solid)
		{
			return false;
		}

		/// <summary>
		/// Called when the actor is blocked between two solids
		/// </summary>
		public virtual void Squish()
		{

		}
	}
}
