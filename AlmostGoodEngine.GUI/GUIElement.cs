using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmostGoodEngine.GUI
{
	public abstract class GUIElement
	{
		public string Id { get; set; }
		public List<string> Classes { get; set; }

		public virtual void Update(float delta)
		{

		}

		public virtual void Draw(float delta)
		{

		}
	}
}
