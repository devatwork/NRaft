using System;
using NRaft.Server.States;
using NUnit.Framework;

namespace NRaft.Server.Tests.States
{
	[TestFixture]
	public class ConsensusServerStateTests
	{
		[Test]
		public void Constructor()
		{
			// assert
			Assert.That(() => new ConsensusServerState(null), Throws.InstanceOf<ArgumentNullException>());
		}
	}
}