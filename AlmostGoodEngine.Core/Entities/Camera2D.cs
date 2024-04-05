using AlmostGoodEngine.Core.ECS;
using AlmostGoodEngine.Core.Utils;
using AlmostGoodEngine.Extended;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlmostGoodEngine.Core.Entities
{
    public class Camera2D : Entity
    {
        /// <summary>
        /// The viewport of the camera
        /// </summary>
        public Viewport Viewport
        {
            get => _viewport;
            set
            {
                _viewport = value;
                ComputeMatrixes();
            }
        }
        private Viewport _viewport;

        public int Width { get => Viewport.Width; }
        public int Height { get => Viewport.Height; }

        /// <summary>
        /// The zoom of the camera
        /// </summary>
        public float Zoom { get; set; } = 1f;

        /// <summary>
        /// Rotation angle
        /// </summary>
        public Angle Rotation { get; set; } = Angle.Zero;

        /// <summary>
        /// The sampler used by the camera
        /// </summary>
        public SamplerState SamplerState { get; set; } = SamplerState.PointClamp;

        /// <summary>
        /// The position of the entity on the 2D screen space
        /// </summary>
        public Vector2 Position2D
        { 
            get => Position.ToVector2();
        }

        /// <summary>
        /// Min distance where the content is displayed
        /// </summary>
        public float ZNearPlane { get; set; } = 0;

        /// <summary>
        /// Max distance where the content is displayed
        /// </summary>
        public float ZFarPlane { get; set; } = 1;

        /// <summary>
        /// The offset of the camera
        /// </summary>
        public Vector2 Offset { get; set; } = Vector2.Zero;

        /// <summary>
        /// View matrix
        /// </summary>
        public Matrix View { get; set; }

        /// <summary>
        /// Projection matrix
        /// </summary>
        public Matrix Projection { get; set; }

        /// <summary>
        /// Screen matrix
        /// </summary>
        public Matrix Screen { get; set; }

        /// <summary>
        /// The zoom matrix
        /// </summary>
        public Matrix ZoomMatrix { get => Matrix.CreateScale(Zoom, Zoom, 1f); }

        public Color BackgroundColor { get; set; } = Color.Transparent;

        public Camera2D(int width, int height)
        {
            Viewport = new((int)Position.X, (int)Position.Y, width, height);
            ComputeMatrixes();
        }

        public Camera2D(Viewport viewport)
        {
            Viewport = viewport;
            ComputeMatrixes();
        }

        public void Move(Vector3 amount)
        {
            Position += amount;
            ComputeMatrixes();
        }

        public void SetPosition(Vector3 position)
        {
            Position = position;
            ComputeMatrixes();
        }

        public void SetZoom(float zoom)
        {
            Zoom = zoom;
            ComputeMatrixes();
        }

        public void ZoomIn(float amount)
        {
            Zoom += amount;
            ComputeMatrixes();
        }

        public void ZoomOut(float amount)
        {
            Zoom -= amount;
            if (Zoom < 0.1f)
            {
                Zoom = 0.1f;
            }
            ComputeMatrixes();
        }

        /// <summary>
        /// Create the view matrix
        /// </summary>
        /// <returns></returns>
        public Matrix ViewMatrix()
        {
            return Matrix.CreateTranslation(new Vector3((int)-Position.X, (int)-Position.Y, 0)) *
                Matrix.CreateRotationZ(Rotation.Radians) *
                Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                Matrix.CreateTranslation(new Vector3(Offset.X, Offset.Y, 0));
        }

        /// <summary>
        /// Create the projection matrix
        /// </summary>
        /// <returns></returns>
        public Matrix ProjectionMatrix()
        {
            return Matrix.CreateOrthographicOffCenter(0, Viewport.Width, Viewport.Height, 0, ZNearPlane, ZFarPlane);
        }

        public Matrix ScreenMatrix()
        {
            return Matrix.CreateScale(Viewport.Width / Viewport.Width);
        }

        /// <summary>
        /// Get the transformation matrix used to display content
        /// </summary>
        /// <returns></returns>
        public Matrix GetTransform()
        {
            return View;
        }

        /// <summary>
        /// Compute the view and projection matrixes
        /// </summary>
        internal void ComputeMatrixes()
        {
            View = ViewMatrix();
            Projection = ProjectionMatrix();
        }

        /// <summary>
        /// Get a position in the world of the screen coordinate
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Vector2 ScreenToWorld(Vector2 position)
        {
            Matrix inverseView = Matrix.Invert(View);
            return Vector2.Transform(position, inverseView);
        }

        /// <summary>
        /// Get a position in the screen of a world coordinate
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Vector2 WorldToScreen(Vector2 position)
        {
            return Vector2.Transform(position, View);
        }

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			if (BackgroundColor == Color.Transparent)
            {
                return;
            }

            Debug.FillRectangle(spriteBatch, Viewport.Bounds, BackgroundColor);
		}
	}
}
