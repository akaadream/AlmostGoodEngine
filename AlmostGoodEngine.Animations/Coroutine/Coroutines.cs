using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace AlmostGoodEngine.Animations.Coroutine
{
    public class Coroutines
    {
        /// <summary>
        /// The list of all the current coroutines
        /// </summary>
        private static readonly List<Tuple<IEnumerator<TimeSpan>, TimeSpan>> _coroutines = new();

        /// <summary>
        /// Update the coroutine system
        /// </summary>
        /// <param name="gameTime"></param>
        public static void Update(GameTime gameTime)
        {
            for (int i = 0; i < _coroutines.Count; i++)
            {
                var coroutine = _coroutines[i];
                coroutine = Tuple.Create(coroutine.Item1, coroutine.Item2 - gameTime.ElapsedGameTime);

                while (coroutine.Item2 <= TimeSpan.Zero)
                {
                    if (!coroutine.Item1.MoveNext())
                    {
                        _coroutines.RemoveAt(i);
                        if (i == _coroutines.Count)
                        {
                            break;
                        }
                        else
                        {
                            coroutine = _coroutines[i];
                        }
                    }
                    else
                    {
                        coroutine = Tuple.Create(coroutine.Item1, coroutine.Item1.Current);
                    }
                }

                if (i < _coroutines.Count)
                {
                    _coroutines[i] = coroutine;
                }
            }
        }

        /// <summary>
        /// Start a new coroutine by giving the coroutine as argument
        /// </summary>
        /// <param name="coroutine"></param>
        public static void StartCoroutine(IEnumerator<TimeSpan> coroutine)
        {
            _coroutines.Add(Tuple.Create(coroutine, TimeSpan.Zero));
        }
    }
}
