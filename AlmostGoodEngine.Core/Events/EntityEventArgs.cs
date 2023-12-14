using AlmostGoodEngine.Core.ECS;
using System;

namespace AlmostGoodEngine.Core.Events
{
    public class EntityEventArgs : EventArgs
    {
        public Entity Entity { get; set; }

        public EntityEventArgs(Entity entity)
        {
            Entity = entity;
        }
    }
}
