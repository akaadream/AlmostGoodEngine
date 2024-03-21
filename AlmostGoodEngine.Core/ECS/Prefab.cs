using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmostGoodEngine.Core.ECS
{
    public class Prefab
    {
        public List<Component> Components { get; private set; }
        public List<Entity> Children { get; private set; }

        public Prefab()
        {
            Components = [];
        }
    }
}
