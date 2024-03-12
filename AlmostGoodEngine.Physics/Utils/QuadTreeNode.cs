using AlmostGoodEngine.Physics.Extends;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmostGoodEngine.Physics.Utils
{
	public struct QuadTreeNode
	{
		public Rectangle Bounds { get; set; }
		public List<Collider> Objects { get; set; }
		public QuadTreeNode[] Children { get; set; }

		public int MaxObjects { get; private set; }
		public int MaxLevels { get; private set; }

		public QuadTreeNode(Rectangle bounds, int maxObjects = 4, int maxLevels = 4)
		{
			Bounds = bounds;
			Objects = new List<Collider>();
			Children = new QuadTreeNode[4];
			MaxObjects = maxObjects;
			MaxLevels = maxLevels;

			if (maxObjects <= 0 || maxLevels <= 0)
			{
				return;
			}

			int halfWidth = bounds.Size.X / 2;
			int halfHeight = bounds.Size.Y / 2;

			Children[0] = new(new Rectangle(bounds.X - halfWidth, bounds.Y + halfHeight, halfWidth, halfHeight), maxObjects, maxLevels - 1);
			Children[1] = new(new Rectangle(bounds.X + halfWidth, bounds.Y + halfHeight, halfWidth, halfHeight), maxObjects, maxLevels - 1);
			Children[2] = new(new Rectangle(bounds.X - halfWidth, bounds.Y - halfHeight, halfWidth, halfHeight), maxObjects, maxLevels - 1);
			Children[3] = new(new Rectangle(bounds.X + halfWidth, bounds.Y - halfHeight, halfWidth, halfHeight), maxObjects, maxLevels - 1);
		}

		public void Insert(Collider collider)
		{
			// If there is no intersection between the tree bounds and the collider bounds, do not insert it
			if (!Bounds.Intersects(collider.GetRectangle()))
			{
				return;
			}

			// Add the collider to the objects list
			Objects.Add(collider);

			// If the node need to be divided
			//if (Children[0] == null || Objects.Count <= Children[0].MaxObjects)
			//{
			//	return;
			//}

			// Divide objects between children and node
			foreach (var child in Children)
			{
				for (int i = Objects.Count - 1; i >= 0; i--)
				{
					if (child.Bounds.Intersects(Objects[i].GetRectangle()))
					{
						child.Insert(Objects[i]);
						Objects.RemoveAt(i);
					}
				}
			}
		}
	}
}
