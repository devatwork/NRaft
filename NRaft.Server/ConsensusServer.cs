using System;
using NLoop.Core;
using NLoop.Core.Utils;
using NRaft.Server.Configuration;
using NRaft.Server.Protocols;
using NRaft.Server.States;
using NRaft.Server.Storage;

namespace NRaft.Server
{
	/// <summary>
	/// Implements the RAFT consensus server.
	/// </summary>
	public class ConsensusServer : Disposable
	{
		/// <summary>
		/// Holds a reference to the <see cref="ServerConfiguration"/> of this <see cref="ConsensusServer"/>.
		/// </summary>
		private readonly ServerConfiguration configuration;
		/// <summary>
		/// Holds a reference to the <see cref="ILog"/> used by this <see cref="ConsensusServer"/> to store entries in.
		/// </summary>
		private readonly ILog log;
		/// <summary>
		/// Holds a reference to the <see cref="IProtocol"/> using by this <see cref="ConsensusServer"/> to communicate with other <see cref="ConsensusServer"/>s in the same cluster.
		/// </summary>
		private readonly IProtocol protocol;
		/// <summary>
		/// Holds a reference to the <see cref="IResourceTrackingScheduler"/> on which callbacks can be executed.
		/// </summary>
		private readonly IResourceTrackingScheduler scheduler;
		/// <summary>
		/// Holds a reference to the <see cref="ConsensusServerState"/>.
		/// </summary>
		private readonly ConsensusServerState state;
		/// <summary>
		/// Constructs a new <see cref="ConsensusServer"/>.
		/// </summary>
		/// <param name="configuration">The <see cref="ServerConfiguration"/> of this <see cref="ConsensusServer"/>.</param>
		/// <param name="log">The <see cref="ILog"/> used by this <see cref="ConsensusServer"/> to store entries in.</param>
		/// <param name="protocol">The <see cref="IProtocol"/> using by this <see cref="ConsensusServer"/> to communicate with other <see cref="ConsensusServer"/>s in the same cluster.</param>
		/// <param name="scheduler">The <see cref="IResourceTrackingScheduler"/> on which to execute callbacks.</param>
		/// <exception cref="ArgumentNullException">Thrown if one of the parameters is null.</exception>
		public ConsensusServer(ServerConfiguration configuration, ILog log, IProtocol protocol, IResourceTrackingScheduler scheduler)
		{
			// validate arguments
			if (configuration == null)
				throw new ArgumentNullException("configuration");
			if (scheduler == null)
				throw new ArgumentNullException("scheduler");
			if (log == null)
				throw new ArgumentNullException("log");
			if (protocol == null)
				throw new ArgumentNullException("protocol");

			// store the arguments
			this.configuration = configuration;
			this.scheduler = scheduler;
			this.log = log;
			this.protocol = protocol;

			// create a new state
			state = new ConsensusServerState(this);
		}
		/// <summary>
		/// Gets the <see cref="ServerConfiguration"/> of this <see cref="ConsensusServer"/>.
		/// </summary>
		public ServerConfiguration Configuration
		{
			get { return configuration; }
		}
		/// <summary>
		/// Gets the <see cref="ILog"/> used by this <see cref="ConsensusServer"/> to store entries in.
		/// </summary>
		public ILog Log
		{
			get { return log; }
		}
		/// <summary>
		/// Gets the <see cref="IProtocol"/> using by this <see cref="ConsensusServer"/> to communicate with other <see cref="ConsensusServer"/>s in the same cluster.
		/// </summary>
		public IProtocol Protocol
		{
			get { return protocol; }
		}
		/// <summary>
		/// Gets the <see cref="IResourceTrackingScheduler"/> on which callbacks can be executed.
		/// </summary>
		public IResourceTrackingScheduler Scheduler
		{
			get { return scheduler; }
		}
		/// <summary>
		/// Dispose resources. Override this method in derived classes. Unmanaged resources should always be released
		/// when this method is called. Managed resources may only be disposed of if disposeManagedResources is true.
		/// </summary>
		/// <param name="disposeManagedResources">A value which indicates whether managed resources may be disposed of.</param>
		protected override void DisposeResources(bool disposeManagedResources)
		{
			// we do not hold unmanaged resources
			if (!disposeManagedResources)
				return;

			// dispose resources
			state.Dispose();
			log.Dispose();
			protocol.Dispose();
		}
	}
}