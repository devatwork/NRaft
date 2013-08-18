using System;
using Moq;
using NRaft.Server.States;
using NUnit.Framework;

namespace NRaft.Server.Tests.States
{
	[TestFixture]
	public class StateTests
	{
		private class TestState : State
		{
			public TestState(IConsensusServerStateApi server) : base(server)
			{
			}
			public IConsensusServerStateApi PublicServer
			{
				get { return Server; }
			}
		}
		[Test]
		public void Constructor()
		{
			// arrange
			var server = new Mock<IConsensusServerStateApi>().Object;

			// assert
			Assert.That(() => new TestState(null), Throws.InstanceOf<ArgumentNullException>());

			// act
			var state = new TestState(server);

			// assert
			Assert.That(state.PublicServer, Is.SameAs(server));
		}
	}
}