using ImGuiNET;
using System.Collections.Generic;
using System.Numerics;

namespace AlmostGoodEngine.Editor.Components
{
	public class SceneTree : Component
	{
		public List<string> Elements { get; set; }
		public string SelectedxSceneElement { get; set; }

		public SceneTree()
		{
			Elements = [];
		}

		public override Vector2 Draw()
		{
			base.Draw();

			return Vector2.Zero;

			//Scene scene = GameManager.CurrentScene();
			//if (scene != null)
			//{
			//	if (ImGui.TreeNode(scene.Name))
			//	{
			//		ImGui.PushStyleColor(ImGuiCol.Text, new Vector4(0.45f, 0.64f, 0.97f, 1f));
			//		foreach (var entity in scene.GetEntities())
			//		{
			//			ImGuiTreeNodeFlags nodeFlags =
			//					ImGuiTreeNodeFlags.OpenOnArrow |
			//					ImGuiTreeNodeFlags.OpenOnDoubleClick;
			//			if (SelectedxSceneElement == entity.Name)
			//			{
			//				nodeFlags |= ImGuiTreeNodeFlags.Selected;
			//			}

			//			if (entity.GetChildrenCount() > 0)
			//			{
			//				if (ImGui.TreeNode("\uf007 " + entity.Name))
			//				{
			//					if (ImGui.IsItemClicked() && !ImGui.IsItemToggledOpen())
			//					{
			//						SelectedxSceneElement = entity.Name;
			//					}

			//					DrawChildren(entity);

			//					ImGui.TreePop();
			//				}
			//			}
			//			else
			//			{
			//				nodeFlags |= ImGuiTreeNodeFlags.Leaf;
			//				if (ImGui.TreeNodeEx("\uf007 " + entity.Name, nodeFlags))
			//				{
			//					if (ImGui.IsItemClicked())
			//					{
			//						SelectedxSceneElement = entity.Name;
			//					}
			//					ImGui.TreePop();
			//				}
			//			}
			//		}
			//		ImGui.PopStyleColor(1);
			//	}
				
			//	ImGui.TreePop();
			//}

			//return Vector2.Zero;
		}

		//public void DrawChildren(Entity entity)
		//{
		//	foreach (var child in entity.GetChildren())
		//	{
		//		ImGuiTreeNodeFlags nodeFlags =
		//						ImGuiTreeNodeFlags.OpenOnArrow |
		//						ImGuiTreeNodeFlags.OpenOnDoubleClick |
		//						ImGuiTreeNodeFlags.Leaf;
		//		if (SelectedxSceneElement == child.Name)
		//		{
		//			nodeFlags |= ImGuiTreeNodeFlags.Selected;
		//		}
		//		if (ImGui.TreeNodeEx("\uf007 " + child.Name, nodeFlags))
		//		{
		//			if (ImGui.IsItemClicked())
		//			{
		//				SelectedxSceneElement = child.Name;
		//			}
		//			ImGui.TreePop();
		//		}
		//	}
		//}
	}
}
