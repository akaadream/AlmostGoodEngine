using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmostGoodEngine.Editor.Components
{
	public class FileSystem : Component
	{
		public FileSystem()
		{

		}

		public override void Draw()
		{
			base.Draw();

			ImGui.TreeNode("");
		}
	}
}
