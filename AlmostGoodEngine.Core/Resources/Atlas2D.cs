using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using AlmostGoodEngine.Serialization;
using System.Threading.Tasks;

namespace AlmostGoodEngine.Core.Resources
{
    /// <summary>
    /// 2D atlas that represent all the sub textures of a big texture
    /// TODO: create the atlas editor within the AlmostGoodEngine.Editor
    /// </summary>
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
            Sources = [];
        }

        /// <summary>
        /// Load an atlas data
        /// </summary>
        /// <param name="filename"></param>
        public static Atlas2D Load(string filename)
        {
			var atlasData = SaveLoad.Load<AtlasSources>(filename, SaveLoadType.Json).GetAwaiter().GetResult();
            if (atlasData != null)
            {
                // Create a new atlas
				var atlas = new Atlas2D(GameManager.Engine.Content.Load<Texture2D>(atlasData.Filename));

				foreach (var source in atlasData.Sources)
                {
                    // We can't duplicate a source
                    if (atlas.Sources.ContainsKey(source.Key))
                    {
                        continue;
                    }

                    // We can't add an empty source
                    if (source.Value != Rectangle.Empty)
                    {
                        continue;
                    }

                    // Store the texture so we can check there is something else than a null value
                    var texture = atlas.CreateTexture(source.Value);
                    if (texture == null)
                    {
                        continue;
                    }

                    // Add the source inside the dictionary
					atlas.Sources.Add(source.Key, texture);
                }

                return atlas;
            }

			return null;
        }

        /// <summary>
        /// The the atlas into a data object
        /// </summary>
        /// <param name="filename"></param>
        public async Task Save(string filename)
        {
            var data = new AtlasSources();
            foreach (var source in Sources)
            {
                data.Sources.Add(source.Key, Retrieve(source.Value));
            }
            await SaveLoad.Save(data, filename, SaveLoadType.Json);
        }

        /// <summary>
        /// Create a texture 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private Texture2D CreateTexture(Rectangle source)
        {
            if (Texture == null)
            {
                return null;
            }

            Texture2D result = new(GameManager.Engine.GraphicsDevice, source.Width, source.Height);
            Color[] data = new Color[source.Width * source.Height];
            Texture.GetData(0, source, data, 0, data.Length);
            result.SetData(data);
            result.Name = "" + source.X + "," + source.Y;

            return result;
        }
        
        /// <summary>
        /// Retrieve the position of the given texture inside the atlas texture
        /// </summary>
        /// <param name="texture"></param>
        /// <returns></returns>
        private Rectangle Retrieve(Texture2D texture)
        {
            string[] position = texture.Name.Split(',');
            if (position.Length == 2 )
            {
                return new Rectangle(int.Parse(position[0]), int.Parse(position[1]), texture.Width, texture.Height);
            }

            return Rectangle.Empty;
        }
    }
}
