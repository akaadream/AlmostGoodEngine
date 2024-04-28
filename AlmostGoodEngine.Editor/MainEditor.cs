using AlmostGoodEngine.Core;
using AlmostGoodEngine.Core.Scenes;
using AlmostGoodEngine.Core.Utils;
using AlmostGoodEngine.Editor.Components;
using AlmostGoodEngine.Editor.Themes;
using AlmostGoodEngine.Inputs;
using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.ImGuiNet;
using System;
using System.Collections.Generic;
using System.IO;

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
		/// The previous window size of the current scene
		/// </summary>

		private static System.Numerics.Vector2 _previousWindowSize;

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

			BuildComponents();
			ReloadTheme();

			ImGuiIOPtr io = ImGui.GetIO();
			io.ConfigFlags |= ImGuiConfigFlags.DockingEnable;

			ImGui.LoadIniSettingsFromDisk("settings.ini");
		}

		static void AddFonts()
		{
			var io = ImGui.GetIO();
			unsafe
			{
				var config = ImGuiNative.ImFontConfig_ImFontConfig();
				config->MergeMode = 1;
				config->GlyphMinAdvanceX = 14;

				io.Fonts.AddFontDefault(config);
				var ranges = new ushort[] { Fonts.FontAwesomeIconRangeStart, Fonts.FontAwesomeIconRangeEnd, 0 };

				fixed (ushort* rangePtr = ranges)
				{
					static ImFontPtr? AddFont(string fontName, float size, ImFontConfigPtr configPtr, IntPtr r)
					{
						string path = Path.Join("fonts", fontName);
						if (!File.Exists(path))
						{
							Logger.Log($"The file {path} is unreachable!");
							return null;
						}
						Logger.Log($"The file {path} is loaded!");

						return ImGui.GetIO().Fonts.AddFontFromFileTTF(path, size, configPtr, r);
					}

					AddFont("fa-regular-400.otf", 12, config, (IntPtr)rangePtr);
					AddFont("fa-solid-400.otf", 12, config, (IntPtr)rangePtr);
					Fonts.LargeIcons = AddFont("fa-solid-400.otf", 18, IntPtr.Zero, (IntPtr)rangePtr)!.Value;
					Fonts.TitleFont = AddFont("fa-regular-400.otf", 14, IntPtr.Zero, io.Fonts.GetGlyphRangesDefault())!.Value;
				}

				ImGuiNative.ImFontConfig_destroy(config);
			}

			io.Fonts.Build();
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
		/// Load the content
		/// </summary>
		public static void LoadContent()
		{
			GUIRenderer.RebuildFontAtlas();
			AddFonts();
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
		public static void Draw(GameTime gameTime)
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
			System.Numerics.Vector2 windowSize = ImGui.GetWindowSize();
			if (windowSize != _previousWindowSize)
			{
				_previousWindowSize = windowSize;
				GameManager.UpdateRenderTarget((int)windowSize.X, (int)windowSize.Y);
			}

			// Add the game texture
			Texture2D texture = GameManager.ScreenTexture(gameTime);
			if (texture != null)
			{
				nint bindedTexture = GUIRenderer.BindTexture(texture);
				ImGui.Image(bindedTexture, new System.Numerics.Vector2(texture.Width, texture.Height));
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
			ImGui.End();

			ImGui.Begin("Files");
			ImGui.End();

			ImGui.Begin("Properties");
			ImGui.End();

			GUIRenderer.EndLayout();
		}
	}
}
