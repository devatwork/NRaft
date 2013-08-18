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
		/// Holds a reference to the <see cref="IResourceTrackingScheduler"/>.
		/// </summary>
		private readonly IResourceTrackingScheduler scheduler;
		/// <summary>
		/// Holds the current <see cref="State"/>.
		/// </summary>
		private State currentState;
		/// <summary>
		/// Creates a new <see cref="ConsensusServerState"/> for a <see cref="ConsensusServer"/>.
		/// </summary>
		/// <param name="scheduler">The <see cref="IResourceTrackingScheduler"/>.</param>
		public ConsensusServerState(IResourceTrackingScheduler scheduler)
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
		/// Gets the <see cref="IResourceTrackingScheduler"/> on which to execute callbacks.
		/// </summary>
		public IResourceTrackingScheduler Scheduler
		{
			get { return scheduler; }
		}
		/// <summary>
		/// Changes to the given <paramref name="newState"/>.
		/// </summary>
		/// <param name="newState">The <see cref="State"/> to change to.</param>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="newState"/> is null.</exception>
		private void ChangeState(State newState)
		{
			// validate arguments
			if (newState == null)
				throw new ArgumentNullException("newState");

			// schedule the state change on the scheduler
			scheduler.Schedule(() => {
				// if we are disposed of do not change the state
				if (IsDisposed)
				{
					// dispose the new state
					newState.Dispose();
					return;
				}

				// dispose the current state if there is one
				if (currentState != null)
					currentState.Dispose();

				// set the new state
				currentState = newState;

				// start the new state
				newState.Start();
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