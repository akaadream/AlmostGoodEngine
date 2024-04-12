using Microsoft.Xna.Framework;
using System;
using System.Linq;

namespace AlmostGoodEngine.Physics
{
	public class Colliders(params Colliders[] colliders) : Collider
	{
		public Collider[] colliders { get; private set; } = colliders;

		public override float Width { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public override float Height { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public override float Top { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public override float Left { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public override float Right { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public override float Bottom { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		public void Add(params Colliders[] add)
		{
			foreach (var collider in add)
			{
				if (colliders.Contains(collider))
				{
					return;
				}
			}

			var nextList = new Collider[colliders.Length + add.Length];
			for (int i = 0; i < colliders.Length; i++)
			{
				nextList[i] = colliders[i];
			}
			for (int i = 0; i < add.Length; i++)
			{
				nextList[i + colliders.Length] = add[i];
			}
			colliders = nextList;
		}

		public void Remove(params Collider[] remove)
		{
			int contained = 0;
			foreach (var collider in remove)
			{
				if (colliders.Contains(collider))
				{
					contained++;
				}
			}

			var nextList = new Collider[colliders.Length - contained];
			int index = 0;
			for (var i = 0; i < colliders.Length; i++)
			{
				if (remove.Contains(colliders[i]))
				{
					continue;
				}

				nextList[index] = colliders[i];
				index++;
			}

			colliders = nextList;
		}

		public override bool Collide(Rectangle rectangle)
		{
			throw new NotImplementedException();
		}

		public override bool Collide(Vector2 position)
		{
			throw new NotImplementedException();
		}

		public override bool Collide(Point position)
		{
			throw new NotImplementedException();
		}

		public override bool Collide(BoxCollider2D box)
		{
			throw new NotImplementedException();
		}

		public override bool Collide(Grid grid)
		{
			throw new NotImplementedException();
		}

		public override bool Collide(CircleCollider2D circle)
		{
			throw new NotImplementedException();
		}

		public override bool Collide(Colliders colliders)
		{
			throw new NotImplementedException();
		}
	}
}
