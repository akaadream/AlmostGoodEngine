using AlmostGoodEngine.Core.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace AlmostGoodEngine.Core.Curves
{
	public class Path2D
	{
		public Curve2D Curve { get; set; }

		public List<Vector2> Points { get; set; }
		public List<Vector2> CurvePoints { get; set; }

		public bool DisplayConstructLines { get; set; } = true;

		public Path2D()
		{
			Curve = new();
			Points = new();
			CurvePoints = new();
		}

		public void AddPoint(Vector2 point)
		{
			//Curve.AddControlPoint(point);

			if (IsNearly(point) && GetNearly(point) == Points[0])
			{
				Points.Add(Points[0]);
			}
			else if (!IsNearly(point))
			{
				Points.Add(point);
			}
			
			Bake();
		}

		public void MovePoint(int index, Vector2 position)
		{
			Points[index] = position;
			Bake();
		}

		public void RemovePoint(Vector2 point)
		{
			//Curve.RemoveControlPoint(point);
			Points.Remove(point);
			Bake();
		}

		public Vector2 GetNearly(Vector2 position)
		{
			foreach (var point in Points)
			{
				if (PointNearly(position, point))
				{
					return point;
				}
			}

			return Vector2.Zero;
		}

		public bool IsNearly(Vector2 position, float range = 40)
		{
			foreach (var point in Points)
			{
				if (PointNearly(position, point, range))
				{
					return true;
				}
			}

			return false;
		}

		private bool PointNearly(Vector2 position, Vector2 point, float range = 40)
		{
			return (point - position).Length() <= range;
		}

		public void RemovePointNear(Vector2 position, float range = 40)
		{
			for (int i = Points.Count - 1; i >= 0; i--)
			{
				if (PointNearly(position, Points[i], range))
				{
					Points.Remove(Points[i]);
					Bake();
					return;
				}
			}
		}

		public void GetPositionAt(float t)
		{
			Curve.GetPosition(t);
		}

		public void Bake(int subdivisions = 24)
		{
			CurvePoints = Curve.GetCurvePoints(Points, subdivisions);
		}

		public void Update()
		{

		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (Points.Count == 1)
			{
				Debug.Rectangle(spriteBatch, new Rectangle((int)Points[0].X - 4, (int)Points[0].Y - 4, 7, 7), Color.Black);
				Debug.FillRectangle(spriteBatch, new Rectangle((int)Points[0].X - 3, (int)Points[0].Y - 3, 6, 6), Color.White);
				return;
			}

			for (int i = 0; i < CurvePoints.Count - 1; i++)
			{
				Debug.Line(spriteBatch, CurvePoints[i], CurvePoints[i + 1], Color.Blue * 0.8f);
			}

			if (DisplayConstructLines)
			{
				for (int i = 0; i < Points.Count - 1; i++)
				{
					Debug.Line(spriteBatch, Points[i], Points[i + 1], Color.Green * 0.8f);
					Debug.Rectangle(spriteBatch, new Rectangle((int)Points[i].X - 4, (int)Points[i].Y - 4, 7, 7), Color.Black);
					Debug.FillRectangle(spriteBatch, new Rectangle((int)Points[i].X - 3, (int)Points[i].Y - 3, 6, 6), Color.White);

					if (i == Points.Count - 2)
					{
						Debug.Rectangle(spriteBatch, new Rectangle((int)Points[i + 1].X - 4, (int)Points[i + 1].Y - 4, 7, 7), Color.Black);
						Debug.FillRectangle(spriteBatch, new Rectangle((int)Points[i + 1].X - 3, (int)Points[i + 1].Y - 3, 6, 6), Color.White);
					}
				}
			}
		}
	}
}
