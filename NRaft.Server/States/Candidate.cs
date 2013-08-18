namespace NRaft.Server.States
{
	/// <summary>
	/// Implements the candidate <see cref="State"/> of the <see cref="ConsensusServer"/>.
	/// Candidates ask other servers in the cluster for votes to, if granted the majority they become<see cref="Leader"/>.
	/// </summary>
	public class Candidate : State
	{
	}
}