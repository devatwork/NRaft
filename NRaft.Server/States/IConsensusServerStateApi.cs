using NLoop.Core;
using NRaft.Server.Configuration;

namespace NRaft.Server.States
{
	/// <summary>
	/// The internal API of the <see cref="ConsensusServer"/>. This API is visible to the <see cref="State"/>s.
	/// </summary>
	public interface IConsensusServerStateApi
	{
		/// <summary>
		/// Gets the <see cref="IResourceTrackingScheduler"/> on which to execute callbacks.
		/// </summary>
		IResourceTrackingScheduler Scheduler { get; }
		/// <summary>
		/// Gets the <see cref="IServerConfiguration"/> of the <see cref="ConsensusServer"/>.
		/// </summary>
		IServerConfiguration Configuration { get; }
		/// <summary>
		/// Starts a new election, starts the <see cref="Candidate"/> <see cref="State"/>.
		/// </summary>
		void StartElection();
	}
}