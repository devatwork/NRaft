using System;

namespace NRaft.Server.Protocols
{
	/// <summary>
	/// Represents the communication protocol for <see cref="ConsensusServer"/>.
	/// </summary>
	public interface IProtocol : IDisposable
	{
	}
}