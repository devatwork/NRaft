using System;
using NLoop.Timing;

namespace NRaft.Server.States
{
	/// <summary>
	/// Implements the follower <see cref="State"/> of the <see cref="ConsensusServer"/>.
	/// Followers respond to incoming request of the <see cref="Leader"/> of the cluster or from <see cref="Candidate"/>s.
	/// </summary>
	public class Follower : State
	{
		/// <summary>
		/// Holds the timer which tracks if an election timeout should occur.
		/// </summary>
		private ITimer watchDog;
		/// <summary>
		/// Constructs a new <see cref="Follower"/>.
		/// </summary>
		/// <param name="server">The <see cref="IConsensusServerStateApi"/> to which this <see cref="State"/> belongs</param>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="server"/> is null.</exception>
		public Follower(IConsensusServerStateApi server) : base(server)
		{
		}
		/// <summary>
		/// Starts this state.
		/// </summary>
		public override void Start()
		{
			base.Start();

			// start the watchdog timer
			watchDog = Server.Scheduler.SetTimeout(ElectionTimedOut, Server.Configuration.ElectionTimeout);
		}
		/// <summary>
		/// Invoked when an election timeout occurred. The state will change to <see cref="Candidate"/>.
		/// </summary>
		private void ElectionTimedOut()
		{
			// cancel the timer to prevent a double trigger
			watchDog.Cancel();

			// start an election
			Server.StartElection();
		}
		/// <summary>
		/// Dispose resources. Override this method in derived classes. Unmanaged resources should always be released
		/// when this method is called. Managed resources may only be disposed of if disposeManagedResources is true.
		/// </summary>
		/// <param name="disposeManagedResources">A value which indicates whether managed resources may be disposed of.</param>
		protected override void DisposeResources(bool disposeManagedResources)
		{
			base.DisposeResources(disposeManagedResources);

			// we only hold managed resources
			if (!disposeManagedResources)
				return;

			// cancel the timer
			if (watchDog != null)
				watchDog.Cancel();
		}
	}
}