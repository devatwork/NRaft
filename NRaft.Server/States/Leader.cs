namespace NRaft.Server.States
{
	/// <summary>
	/// Implements the leader <see cref="State"/> of the <see cref="ConsensusServer"/>. 
	/// There is at most one leader in the cluster. Leader manage log distribution.
	/// </summary>
	public class Leader : State
	{
	}
}