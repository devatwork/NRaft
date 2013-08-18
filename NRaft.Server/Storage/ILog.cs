using System;
using NRaft.Core;

namespace NRaft.Server.Storage
{
	/// <summary>
	/// Reliably stores <see cref="LogEntry"/>s and state data of a <see cref="ConsensusServer"/>.
	/// </summary>
	public interface ILog : IDisposable
	{
	}
}