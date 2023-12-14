using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmostGoodEngine.Core.Resources
{
    public class Atlas2D
    {
        /// <summary>
        /// Atlas texture
        /// </summary>
        public Texture2D Texture { get; set; }

        /// <summary>
        /// Sources sprites inside the atlas texture
        /// </summary>
        public Dictionary<string, Texture2D> Sources { get; set; }

        public Atlas2D(Texture2D texture)
        {
            Texture = texture;
            Sources = new Dictionary<string, Texture2D>();
        }

        /// <summary>
        /// Load an atlas data
        /// </summary>
        /// <param name="filename"></param>
        public void Load(string filename)
        {
        }

        private Texture2D CreateTexture(Rectangle source)
        {
            if (Texture == null)
            {
                return null;
            }

            Texture2D result = new(GameManager.Game.GraphicsDevice, source.Width, source.Height);
            Color[] data = new Color[source.Width * source.Height];
            Texture.GetData(0, source, data, 0, data.Length);
            result.SetData(data);

            return result;
        }
    }
}
