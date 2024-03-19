using Apos.Shapes;
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

		public T GetElement<T>() where T : GUIElement
		{
			foreach (var child in Children)
			{
				if (child.GetType() == typeof(T))
				{
					return (T)child;
				}
			}

			return default;
		}

		public List<T> GetElements<T>() where T : GUIElement
		{
			List<T> elements = [];

			foreach (var child in Children)
			{
				if (child.GetType() == typeof(T))
				{
					elements.Add((T)child);
				}
			}

			return elements;
		}

		public GUIElement GetElementById(string id)
		{
			foreach (var child in Children)
			{
				if (child.Id == id)
				{
					return child;
				}
			}

			return null;
		}

		public GUIElement GetElementByClass(string className)
		{
			foreach (var child in Children)
			{
				if (child.Classes.Contains(className))
				{
					return child;
				}
			}

			return null;
		}

		public List<GUIElement> GetElementsByClass(string className)
		{
			List<GUIElement> elements = [];

			foreach (var child in Children)
			{
				if (child.Classes.Contains(className))
				{
					elements.Add(child);
				}
			}

			return elements;
		}
	}
}
