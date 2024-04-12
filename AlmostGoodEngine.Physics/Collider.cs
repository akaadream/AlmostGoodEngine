using Microsoft.Xna.Framework;

namespace AlmostGoodEngine.Physics
{
	/**
	 * Inspired by the physics engine of Monocle from Maddy Thorson
	 * 
	 **/
	public abstract class Collider
	{
		public Vector2 Position;

		public abstract float Width { get; set; }
		public abstract float Height { get; set; }
		public abstract float Top { get; set; }
		public abstract float Left { get; set; }
		public abstract float Right { get; set; }
		public abstract float Bottom { get; set; }

		public bool Collide(Collider other)
		{
			if (other is BoxCollider2D boxCollider2D)
			{
				return Collide(boxCollider2D);
			}

			if (other is Grid grid)
			{
				return Collide(grid);
			}

			if (other is Colliders colliders)
			{
				return Collide(colliders);
			}

			if (other is CircleCollider2D circleCollider2D)
			{
				return Collide(circleCollider2D);
			}

			throw new System.Exception("The given collider type is not implemented!");
		}

		public abstract bool Collide(Rectangle rectangle);
		public abstract bool Collide(Vector2 position);
		public abstract bool Collide(Point position);

		public abstract bool Collide(BoxCollider2D box);
		public abstract bool Collide(Grid grid);
		public abstract bool Collide(CircleCollider2D circle);
		public abstract bool Collide(Colliders colliders);

		public void CenterOrigin()
		{
			Position.X = -Width / 2;
			Position.Y = -Height / 2;
		}

		public float CenterX
		{
			get => Left + Width / 2f;
			set => Left = value - Width / 2f;
		}

		public float CenterY
		{
			get => Top + Height / 2f;
			set => Top = value - Height / 2f;
		}

		public Vector2 TopLeft
		{
			get => new(Left, Top);
			set
			{
				Left = value.X;
				Top = value.Y;
			}
		}

		public Vector2 TopCenter
		{
			get => new(CenterX, Top);
			set
			{
				CenterX = value.X;
				Top = value.Y;
			}
		}

		public Vector2 TopRight
		{
			get => new(Right, Top);
			set
			{
				Right = value.X;
				Top = value.Y;
			}
		}

		public Vector2 CenterLeft
		{
			get => new(Left, CenterY);
			set
			{
				Left = value.X;
				CenterY = value.Y;
			}
		}

		public Vector2 Center
		{
			get => new(CenterX, CenterY);
			set
			{
				CenterX = value.X;
				CenterY = value.Y;
			}
		}

		public Vector2 Size
		{
			get => new(Width, Height);
		}

		public Vector2 HalfSize
		{
			get => Size * 0.5f;
		}

		public Vector2 CenterRight
		{
			get => new(Right, CenterY);
			set
			{
				Right = value.X;
				CenterY = value.Y;
			}
		}

		public Vector2 BottomLeft
		{
			get => new(Left, Bottom);
			set
			{
				Left = value.X;
				Bottom = value.Y;
			}
		}

		public Vector2 BottomCenter
		{
			get => new(CenterX, Bottom);
			set
			{
				CenterX = value.X;
				Bottom = value.Y;
			}
		}

		public Vector2 BottomRight
		{
			get => new(Right, Bottom);
			set
			{
				Right = value.X;
				Bottom = value.Y;
			}
		}

		public Rectangle Bounds
		{
			get => new((int)Left, (int)Top, (int)Width, (int)Height);
		}
	}
}
