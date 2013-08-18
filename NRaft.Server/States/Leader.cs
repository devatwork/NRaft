using System;

namespace NRaft.Server.States
{
	/// <summary>
	/// Implements the leader <see cref="State"/> of the <see cref="ConsensusServer"/>. 
	/// There is at most one leader in the cluster. Leader manage log distribution.
	/// </summary>
	public class Leader : State
	{
		/// <summary>
		/// Constructs a new <see cref="Leader"/>.
		/// </summary>
		/// <param name="server">The <see cref="ConsensusServer"/> to which this <see cref="State"/> belongs</param>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="server"/> is null.</exception>
		public Leader(ConsensusServer server) : base(server)
		{
		}
	}
}