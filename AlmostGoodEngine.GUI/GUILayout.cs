using Apos.Shapes;
using Microsoft.Xna.Framework.Graphics;
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

		public override void Draw(ShapeBatch shapeBatch, SpriteBatch spriteBatch, float delta)
		{
			// Scissor rectangle
			GUIManager.SetScissor(ScissorRectangle);

			//Console.WriteLine(ScissorRectangle.ToString());
			spriteBatch.Begin();

			// Start drawing shapes
			shapeBatch.Begin();

			base.Draw(shapeBatch, spriteBatch, delta);
			foreach (var child in Children)
			{
				child.Draw(shapeBatch, spriteBatch, delta);
			}

			shapeBatch.End();

			spriteBatch.End();

			// Remove the scissor rectangle
			GUIManager.ClearScissor();
		}
	}
}
