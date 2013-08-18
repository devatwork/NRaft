using System;
using NRaft.Server.States;
using NUnit.Framework;

namespace NRaft.Server.Tests.States
{
	[TestFixture]
	public class StateTests
	{
		private class TestState : State
		{
			public TestState(ConsensusServer server) : base(server)
			{
			}
		}
		[Test]
		public void Constructor()
		{
			// assert
			Assert.That(() => new TestState(null), Throws.InstanceOf<ArgumentNullException>());
		}
	}
}