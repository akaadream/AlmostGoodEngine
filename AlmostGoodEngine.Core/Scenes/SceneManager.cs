using AlmostGoodEngine.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;

namespace AlmostGoodEngine.Core.Scenes
{
    public class SceneManager : IGameObjectMethods
    {
        public Dictionary<string, Scene> Scenes { get; private set; }
        public Scene CurrentScene { get; private set; }

        public bool LoadEverythingOnStartup { get; set; }

        public SceneManager()
        {
            Scenes = new();
            LoadEverythingOnStartup = true;
        }

        /// <summary>
        /// Add the given scene if the key is not already present and put the scene as the current scene if 
        /// the property setAsCurrent is set to true (default value)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="scene"></param>
        public void Add(string name, Scene scene, bool setAsCurrent = true)
        {
            // The scene is already contained inside the scenes dictionary
            if (Scenes.ContainsKey(name))
            {
                return;
            }

            Scenes.Add(name, scene);
            if (setAsCurrent)
            {
                Set(scene);
            }
        }

        /// <summary>
        /// Select a new current scene
        /// </summary>
        /// <param name="name"></param>
        public void Set(string name)
        {
            // Scene does not exists
            if (!Scenes.ContainsKey(name))
            {
                return;
            }

            Set(Scenes[name]);
        }

        /// <summary>
        /// Select a new current scene from a scene instance
        /// </summary>
        /// <param name="scene"></param>
        private void Set(Scene scene)
        {
            if (scene == null)
            {
                return;
            }

            if (CurrentScene != null)
            {
                CurrentScene.End();
            }

            CurrentScene = scene;

            if (!CurrentScene.ContentLoaded)
            {
                CurrentScene.LoadContent(GameManager.Game.Content);
            }
            CurrentScene.Start();
        }

        /// <summary>
        /// Load scene content
        /// </summary>
        public void LoadContent(ContentManager content)
        {
            // We don't want the game to load all scenes content at the same time
            if (!LoadEverythingOnStartup)
            {
                return;
            }

            // Load scenes content
            foreach (var scene in Scenes.Values)
            {
                scene.LoadContent(content);
            }
        }

        public void Start()
        {
            CurrentScene?.Start();
        }

        public void End()
        {
            CurrentScene?.End();
        }

        /// <summary>
        /// Before the update of the scene
        /// </summary>
        /// <param name="gameTime"></param>
        public void BeforeUpdate(GameTime gameTime)
        {

        }

        /// <summary>
        /// Update the scene
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            CurrentScene?.Update(gameTime);
        }

        public void FixedUpdate(GameTime gameTime)
        {
            CurrentScene?.FixedUpdate(gameTime);
        }

        /// <summary>
        /// After the update of the scene
        /// </summary>
        /// <param name="gameTime"></param>
        public void AfterUpdate(GameTime gameTime)
        {

        }

        /// <summary>
        /// Draw the scene
        /// </summary>
        /// <param name="gameTime"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            CurrentScene?.Draw(gameTime, spriteBatch);
        }

        /// <summary>
        /// Draw the scene's UI
        /// </summary>
        /// <param name="gameTime"></param>
        public void DrawUI(GameTime gameTime, SpriteBatch spriteBatch)
        {
            CurrentScene?.DrawUI(gameTime, spriteBatch);
        }
    }
}
