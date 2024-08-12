using System.Collections.Generic;

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
