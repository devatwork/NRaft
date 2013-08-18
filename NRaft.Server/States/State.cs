using System;

namespace NRaft.Server.States
{
	/// <summary>
	/// Implements the state a <see cref="ConsensusServer"/> can be in.
	/// </summary>
	public abstract class State
	{
		/// <summary>
		/// Holds a reference to the <see cref="ConsensusServer"/> for this state.
		/// </summary>
		private readonly ConsensusServer server;
		/// <summary>
		/// Constructs a new <see cref="State"/>.
		/// </summary>
		/// <param name="server">The <see cref="ConsensusServer"/> to which this <see cref="State"/> belongs</param>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="server"/> is null.</exception>
		protected State(ConsensusServer server)
		{
			// validate arguments
			if (server == null)
				throw new ArgumentNullException("server");

			// store the argument
			this.server = server;
		}
		/// <summary>
		/// Gets the <see cref="ConsensusServer"/> to which this <see cref="State"/> belongs.
		/// </summary>
		protected ConsensusServer Server
		{
			get { return server; }
		}
	}
}