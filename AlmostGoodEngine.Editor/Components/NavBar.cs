using ImGuiNET;
using System.Collections.Generic;

namespace AlmostGoodEngine.Editor.Components
{
	public class NavBar : Component
	{
		public List<Menu> Menus { get; set; }

		public NavBar()
		{
			Menus = [];

			Menu project = new("Project");
			Menu scene = new("Scene");
			Menu editor = new("Editor");
			Menu edit = new("Edit");
			Menu tools = new("Tools");
			Menu run = new("Run");
			Menu layout = new("Layout");
			Menu help = new("Help");

			project.Children.Add(new MenuItem("New project"));
			project.Children.Add(new MenuItem("Open project"));
			project.Children.Add(new MenuItem("Close project", "Ctrl+Q", false));
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

			editor.Children.Add(new MenuItem("Editor settings"));
			editor.Children.Add(new MenuItem("Command palette", "Ctrl+Maj+P"));
			editor.Children.Add(new MenuSeparator());
			editor.Children.Add(new MenuItem("Take a screenshot"));

			edit.Children.Add(new MenuItem("Go to..."));
			edit.Children.Add(new MenuItem("Find and replace"));
			edit.Children.Add(new MenuSeparator());
			edit.Children.Add(new MenuItem("Cut", "Ctrl+X"));
			edit.Children.Add(new MenuItem("Copy", "Ctrl+C"));
			edit.Children.Add(new MenuItem("Paste", "Ctrl+V"));
			edit.Children.Add(new MenuItem("Duplicate", "Ctrl+D"));
			edit.Children.Add(new MenuItem("Delete", "Del"));
			edit.Children.Add(new MenuSeparator());
			edit.Children.Add(new MenuItem("Select all", "Ctrl+A"));

			var themesMenu = new Menu("Themes");
			themesMenu.Children.Add(new MenuItem("Default", "", true, () =>
			{
				MainEditor.ApplyTheme("default");
			}));
			themesMenu.Children.Add(new MenuItem("Dracula", "", true, () =>
			{
				MainEditor.ApplyTheme("dracula");
			}));
			themesMenu.Children.Add(new MenuItem("Cherry", "", true, () =>
			{
				MainEditor.ApplyTheme("cherry");
			}));
			tools.Children.Add(themesMenu);
			tools.Children.Add(new MenuItem("Command line"));
			tools.Children.Add(new MenuItem("Import / Export settings"));
			tools.Children.Add(new MenuItem("Customize..."));
			tools.Children.Add(new MenuItem("Settings"));

			run.Children.Add(new MenuItem("Run the game", "F5"));
			run.Children.Add(new MenuSeparator());
			run.Children.Add(new MenuItem("Full screen", "", true, false, true));

			layout.Children.Add(new MenuItem("Reset main layout"));
			layout.Children.Add(new MenuSeparator());
			layout.Children.Add(new MenuItem("Default layout", "", true, true, true));
			layout.Children.Add(new MenuItem("Reversed layout", "", true, false, true));

			help.Children.Add(new MenuItem("Wiki"));
			help.Children.Add(new MenuSeparator());
			help.Children.Add(new MenuItem("About Almost Good Editor"));

			Menus.Add(project);
			Menus.Add(scene);
			Menus.Add(editor);
			Menus.Add(edit);
			Menus.Add(tools);
			Menus.Add(run);
			Menus.Add(layout);
			Menus.Add(help);
		}



		public override void Draw()
		{
			base.Draw();

			if (ImGui.BeginMainMenuBar())
			{
				foreach (var menu in Menus)
				{
					menu.Draw();
				}
				ImGui.EndMainMenuBar();
			}
		}
	}
}
