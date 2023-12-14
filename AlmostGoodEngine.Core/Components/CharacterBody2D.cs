using AlmostGoodEngine.Core.ECS;
using AlmostGoodEngine.Inputs;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmostGoodEngine.Core.Components
{
    public class CharacterBody2D : Component
    {
        public InputBinds LeftBind { get; set; }
        public InputBinds RightBind { get; set; }
        public InputBinds UpBind { get; set; }
        public InputBinds DownBind { get; set; }

        public CharacterBody2D()
        {

        }
    }
}
