using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmostGoodEngine.Core.Resources
{
    public class AtlasSources
    {
        [JsonProperty]
        public Rectangle[] Sources { get; set; }

        public AtlasSources()
        {
            Sources = new Rectangle[0];
        }

        [JsonConstructor]
        public AtlasSources(Rectangle[] sources)
        {
            Sources = sources;
        }
    }
}
