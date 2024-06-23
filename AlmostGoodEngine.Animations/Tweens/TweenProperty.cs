using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AlmostGoodEngine.Animations.Tweens
{
    internal class TweenProperty
    {
        public Type Type { get; set; }
        public PropertyInfo Property { get; set; }
    }
}
