using System;

namespace NRaft.Server.States
{
	/// <summary>
	/// Implements the state a <see cref="ConsensusServer"/> can be in.
	/// </summary>
	public abstract class State
	{
		/// <summary>
		/// Holds a reference to the <see cref="IConsensusServerStateApi"/> for this state.
		/// </summary>
		private readonly IConsensusServerStateApi server;
		/// <summary>
		/// Constructs a new <see cref="State"/>.
		/// </summary>
		/// <param name="server">The <see cref="IConsensusServerStateApi"/> to which this <see cref="State"/> belongs</param>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="server"/> is null.</exception>
		protected State(IConsensusServerStateApi server)
		{
			// validate arguments
			if (server == null)
				throw new ArgumentNullException("server");

			// store the argument
			this.server = server;
		}
		/// <summary>
		/// Gets the <see cref="IConsensusServerStateApi"/> to which this <see cref="State"/> belongs.
		/// </summary>
		protected IConsensusServerStateApi Server
		{
			get { return server; }
		}
	}
}