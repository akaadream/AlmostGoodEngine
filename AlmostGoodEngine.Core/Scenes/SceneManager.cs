using AlmostGoodEngine.Core.Interfaces;
using AlmostGoodEngine.Core.Scenes.Transitions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace AlmostGoodEngine.Core.Scenes
{
    public class SceneManager() : IGameObjectMethods
    {
        public Dictionary<string, Scene> Scenes { get; private set; } = [];

        public Dictionary<string, ITransition> Transitions { get; private set; } = [];

        public Scene CurrentScene { get; private set; }

        public string LoadAtStart { get; internal set; }

        public bool LoadEverythingOnStartup { get; set; } = true;
        public bool EngineStarted { get; internal set; }
        
        public bool DoingTransition { get; internal set; }

        private bool _startLoaded = false;
        private ITransition _transition = null;

        /// <summary>
        /// Add the given scene if the key is not already present and put the scene as the current scene if 
        /// the property setAsCurrent is set to true (default value)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="scene"></param>
        public void Add(string name, Scene scene, bool setAsCurrent = false)
        {
			// The scene is already contained inside the scenes dictionary
			if (Scenes.TryGetValue(name, out _))
            {
                return;
            }

            scene.Name = name;

            // Add the scene inside the scenes collection
            Scenes.Add(name, scene);

            // If the scene may be defined as the current scene
            if (setAsCurrent)
            {
                Set(scene, "", 0f);
            }
        }

        /// <summary>
        /// Select a new current scene
        /// </summary>
        /// <param name="name"></param>
        public void Set(string name, string transition = "", float duration = 0.5f)
		{
			// Scene does not exists
			if (Scenes.TryGetValue(name, out Scene value))
			{
				Set(value, transition, duration);
			}
		}

		/// <summary>
		/// Select a new current scene from a scene instance
		/// </summary>
		/// <param name="scene"></param>
		private void Set(Scene scene, string transition, float duration)
        {
            if (scene == null)
            {
                return;
            }

            if (EngineStarted)
            {
				CurrentScene?.End();
			}

            // Update the current scene
            var previousScene = CurrentScene;
            CurrentScene = scene;

            if (!CurrentScene.ContentLoaded)
            {
                CurrentScene.LoadContent(GameManager.Engine.Content);
            }

            if (EngineStarted)
            {
				CurrentScene.Start();
			}

            if (duration > 0f && Transitions.TryGetValue(transition, out var value))
            {
                var previousSceneFrame = previousScene.GetFrame(new GameTime());
                var nextSceneFrame = CurrentScene.GetFrame(new GameTime());
                _transition = value;
                _transition.Start(previousSceneFrame, nextSceneFrame, duration);
            }
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

            // Load all the transitions
            RegisterTransition("fade", new FadeTransition());

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

        public void Resize(Viewport viewport)
        {
            foreach (var scene in Scenes.Values)
            {
                scene.Resize(viewport);
            }
        }

        /// <summary>
        /// Register a new transition
        /// </summary>
        /// <param name="name"></param>
        /// <param name="transition"></param>
        /// <returns></returns>
        public bool RegisterTransition(string name, Transition transition)
        {
            if (Transitions.TryGetValue(name, out var _))
            {
                return false;
            }

            Transitions.Add(name, transition);
            return true;
        }

        /// <summary>
        /// Before the update of the scene
        /// </summary>
        /// <param name="gameTime"></param>
        public void BeforeUpdate(GameTime gameTime)
        {
            if (!_startLoaded)
            {
                if (Scenes.TryGetValue(LoadAtStart, out Scene scene))
                {
                    Set(scene, "", 0f);
                    _startLoaded = true;
                    return;
                }
            }

            if (DoingTransition)
            {
                if (_transition == null)
                {
                    DoingTransition = true;
                }
                else
                {
                    DoingTransition = !_transition.Update();
                    return;
                }
            }

            CurrentScene?.BeforeUpdate(gameTime);
        }

        /// <summary>
        /// Update the scene
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            if (DoingTransition)
            {
                return;
            }

            CurrentScene?.Update(gameTime);
        }

        public void FixedUpdate(GameTime gameTime)
        {
            if (DoingTransition)
            {
                return;
            }

            CurrentScene?.FixedUpdate(gameTime);
        }

        /// <summary>
        /// After the update of the scene
        /// </summary>
        /// <param name="gameTime"></param>
        public void AfterUpdate(GameTime gameTime)
        {
            if (DoingTransition)
            {
                return;
            }

            CurrentScene?.AfterUpdate(gameTime);
        }

        /// <summary>
        /// Draw the scene
        /// </summary>
        /// <param name="gameTime"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (DoingTransition)
            {
                GameManager.SpriteBatch.Draw(_transition.GetFrame(), Vector2.Zero, Color.White);
                return;
            }
            CurrentScene?.Draw(gameTime, spriteBatch);
        }

        /// <summary>
        /// Draw the scene's UI
        /// </summary>
        /// <param name="gameTime"></param>
        public void DrawUI(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (DoingTransition)
            {
                return;
            }
            CurrentScene?.DrawUI(gameTime, spriteBatch);
        }

        /// <summary>
        /// Draw the debug UI
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void DrawDebug(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (DoingTransition)
            {
                return;
            }
            CurrentScene?.DrawDebug(gameTime, spriteBatch);
        }
    }
}
