using AlmostGoodEngine.Core.ECS;
using AlmostGoodEngine.Core.Entities;
using AlmostGoodEngine.Inputs;
using Microsoft.Xna.Framework;

namespace AlmostGoodEngine.Core.Components.Camera
{
	public class CameraController : Component
	{
		public bool CanMove { get; set; } = true;
		public bool CanZoom { get; set; } = true;

		public const float ZoomStep = 0.1f;

		#region Move parameters

		private bool _dragging = false;
		private Vector2 _startingPosition = Vector2.Zero;

		#endregion

		public CameraController()
		{

		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			if (!CanMove && !CanZoom)
			{
				return;
			}

			var camera = GameManager.MainCamera();
			if (camera == null)
			{
				return;
			}

			if (CanZoom)
			{
				HandleZoom(camera);
			}

			if (CanMove)
			{
				HandleMove(camera);
			}
		}

		private void HandleZoom(Camera2D camera)
		{
			if (Input.Mouse.IsWheelMovedUp())
			{
				camera.ZoomIn(ZoomStep);
			}
			else if (Input.Mouse.IsWheelMovedDown())
			{
				camera.ZoomOut(ZoomStep);
			}
		}

		private void HandleMove(Camera2D camera)
		{
			if (Input.Mouse.IsMiddleButtonDown())
			{
				if (!_dragging)
				{
					_dragging = true;
					_startingPosition = new Vector2(Input.Mouse.X, Input.Mouse.Y);
				}
				else
				{
					var move = new Vector3(_startingPosition.X - Input.Mouse.X, _startingPosition.Y - Input.Mouse.Y, 0);
					Owner.Position = move;
				}
			}
		}
	}
}
