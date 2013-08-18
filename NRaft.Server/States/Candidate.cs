using System;

namespace NRaft.Server.States
{
	/// <summary>
	/// Implements the candidate <see cref="State"/> of the <see cref="ConsensusServer"/>.
	/// Candidates ask other servers in the cluster for votes to, if granted the majority they become<see cref="Leader"/>.
	/// </summary>
	public class Candidate : State
	{
		/// <summary>
		/// Constructs a new <see cref="Candidate"/>.
		/// </summary>
		/// <param name="server">The <see cref="IConsensusServerStateApi"/> to which this <see cref="State"/> belongs</param>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="server"/> is null.</exception>
		public Candidate(IConsensusServerStateApi server) : base(server)
		{
		}
	}
}