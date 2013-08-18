using NLoop.Core;

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
	}
}