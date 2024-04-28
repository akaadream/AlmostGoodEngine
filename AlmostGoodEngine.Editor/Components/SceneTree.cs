using AlmostGoodEngine.Core;
using AlmostGoodEngine.Core.Scenes;
using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AlmostGoodEngine.Editor.Components
{
	public class SceneTree : Component
	{
		public List<string> Elements { get; set; }

		public SceneTree()
		{
			Elements = [];
		}

		public override void Draw()
		{
			base.Draw();

			Scene scene = GameManager.CurrentScene();
			if (scene != null)
			{
				if (ImGui.TreeNode(scene.Name))
				{
					foreach (var entity in scene.GetEntities())
					{
						if (ImGui.TreeNodeEx("#\uf07b " + entity.Name))
						{
							
						}
					}
				}
				
				ImGui.TreePop();
			}
		}
	}
}
