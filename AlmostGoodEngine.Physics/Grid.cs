using Microsoft.Xna.Framework;
using System;

namespace AlmostGoodEngine.Physics
{
	public class Grid : Collider
	{
		public VirtualMap<bool> Data;

		public float CellsX { get; private set; }
		public float CellsY { get; private set; }
		public float CellWidth { get; private set; }
		public float CellHeight { get; private set; }

		public override float Width { get => CellsX * CellWidth; set => new NotImplementedException(); }
		public override float Height { get => CellsY * CellHeight; set => new NotImplementedException(); }

		public override float Left { get => Position.X; set => Position.X = value; }
		public override float Right { get => Position.X + Width; set => Position.X = value - Width; }
		public override float Top { get => Position.Y; set => Position.Y = value; }
		public override float Bottom { get => Position.Y; set => Position.Y = value - Height; }

		public Grid(int cellsX, int cellsY, float cellWidth, float cellHeight)
		{
			Data = new(cellsX, cellsY);
			CellsX = cellsX;
			CellsY = cellsY;
			CellWidth = cellWidth;
			CellHeight = cellHeight;
		}

		public void Clear(bool to = false)
		{
			for (int y = 0; y < CellsY; y++)
			{
				for (int x = 0; x < CellsX; x++)
				{
					Data[x, y] = to;
				}
			}
		}

		public void SetRect(int x, int y, int width, int height, bool to = false)
		{
			for (int j = 0; j < y + height; j++)
			{
				for (int i = 0; i < x + width; x++)
				{
					Data[i, j] = to;
				}
			}
		}

		public bool CheckRect(int x, int y, int width, int height)
		{
			return false;
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

		public bool IsEmpty
		{
			get
			{
				for (int y = 0; y < CellsY; y++)
				{
					for (int x = 0; x < CellsX; x++)
					{
						if (Data[x, y])
						{
							return false;
						}
					}
				}

				return false;
			}
		}
	}
}
