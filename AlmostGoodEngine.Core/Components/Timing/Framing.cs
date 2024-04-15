using AlmostGoodEngine.Core.ECS;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace AlmostGoodEngine.Core.Components.Timing
{
	public class Framing : Component
	{
		/// <summary>
		/// The current frame
		/// </summary>
		public int Frame { get; private set; }

		/// <summary>
		/// If the component may work inside the fixed time step update
		/// </summary>
		public bool IsPhysics { get; private set; }

		/// <summary>
		/// True is the framing component is currently running
		/// </summary>
		public bool IsRunning { get; private set; }

		/// <summary>
		/// The number of frames maximum computed
		/// </summary>
		public int Max { get; private set; }

		/// <summary>
		/// A dictionary containing actions the framing system will execute
		/// </summary>
		public Dictionary<int, Action> Actions { get; private set; }

		public Framing(int max)
		{
			Max = max;
		}

		/// <summary>
		/// Launch the framing system
		/// </summary>
		/// <param name="restart"></param>
		public void Launch(bool restart = true)
		{
			IsRunning = true;

			if (restart)
			{
				Frame = 0;
			}
		}

		/// <summary>
		/// Stop the framing system
		/// </summary>
		/// <param name="reset"></param>
		public void Stop(bool reset = true)
		{
			IsRunning = false;
			
			if (reset)
			{
				Frame = 0;
			}
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			if (!IsRunning || IsPhysics)
			{
				return;
			}

			Increment();
		}

		public override void FixedUpdate(GameTime gameTime)
		{
			base.FixedUpdate(gameTime);

			if (!IsRunning || !IsPhysics)
			{
				return;
			}

			Increment();
		}

		/// <summary>
		/// Execute the next frame
		/// </summary>
		private void Increment()
		{
			Frame++;

			// Check if an action is binded to the specific frame
			if (Actions.TryGetValue(Frame, out Action action))
			{
				action();
			}

			if (Frame >= Max)
			{
				Stop();
			}
		}
	}
}
