using AlmostGoodEngine.Core;
using AlmostGoodEngine.Core.Components;
using AlmostGoodEngine.Core.Components.Camera;
using AlmostGoodEngine.Core.Components.Curves;
using AlmostGoodEngine.Core.Components.Rendering;
using AlmostGoodEngine.Core.ECS;
using AlmostGoodEngine.Core.Scenes;
using AlmostGoodEngine.Core.Utils;
using AlmostGoodEngine.GUI;
using AlmostGoodEngine.Tests.GameObjects;
using Microsoft.Xna.Framework;

namespace AlmostGoodEngine.Tests.Scenes
{
    internal class TestScene : Scene
    {
        readonly Player player;

        public TestScene() : base()
        {
            Entity entity = new();
            entity.AddComponent(new DebugObject());

            Entity pathEntity = new();
            entity.AddComponent(new DrawPath());

            GameObjects.Add(entity);
            GameObjects.Add(pathEntity);

            player = new();
            GameObjects.Add(player);

            Entity hitBoxEntity = new();
            Hitbox hitbox = new(new(300, 250, 100, 100))
            {
                DisplayDebug = true,
            };
            hitbox.OnEntityEnter += Hitbox_OnEntityEnter;
            hitbox.OnEntityExit += Hitbox_OnEntityExit;

            hitBoxEntity.AddComponent(hitbox);
            GameObjects.Add(hitBoxEntity);

            Renderer.Camera.AddComponent(new FollowTarget(Renderer.Camera, player));
            Renderer.Camera.AddComponent(new CameraLimit(new(-470, -320, 1), new(470, 320, 1)));

            GUILayout menu = new();
            menu.Style.FullWidth = true;
            menu.Style.Height = 42;
            menu.Style.BackgroundColor = new Color(35, 35, 35);

            GUIElement window = new();
            window.Style.Width = 400;
            window.Style.Height = 280;
            window.Style.BorderRadius = 20;
            window.Style.BackgroundColor = new Color(185, 35, 35);
            window.Style.Top = 10;
            window.Style.Left = 115;

            menu.Children.Add(window);
            GUIManager.Layouts.Add(menu);
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
    }
}
