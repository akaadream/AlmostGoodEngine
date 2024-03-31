using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AlmostGoodEngine.Core.Resources
{
    public class AtlasSources
    {
        /// <summary>
        /// The dictionary of the atlas sources rectangles
        /// </summary>
        [JsonProperty]
        public Dictionary<string, Rectangle> Sources { get; set; }

        /// <summary>
        /// The filename of the atlas texture
        /// </summary>
        [JsonProperty]
        public string Filename { get; set; }

        public AtlasSources()
        {
            Sources = [];
        }

        [JsonConstructor]
        public AtlasSources(Dictionary<string, Rectangle> sources)
        {
            Sources = sources;
        }
    }
}
