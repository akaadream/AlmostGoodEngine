using Apos.Shapes;
using System;
using System.Collections.Generic;

namespace AlmostGoodEngine.GUI
{
	public class GUILayout : GUIElement
	{
		public override void Update(float delta)
		{
			base.Update(delta);
			foreach (var child in Children)
			{
				child.Update(delta);
			}
		}

		public override void Draw(ShapeBatch shapeBatch, float delta)
		{
			// Scissor rectangle
			GUIManager.SetScissor(ScissorRectangle);

			//Console.WriteLine(ScissorRectangle.ToString());

			// Start drawing shapes
			shapeBatch.Begin();

			base.Draw(shapeBatch, delta);
			foreach (var child in Children)
			{
				child.Draw(shapeBatch, delta);
			}

			shapeBatch.End();

			// Remove the scissor rectangle
			GUIManager.ClearScissor();
		}
	}
}
