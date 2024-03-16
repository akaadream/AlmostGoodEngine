using AlmostGoodEngine.Core.Curves;
using AlmostGoodEngine.Core.ECS;
using AlmostGoodEngine.Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlmostGoodEngine.Core.Components.Curves
{
	public class DrawPath : Component
	{
		Vector2 mousePosition = Vector2.Zero;
		Path2D path;
		const float range = 20;
		bool dragging = false;
		Vector2 pointPosition = Vector2.Zero;
		int pointIndex = -1;

		public override void Start()
		{
			path = new();
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			mousePosition = GameManager.MainCamera().ScreenToWorld(new(Input.Mouse.X, Input.Mouse.Y));

			if (!dragging)
			{
				if (Input.Mouse.X < 0 || Input.Mouse.Y < 0 || Input.Mouse.X > GameManager.MainCamera().Width || Input.Mouse.Y > GameManager.MainCamera().Height)
				{
					return;
				}

				// Add a point on the scene
				if (Input.Mouse.IsLeftButtonPressed() && !path.IsNearly(mousePosition))
				{
					path.AddPoint(mousePosition);
				}
				else if (Input.Mouse.IsRightButtonPressed())
				{
					path.RemovePointNear(mousePosition, range);
				}
			}

			// Now down
			if (Input.Mouse.IsLeftButtonDown())
			{
				if (Input.Mouse.PreviousState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
				{
					if (path.IsNearly(mousePosition))
					{
						dragging = true;
						pointPosition = path.GetNearly(mousePosition);
						pointIndex = path.Points.IndexOf(pointPosition);
					}
				}
				else if (dragging)
				{
					path.MovePoint(pointIndex, mousePosition);
					path.Points[pointIndex] = mousePosition;
				}
			}
			else if (Input.Mouse.IsLeftButtonReleased() && dragging)
			{
				dragging = false;
				path.Points[pointIndex] = mousePosition;
				pointIndex = -1;
				pointPosition = Vector2.Zero;
			}

			if (Input.Keyboard.IsPressed(Microsoft.Xna.Framework.Input.Keys.R))
			{
				path.Points.Clear();
			}

			if (Input.Keyboard.IsPressed(Microsoft.Xna.Framework.Input.Keys.G))
			{
				path.DisplayConstructLines = !path.DisplayConstructLines;
			}
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			base.Draw(gameTime, spriteBatch);

			path.Draw(spriteBatch);
		}
	}
}
