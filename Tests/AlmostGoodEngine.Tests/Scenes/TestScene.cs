using AlmostGoodEngine.Core;
using AlmostGoodEngine.Core.Components;
using AlmostGoodEngine.Core.Components.Camera;
using AlmostGoodEngine.Core.Components.Curves;
using AlmostGoodEngine.Core.Components.Rendering;
using AlmostGoodEngine.Core.ECS;
using AlmostGoodEngine.Core.Entities;
using AlmostGoodEngine.Core.Scenes;
using AlmostGoodEngine.Core.Utils;
using AlmostGoodEngine.Inputs;
using AlmostGoodEngine.Tests.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace AlmostGoodEngine.Tests.Scenes
{
    internal class TestScene : Scene
    {
        readonly Player player;
        readonly Player player_two;

        Viewport viewport { get => GameManager.Engine.GraphicsDevice.Viewport; }

        public TestScene() : base()
        {
            Entity entity = new();
            entity.AddComponent(new DebugObject());

            Entity pathEntity = new();
			//entity.AddComponent(new DrawPath());

			AddEntity(entity);
            //GameObjects.Add(pathEntity);

            player = new();
			AddEntity(player);

            player_two = new();
            player_two.Position += Vector3.Left * 60;
            //GameObjects.Add(player_two);

            Entity hitBoxEntity = new();
            Hitbox hitbox = new(new(300, 250, 100, 100))
            {
                DisplayDebug = true,
            };
            hitbox.OnEntityEnter += Hitbox_OnEntityEnter;
            hitbox.OnEntityExit += Hitbox_OnEntityExit;

            hitBoxEntity.AddComponent(hitbox);
            AddEntity(hitBoxEntity);


            //camera.AddComponent(new CameraLimit(new(-470, -320, 1), new(470, 320, 1)));

            // First player camera
            if (Renderer.Cameras.Count > 0)
            {
                //Renderer.Cameras[0].Viewport = new Viewport(0, 0, viewport.Width / 2, viewport.Height / 2);
                Renderer.Cameras[0].AddComponent(new FollowTarget(Renderer.Cameras[0], player)
                {
                    Limit = true,
                    Min = new(0, 0, 1),
                    Max = new(0, 0, 1)
				});
                Renderer.Cameras[0].AddComponent(new CameraController());
            }

            //var camera_two = new Camera2D(new Viewport(viewport.Width / 2, 0, viewport.Width / 2, viewport.Height / 2))
            //{
            //	Position = Vector3.Zero,
            //	Offset = new(viewport.Width / 2, 0),
            //};
            //camera_two.AddComponent(new FollowTarget(camera_two, player));
            //Renderer.Cameras.Add(camera_two);

            //var camera_three = new Camera2D(new Viewport(0, viewport.Height / 2, viewport.Width / 2, viewport.Height / 2))
            //{
            //	Position = Vector3.Zero,
            //	Offset = new(0, viewport.Height / 2),
            //};
            //camera_three.AddComponent(new FollowTarget(camera_three, player));
            //Renderer.Cameras.Add(camera_three);

            //var camera_four = new Camera2D(new Viewport(viewport.Width / 2, viewport.Height / 2, viewport.Width / 2, viewport.Height / 2))
            //{
            //	Position = Vector3.Zero,
            //	Offset = new(viewport.Width / 2, viewport.Height / 2),
            //};
            //camera_four.AddComponent(new FollowTarget(camera_four, player));
            //Renderer.Cameras.Add(camera_four);
        }

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

            if (GameManager.MainCamera() == null)
            {
                return;
            }

            if (Input.Keyboard.IsPressed(Microsoft.Xna.Framework.Input.Keys.T))
            {
				Random random = new();
				GameManager.MainCamera().BackgroundColor = new Color(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
            }
		}

		private void Hitbox_OnEntityEnter(object sender, Core.Events.EntityEventArgs e)
        {

            Text text = player.GetComponent<Text>();
            if (text != null)
            {
                text.Color = Color.White;
            }
            Logger.Log("The entity " + e.Entity.Tags.ToString() + " entered the hitbox");
        }

        private void Hitbox_OnEntityExit(object sender, Core.Events.EntityEventArgs e)
        {
            Text text = player.GetComponent<Text>();
            if (text != null)
            {
                text.Color = Color.Transparent;
            }
            Logger.Log("The entity " + e.Entity.Tags.ToString() + " exited the hitbox");
        }

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
            
            var camera = GameManager.MainCamera();
            int tileSize = 16 * 4;
            if (camera != null)
            {
				spriteBatch.Begin(transformMatrix: camera.GetTransform());
				// Draw the grid
				// x
				for (int y = 0; y <= camera.Height; y += tileSize)
                {
                    Debug.Line(
                        spriteBatch,
                        new Vector2(0, y),
                        new Vector2(camera.Width, y),
                        Color.White * 0.25f, 4f);
                }
                // y
				for (int x = 0; x <= camera.Width; x += tileSize)
				{
					Debug.Line(
						spriteBatch,
						new Vector2(x, 0),
						new Vector2(x, camera.Height),
						Color.White * 0.25f, 4f);
				}
				spriteBatch.End();
			}

			base.Draw(gameTime, spriteBatch);

            if (camera != null)
            {
                //spriteBatch.Begin(transformMatrix: camera.GetTransform());
                //Debug.Rectangle(
                //    spriteBatch,
                //    new Rectangle(
                //        (int)(WorldMousePosition().X) / tileSize * tileSize,
                //        (int)(WorldMousePosition().Y) / tileSize * tileSize,
                //        tileSize,
                //        tileSize),
                //    Color.YellowGreen * 0.6f,
                //    4f);
                //spriteBatch.End();
            }
		}
	}
}
