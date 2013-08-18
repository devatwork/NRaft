namespace NRaft.Core
{
	/// <summary>
	/// Represents a single log entry.
	/// </summary>
	public class LogEntry
	{
		/// <summary>
		/// Gets/Sets the term when entry was received by leader.
		/// </summary>
		public long Term { get; set; }
		/// <summary>
		/// Gets/Sets the position of entry in the log.
		/// </summary>
		public long CommitIndex { get; set; }
	}
}