namespace NRaft.Server.States
{
	/// <summary>
	/// Implements the follower <see cref="State"/> of the <see cref="ConsensusServer"/>.
	/// Followers respond to incoming request of the <see cref="Leader"/> of the cluster or from <see cref="Candidate"/>s.
	/// </summary>
	public class Follower : State
	{
	}
}