using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmostGoodEngine.Core.Interfaces
{
    public interface IGameObject : IGameObjectMethods
    {
        void LoadContent(ContentManager content);
    }
}
