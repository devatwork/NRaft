using System;
using NLoop.Core;
using NRaft.Server.Configuration;
using NRaft.Server.Utils;

namespace NRaft.Server.States
{
	/// <summary>
	/// Holds the <see cref="State"/> of a <see cref="ConsensusServer"/>
	/// </summary>
	public class ConsensusServerState : Disposable, IConsensusServerStateApi
	{
		/// <summary>
		/// Holds a reference to the <see cref="ConsensusServer"/>.
		/// </summary>
		private readonly ConsensusServer server;
		/// <summary>
		/// Holds the current <see cref="State"/>.
		/// </summary>
		private State currentState;
		/// <summary>
		/// Creates a new <see cref="ConsensusServerState"/> for a <see cref="ConsensusServer"/>.
		/// </summary>
		/// <param name="server">The <see cref="ConsensusServer"/>.</param>
		public ConsensusServerState(ConsensusServer server)
		{
			// validate arguments
			if (server == null)
				throw new ArgumentNullException("server");

			// store the argument
			this.server = server;

			// always start as a follower
			ChangeState(new Follower(this));
		}
		/// <summary>
		/// Gets the <see cref="IResourceTrackingScheduler"/> on which to execute callbacks.
		/// </summary>
		public IResourceTrackingScheduler Scheduler
		{
			get { return server.Scheduler; }
		}
		/// <summary>
		/// Gets the <see cref="IServerConfiguration"/> of the <see cref="ConsensusServer"/>.
		/// </summary>
		public IServerConfiguration Configuration
		{
			get { return server.Configuration; }
		}
		/// <summary>
		/// Starts a new election, starts the <see cref="Candidate"/> <see cref="State"/>.
		/// </summary>
		public void StartElection()
		{
			// change to the candidate state
			ChangeState(new Candidate(this));
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