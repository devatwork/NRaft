using System;
using NLoop.Core;
using NRaft.Server.Utils;

namespace NRaft.Server.States
{
	/// <summary>
	/// Holds the <see cref="State"/> of a <see cref="ConsensusServer"/>
	/// </summary>
	public class ConsensusServerState : Disposable, IConsensusServerStateApi
	{
		/// <summary>
		/// Holds a reference to the <see cref="IScheduler"/>.
		/// </summary>
		private readonly IScheduler scheduler;
		/// <summary>
		/// Holds the current <see cref="State"/>.
		/// </summary>
		private State currentState;
		/// <summary>
		/// Creates a new <see cref="ConsensusServerState"/> for a <see cref="ConsensusServer"/>.
		/// </summary>
		/// <param name="scheduler">The <see cref="IScheduler"/>.</param>
		public ConsensusServerState(IScheduler scheduler)
		{
			// validate arguments
			if (scheduler == null)
				throw new ArgumentNullException("scheduler");

			// store the argument
			this.scheduler = scheduler;

			// always start as a follower
			ChangeState(new Follower(this));
		}
		/// <summary>
		/// Changes to the given <paramref name="state"/>.
		/// </summary>
		/// <param name="state">The <see cref="State"/> to change to.</param>
		private void ChangeState(State state)
		{
			// validate arguments
			if (state == null)
				throw new ArgumentNullException("state");

			// schedule the state change on the scheduler
			scheduler.Schedule(() => {
				// if we are disposed of do not change the state
				if (IsDisposed)
				{
					// dispose the new state
					state.Dispose();
					return;
				}

				// dispose the current state if there is one
				if (currentState != null)
					currentState.Dispose();

				// set the new state
				currentState = state;
			});
		}
		/// <summary>
		/// Dispose resources. Override this method in derived classes. Unmanaged resources should always be released
		/// when this method is called. Managed resources may only be disposed of if disposeManagedResources is true.
		/// </summary>
		/// <param name="disposeManagedResources">A value which indicates whether managed resources may be disposed of.</param>
		protected override void DisposeResources(bool disposeManagedResources)
		{
			// we do not hold unmanaged resource
			if (!disposeManagedResources)
				return;

			// dispose the current state
			if (currentState != null)
				currentState.Dispose();
		}
	}
}