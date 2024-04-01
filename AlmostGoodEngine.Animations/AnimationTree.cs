using System.Collections.Generic;

namespace AlmostGoodEngine.Animations
{
	public class AnimationTree
	{
		/// <summary>
		/// Dictionary of all the animations
		/// </summary>
		public Dictionary<string, Animation> Animations { get; set; }

		/// <summary>
		/// The name of the animation curretly played
		/// </summary>
		public string CurrentAnimationName { get; private set; }

		public AnimationTree() { }

		/// <summary>
		/// Play the given animation
		/// </summary>
		/// <param name="animation"></param>
		/// <returns></returns>
		public Animation Play(string animation)
		{
			if (!Animations.ContainsKey(animation))
			{
				if (!Animations.ContainsKey(CurrentAnimationName))
				{
					return null;
				}
				
				return Animations[CurrentAnimationName];
			}

			CurrentAnimationName = animation;
			return Animations[animation];
		}

		/// <summary>
		/// Add the given animation inside the animations tree. Return true if the animation is correctly added.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="animation"></param>
		/// <returns></returns>
		public bool AddAnimation(string name, Animation animation)
		{
			if (Animations.ContainsKey(name))
			{
				return false;
			}

			Animations.Add(name, animation);
			return true;
		}

		/// <summary>
		/// Remove the animation which is corresponding with the given name. Return true if the animation if correctly removed
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public bool RemoveAnimation(string name)
		{
			return Animations.Remove(name);
		}

		/// <summary>
		/// Update the current animation
		/// </summary>
		/// <param name="delta"></param>
		public void Update(float delta)
		{
			if (!Animations.ContainsKey(CurrentAnimationName))
			{
				return;
			}

			Animations[CurrentAnimationName].Update(delta);
		}
	}
}
