using AlmostGoodEngine.Editor.Utils;
using ImGuiNET;
using System.Collections.Generic;
using System.Numerics;

namespace AlmostGoodEngine.Editor.Components
{
	public class NavBar : Component
	{
		public List<MenuComponent> Menus { get; set; }
		public List<MenuComponent> MiddleMenus { get; set; }
		public List<MenuComponent> RightMenus { get; set; }

		private float _menusX = 0f;
		private float _middleMenuX = 0f;
		private float _rightMenuX = 0f;

		public NavBar()
		{
			Menus = [];
			MiddleMenus = [];
			RightMenus = [];

			CreateMenu();

			// Compute list sizes
			//_menusX = ImGuiHelper.GetSizeOf(Menus).X;
			//_middleMenuX = ImGuiHelper.GetSizeOf(MiddleMenus).X;
			//_middleMenuX = ImGuiHelper.GetSizeOf(RightMenus).X;
		}

		/// <summary>
		/// Hydrate the lists with menu items
		/// </summary>
		private void CreateMenu()
		{
			Menu project = new("Project", "\uf15b");
			Menu scene = new("Scene", "\uf03d");
			Menu editor = new("Editor", "\ue131");
			Menu edit = new("Edit", "\uf044");
			Menu tools = new("Tools", "\uf0ad");
			Menu run = new("Run", "\uf04b");
			Menu layout = new("Layout", "\uf2d2");
			Menu help = new("Help", "\uf059");

			project.Children.Add(new MenuItem("New project"));
			project.Children.Add(new MenuItem("Open project"));
			project.Children.Add(new MenuItem("Close project", "Ctrl+Q", "", false));
			project.Children.Add(new MenuSeparator());
			project.Children.Add(new MenuItem("Quit Almost Good Editor", "Alt+F4"));

			scene.Children.Add(new MenuItem("New scene", "Ctrl+N"));
			scene.Children.Add(new MenuItem("Open existing scene", "Ctrl+O"));
			scene.Children.Add(new MenuItem("Open last closed scene", "Ctrl+Maj+T"));
			scene.Children.Add(new MenuSeparator());
			scene.Children.Add(new MenuItem("Save the scene", "Ctrl+S"));
			scene.Children.Add(new MenuItem("Save the scene to", "Ctrl+Maj+S"));
			scene.Children.Add(new MenuSeparator());
			scene.Children.Add(new MenuItem("Close current scene", "Ctrl+W"));

			editor.Children.Add(new MenuItem("Editor settings", "", "\uf085"));
			editor.Children.Add(new MenuItem("Command palette", "Ctrl+Maj+P", "\uf120"));
			editor.Children.Add(new MenuSeparator());
			editor.Children.Add(new MenuItem("Take a screenshot", "", "\uf030"));

			edit.Children.Add(new MenuItem("Go to...", "", "\uf090"));
			edit.Children.Add(new MenuItem("Find and replace", "\uf002"));
			edit.Children.Add(new MenuSeparator());
			edit.Children.Add(new MenuItem("Cut", "Ctrl+X", "\uf0c4"));
			edit.Children.Add(new MenuItem("Copy", "Ctrl+C", "\uf0c5"));
			edit.Children.Add(new MenuItem("Paste", "Ctrl+V", "\uf0ea"));
			edit.Children.Add(new MenuItem("Duplicate", "Ctrl+D", "\uf24d"));
			edit.Children.Add(new MenuItem("Delete", "Del", "\uf1f8"));
			edit.Children.Add(new MenuSeparator());
			edit.Children.Add(new MenuItem("Select all", "Ctrl+A"));

			var themesMenu = new Menu("Themes", "\uf53f");
			themesMenu.Children.Add(new MenuItem("Default", "", "", true, () =>
			{
				MainEditor.ApplyTheme("default");
			}));
			themesMenu.Children.Add(new MenuItem("Dracula", "", "", true, () =>
			{
				MainEditor.ApplyTheme("dracula");
			}));
			themesMenu.Children.Add(new MenuItem("Cherry", "", "", true, () =>
			{
				MainEditor.ApplyTheme("cherry");
			}));
			tools.Children.Add(themesMenu);
			tools.Children.Add(new MenuItem("Command line", "", "\uf120"));
			tools.Children.Add(new MenuItem("Import / Export settings", "", "\uf574"));
			tools.Children.Add(new MenuItem("Customize...", "", "\uf1fc"));
			tools.Children.Add(new MenuItem("Settings", "", "\uf013"));

			run.Children.Add(new MenuItem("Run the game", "F5", "\uf144"));
			run.Children.Add(new MenuSeparator());
			run.Children.Add(new MenuItem("Full screen", "", "\uf065", true, false, true));

			layout.Children.Add(new MenuItem("Reset main layout"));
			layout.Children.Add(new MenuSeparator());
			layout.Children.Add(new MenuItem("Default layout", "", "", true, true, true));
			layout.Children.Add(new MenuItem("Reversed layout", "", "", true, false, true));

			help.Children.Add(new MenuItem("Wiki", "", "\uf02d"));
			help.Children.Add(new MenuSeparator());
			help.Children.Add(new MenuItem("About Almost Good Editor", "", "\uf05a"));

			Menus.Add(project);
			Menus.Add(scene);
			Menus.Add(editor);
			Menus.Add(edit);
			Menus.Add(tools);
			Menus.Add(run);
			Menus.Add(layout);
			Menus.Add(help);
		}

		public override Vector2 Draw()
		{
			base.Draw();

			float availableX = ImGui.GetContentRegionAvail().X;
			_menusX = 0;
			_middleMenuX = 0;
			_rightMenuX = 0;

			ImGui.PushStyleVar(ImGuiStyleVar.FramePadding, new Vector2(8f, 8f));
			if (ImGui.BeginMainMenuBar())
			{
				ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, new Vector2(8.0f, 8.0f));
				foreach (var menu in Menus)
				{
					menu.Draw();
				}

				if (availableX >= _menusX + _middleMenuX)
				{
					foreach (var menu in MiddleMenus)
					{
						menu.Draw();
					}
				}

				if (availableX >= _menusX + _middleMenuX + _rightMenuX)
				{
					foreach (var menu in RightMenus)
					{
						menu.Draw();
					}
				}

				ImGui.EndMainMenuBar();
				ImGui.PopStyleVar(1);
			}
			ImGui.PopStyleVar(1);

			return Vector2.Zero;
		}
	}
}
