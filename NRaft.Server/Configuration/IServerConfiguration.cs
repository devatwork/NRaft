using System;

namespace NRaft.Server.Configuration
{
	/// <summary>
	/// Holds the configuration of a <see cref="ConsensusServer"/>.
	/// </summary>
	public interface IServerConfiguration
	{
		/// <summary>
		/// The timeout to wait before an election is started for this <see cref="Server"/>.
		/// </summary>
		TimeSpan ElectionTimeout { get; }
	}
}