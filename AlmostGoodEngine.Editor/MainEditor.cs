using AlmostGoodEngine.Editor.Components;
using AlmostGoodEngine.Editor.Themes;
using AlmostGoodEngine.Editor.Utils;
using AlmostGoodEngine.Inputs;
using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace AlmostGoodEngine.Editor
{
	public static class MainEditor
	{
		/// <summary>
		/// The renderer used to render ImGui components
		/// </summary>
		public static ImGuiRenderer GUIRenderer { get; set; }

		/// <summary>
		/// Components of the editor
		/// </summary>
		public static List<Component> Components { get; set; }

		/// <summary>
		/// A dictionary containing all the themes
		/// </summary>
		public static Dictionary<string, Theme> Themes { get; set; }

		/// <summary>
		/// The current theme name
		/// </summary>
		public static string CurrentTheme { get; set; }

		/// <summary>
		/// Force the game to do a custom rendering
		/// </summary>
		public static bool CustomRendering { get; set; }

		/// <summary>
		/// True if the editor is currently used
		/// </summary>
		public static bool CurrentlyUsed { get; set; }

		/// <summary>
		/// The previous window size of the current scene
		/// </summary>

		private static Vector2 _previousWindowSize;

		private static IntPtr _sceneTextureId;

		public static Vector2 GameSize { get; private set; }

		public static Action<Vector2> OnGameSizeChanged { get; set; }

		/// <summary>
		/// Initialize the ImGUI renderer
		/// </summary>
		/// <param name="game"></param>
		public static void Initialize(Game game)
		{
			GUIRenderer = new ImGuiRenderer(game);

			Components = [];
			Themes = [];

			Themes.Add("default", new DefaultTheme());
			Themes.Add("dracula", new DraculaTheme());
			Themes.Add("cherry", new CherryTheme());
			CurrentTheme = "default";

			CustomRendering = true;
			CurrentlyUsed = true;

			BuildComponents();
			ReloadTheme();

			ImGuiIOPtr io = ImGui.GetIO();
			io.ConfigFlags |= ImGuiConfigFlags.DockingEnable;

			ImGui.LoadIniSettingsFromDisk("settings.ini");

			_sceneTextureId = GUIRenderer.GetNextIntPtr();
		}

		public static void Dispose()
		{
			GUIRenderer?.Dispose();
		}

		private static void BuildComponents()
		{
			// Nav bar
			Components.Add(new NavBar());

			// Scene hierarchy window
			var sceneWindow = new Window("Scene hierarchy");
			sceneWindow.Children.Add(new SceneTree());
			Components.Add(sceneWindow);
		}

		private static void ReloadTheme()
		{
			if (Themes.TryGetValue(CurrentTheme, out var theme))
			{
				theme.Apply();
			}
		}

		public static void ApplyTheme(string themeName)
		{
			if (Themes.TryGetValue(themeName, out var theme))
			{
				CurrentTheme = themeName;
				theme.Apply();
			}
		}

		/// <summary>
		/// Update
		/// </summary>
		/// <param name="gameTime"></param>
		public static void Update(GameTime gameTime)
		{
			if (Input.Keyboard.IsPressed(Microsoft.Xna.Framework.Input.Keys.G))
			{
				ImGui.SaveIniSettingsToDisk("settings.ini");
			}
		}

		/// <summary>
		/// Draw
		/// </summary>
		/// <param name="gameTime"></param>
		public static void Draw(GameTime gameTime, Texture2D frameTexture)
		{
			GUIRenderer.BeginLayout(gameTime);

			ImGuiViewportPtr viewport = ImGui.GetMainViewport();
			ImGui.SetNextWindowPos(viewport.WorkPos);
			ImGui.SetNextWindowSize(viewport.WorkSize);
			ImGui.SetNextWindowViewport(viewport.ID);

			bool opened = true;
			ImGui.Begin("Main Dockspace", ref opened, ImGuiWindowFlags.NoDocking | ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoCollapse |
				ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoBringToFrontOnFocus | ImGuiWindowFlags.NoNavFocus |
				ImGuiWindowFlags.NoBackground);

			ImGui.PopStyleVar(2);
			ImGui.DockSpace(ImGui.GetID("Main Dockspace"), new System.Numerics.Vector2(0, 0), ImGuiDockNodeFlags.PassthruCentralNode);
			ImGui.End();

			foreach (var component in Components)
			{
				component.Draw();
			}

			ImGuiWindowFlags windowFlags = ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse;
			ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, 0);
			ImGui.Begin("Scene 1", windowFlags);

			// Window size
			GameSize = ImGui.GetWindowSize();
			if (GameSize != _previousWindowSize)
			{
				_previousWindowSize = GameSize;

				OnGameSizeChanged?.Invoke(GameSize);
			}

			// Add the game texture
			if (frameTexture != null)
			{
				nint bindedTexture = GUIRenderer.BindTexture(_sceneTextureId, frameTexture, false);
				ImGui.Image(bindedTexture, new System.Numerics.Vector2(frameTexture.Width, frameTexture.Height));
			}
			ImGui.PopStyleVar(1);
			
			//ImGuiTabBarFlags tabBarFlags = ImGuiTabBarFlags.Reorderable | ImGuiTabBarFlags.FittingPolicyResizeDown;
			//if (ImGui.BeginTabBar("Scenes", tabBarFlags))
			//{
			//	if (ImGui.BeginTabItem("Scene 1"))
			//	{
			//		ImGui.Text("Tab 1 item");
			//		ImGui.EndTabItem();
			//	}

			//	if (ImGui.BeginTabItem("Scene 2"))
			//	{
			//		ImGui.Text("Tab 2 item");
			//		ImGui.EndTabItem();
			//	}

			//	if (ImGui.BeginTabItem("Scene 3"))
			//	{
			//		ImGui.Text("Tab 3 item");
			//		ImGui.EndTabItem();
			//	}

			//	ImGui.EndTabBar();
			//}
			ImGui.End();

			ImGui.Begin("Scene 2");
			//imnodes.BeginNodeEditor();
			//const int hardCodedNodeId = 1;
			//imnodes.BeginNode(hardCodedNodeId);
			//ImGui.Dummy(new System.Numerics.Vector2(80f, 45f));
			//imnodes.BeginOutputAttribute(hardCodedNodeId);
			//ImGui.Text("output");
			//imnodes.EndNode();
			//imnodes.EndNodeEditor();
			ImGui.End();

			ImGui.Begin("Files");
			ImGui.End();

			ImGui.Begin("Properties");
			ImGui.End();

			GUIRenderer.EndLayout();
		}
	}
}
