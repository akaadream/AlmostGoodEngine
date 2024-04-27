using AlmostGoodEngine.Editor.Components;
using ImGuiNET;
using Microsoft.Xna.Framework;
using MonoGame.ImGuiNet;
using System;
using System.Collections.Generic;

namespace AlmostGoodEngine.Editor
{
	public static class Editor
	{
		public static ImGuiRenderer GUIRenderer { get; set; }

		public static List<Component> Components { get; set; }

		/// <summary>
		/// Initialize the ImGUI renderer
		/// </summary>
		/// <param name="game"></param>
		public static void Initialize(Game game)
		{
			GUIRenderer = new ImGuiRenderer(game);
			Components = [];

			Components.Add(new NavBar());
		}

		public static void LoadContent()
		{
			GUIRenderer.RebuildFontAtlas();
		}

		public static void Draw(GameTime gameTime)
		{
			GUIRenderer.BeginLayout(gameTime);

			ImGuiIOPtr io = ImGui.GetIO();
			io.ConfigFlags |= ImGuiConfigFlags.DockingEnable;

			ImGuiViewportPtr viewport = ImGui.GetMainViewport();
			ImGui.SetNextWindowPos(viewport.WorkPos);
			ImGui.SetNextWindowSize(viewport.WorkSize);
			ImGui.SetNextWindowViewport(viewport.ID);

			ImGui.PushStyleVar(ImGuiStyleVar.WindowRounding, 4f);
			ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new System.Numerics.Vector2(0, 0));

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

			ImGuiTabBarFlags tabBarFlags = ImGuiTabBarFlags.Reorderable | ImGuiTabBarFlags.FittingPolicyResizeDown;
			if (ImGui.BeginTabBar("TabBar", tabBarFlags))
			{
				if (ImGui.BeginTabItem("Item1"))
				{
					ImGui.Text("Tab 1 item");
					ImGui.EndTabItem();
				}

				if (ImGui.BeginTabItem("Item2"))
				{
					ImGui.Text("Tab 2 item");
					ImGui.EndTabItem();
				}

				if (ImGui.BeginTabItem("Item3"))
				{
					ImGui.Text("Tab 3 item");
					ImGui.EndTabItem();
				}

				ImGui.EndTabBar();
			}

			GUIRenderer.EndLayout();
		}
	}
}
