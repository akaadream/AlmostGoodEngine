using AlmostGoodEngine.Animations.Coroutine;
using AlmostGoodEngine.Animations.Utils;
using AlmostGoodEngine.Core;
using AlmostGoodEngine.Core.Components;
using AlmostGoodEngine.Core.Components.Animations;
using AlmostGoodEngine.Core.Components.Rendering;
using AlmostGoodEngine.Core.Components.Timing;
using AlmostGoodEngine.Core.ECS;
using AlmostGoodEngine.Core.Entities;
using AlmostGoodEngine.Core.Utils;
using AlmostGoodEngine.Extended;
using AlmostGoodEngine.Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace AlmostGoodEngine.Tests.GameObjects
{
    internal class Player : Actor2D
    {
        Text timerText;
        Timer timer;

        Vector3 velocity = Vector3.Zero;
        float speed = 390f;

        public Text TestText { get; set; }

        public Player()
        {
            Tags = ["Player"];
            Scale = new(4f);
            Position = new(100, 100, 1);
        }

        public override void LoadContent(ContentManager content)
        {
            TestText = new("This is a test text", 32)
            {
                Position = new Vector2(100, 100),
                Anchor = Anchor.MiddleCentered,
                TextAnchor = Anchor.MiddleCentered,
                DisplayInsideWorld = false,
                Color = Color.White
            };
            AddComponent(TestText);

            Texture2D playerTexture = content.Load<Texture2D>("Sprites/character");
            if (playerTexture != null)
            {
                AnimatedSprite2D animatedSprite2D = new(16, 19, 4, playerTexture);
                animatedSprite2D.SpriteSheet.Scale = 4f;
                animatedSprite2D.OnMouseClicked += (object sender, EventArgs args) =>
                {
                    Logger.Log("Sprite clicked");
                };
                AddComponent(animatedSprite2D);

                Collider = new Physics.BoxCollider2D(-20, 12, 42, 22)
                {
                    Position = Position.ToVector2()
                };
			}

            Hitbox hitbox = new(new Rectangle(-5, 4, 10, 5), false)
            {
                DisplayDebug = false,
            };
            AddComponent(hitbox);

            //Sprite2D sprite2D = new(content.Load<Texture2D>("Sprites/character"))
            //{
            //    Source = new(0, 0, 16, 24),
            //};
            //AddComponent(sprite2D);

            timer = new(10)
            {
                Looping = true,
            };
            timer.Launch();
            AddComponent(timer);

            timerText = new("Remaining time: 10s", 32)
            {
                Position = new Vector2(15, 15),
                Anchor = Anchor.BottomRight,
                TextAnchor = Anchor.BottomRight
            };
            AddComponent(timerText);
        }

		public override void BeforeUpdate(GameTime gameTime)
		{
            Collider.Position = Position.ToVector2();

			base.BeforeUpdate(gameTime);
		}

		public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

			//Position = new(Input.Mouse.X, Input.Mouse.Y, 1);

			timerText.Value = "Remaining time: " + timer.ToString() + "s";

            if (Input.Keyboard.IsPressed(Keys.C))
            {
                Coroutines.StartCoroutine(FadeOut());
            }

            if (Input.Keyboard.IsPressed(Keys.D))
            {
                Logger.Log(Position.ToVector2().ToString());
            }

            Text text = GetComponent<Text>();
            if (text != null)
            {
                if (Input.Keyboard.IsPressed(Keys.NumPad7))
                {
                    text.Anchor = Anchor.TopLeft;
                    text.TextAnchor = Anchor.TopLeft;
                }

                if (Input.Keyboard.IsPressed(Keys.NumPad8))
                {
                    text.Anchor = Anchor.TopCentered;
                    text.TextAnchor = Anchor.TopCentered;
                }

                if (Input.Keyboard.IsPressed(Keys.NumPad9))
                {
                    text.Anchor = Anchor.TopRight;
                    text.TextAnchor = Anchor.TopRight;
                }

                if (Input.Keyboard.IsPressed(Keys.NumPad4))
                {
                    text.Anchor = Anchor.MiddleLeft;
                    text.TextAnchor = Anchor.MiddleLeft;
                }

                if (Input.Keyboard.IsPressed(Keys.NumPad5))
                {
                    text.Anchor = Anchor.MiddleCentered;
                    text.TextAnchor = Anchor.MiddleCentered;
                }

                if (Input.Keyboard.IsPressed(Keys.NumPad6))
                {
                    text.Anchor = Anchor.MiddleRight;
                    text.TextAnchor = Anchor.MiddleRight;
                }

                if (Input.Keyboard.IsPressed(Keys.NumPad1))
                {
                    text.Anchor = Anchor.BottomLeft;
                    text.TextAnchor = Anchor.BottomLeft;
                }

                if (Input.Keyboard.IsPressed(Keys.NumPad2))
                {
                    text.Anchor = Anchor.BottomCentered;
                    text.TextAnchor = Anchor.BottomCentered;
                }

                if (Input.Keyboard.IsPressed(Keys.NumPad3))
                {
                    text.Anchor = Anchor.BottomRight;
                    text.TextAnchor = Anchor.BottomRight;
                }

                if (Input.Mouse.WheelDelta() > 0)
                {
                    text.Position = new(text.Position.X + 15, text.Position.Y + 15);
                }
                else if (Input.Mouse.WheelDelta() < 0)
                {
                    text.Position = new(text.Position.X - 15, text.Position.Y - 15);
                }
            }

            velocity = Vector3.Zero;

            if (Input.Keyboard.IsDown(Keys.Left)) velocity.X = -1;
            else if (Input.Keyboard.IsDown(Keys.Right)) velocity.X = 1;

            if (Input.Keyboard.IsDown(Keys.Up)) velocity.Y = -1;
            else if (Input.Keyboard.IsDown(Keys.Down)) velocity.Y = 1;

			if (velocity.Length() > 0)
			{
				velocity.Normalize();
                Move(velocity.X * speed * Time.DeltaTime, velocity.Y * speed * Time.DeltaTime);
            }
		}

        public override void FixedUpdate(GameTime gameTime)
        {
            base.FixedUpdate(gameTime);
		}

        public IEnumerator<TimeSpan> FadeOut()
        {
            Sprite2D sprite = GetComponent<Sprite2D>();

            for (float alpha = 100f; alpha >= 0f; alpha -= 1f)
            {
                if (sprite != null)
                {
                    Position = new(Position.X - alpha * 10f * Time.DeltaTime, Position.Y, Position.Z);
                    yield return Wait.ForMilliseconds(10);
                }
            }

            for (float alpha = 0f; alpha <= 100f; alpha += 1f)
            {
                if (sprite != null)
                {
                    Position = new(Position.X + alpha * 10f * Time.DeltaTime, Position.Y, Position.Z);
                    yield return Wait.ForMilliseconds(10);
                }
            }
        }

		public override void DrawDebug(GameTime gameTime, SpriteBatch spriteBatch)
		{
			base.DrawDebug(gameTime, spriteBatch);

            Debug.FillRectangle(spriteBatch, Collider.Bounds, Color.Yellow * 0.5f);
		}
	}
}
