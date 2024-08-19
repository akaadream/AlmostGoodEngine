using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmostGoodEngine.Editor
{
    public class ProjectTree
    {
        public ProjectTree(Game game)
        {
            var root = game.Content.RootDirectory;
        }
    }
}
