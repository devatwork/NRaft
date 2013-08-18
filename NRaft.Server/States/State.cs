using System;
using NRaft.Server.Utils;

namespace NRaft.Server.States
{
	/// <summary>
	/// Implements the state a <see cref="ConsensusServer"/> can be in.
	/// </summary>
	public abstract class State : Disposable
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
		/// <summary>
		/// Dispose resources. Override this method in derived classes. Unmanaged resources should always be released
		/// when this method is called. Managed resources may only be disposed of if disposeManagedResources is true.
		/// </summary>
		/// <param name="disposeManagedResources">A value which indicates whether managed resources may be disposed of.</param>
		protected override void DisposeResources(bool disposeManagedResources)
		{
		}
		/// <summary>
		/// Starts this state.
		/// </summary>
		public virtual void Start()
		{
		}
	}
}