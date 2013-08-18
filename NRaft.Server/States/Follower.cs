using System;

namespace NRaft.Server.States
{
	/// <summary>
	/// Implements the follower <see cref="State"/> of the <see cref="ConsensusServer"/>.
	/// Followers respond to incoming request of the <see cref="Leader"/> of the cluster or from <see cref="Candidate"/>s.
	/// </summary>
	public class Follower : State
	{
		/// <summary>
		/// Constructs a new <see cref="Follower"/>.
		/// </summary>
		/// <param name="server">The <see cref="ConsensusServer"/> to which this <see cref="State"/> belongs</param>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="server"/> is null.</exception>
		public Follower(ConsensusServer server) : base(server)
		{
		}
	}
}