using System;
using System.Threading;
using Moq;
using NLoop.Core;
using NRaft.Server.Configuration;
using NRaft.Server.States;
using NUnit.Framework;

namespace NRaft.Server.Tests.States
{
	[TestFixture]
	public class FollowerTests
	{
		[Test]
		public void Constructor()
		{
			// assert
			Assert.That(() => new Follower(null), Throws.InstanceOf<ArgumentNullException>());
		}
		[Test]
		public void DoNotStartElectionAfterDisposal()
		{
			// arrange
			var resetEvent = new ManualResetEvent(false);
			var timeout = TimeSpan.FromMilliseconds(100);
			var configMock = new Mock<IServerConfiguration>();
			configMock.Setup(mocked => mocked.ElectionTimeout).Returns(timeout);
			var config = configMock.Object;
			var scheduler = new EventLoop();
			var serverMock = new Mock<IConsensusServerStateApi>();
			serverMock.Setup(mocked => mocked.StartElection()).Callback(() => resetEvent.Set());
			serverMock.Setup(mocked => mocked.Configuration).Returns(config);
			serverMock.Setup(mocked => mocked.Scheduler).Returns(scheduler);
			var server = serverMock.Object;
			var state = new Follower(server);

			// act
			state.Start();
			state.Dispose();
			var changedToCandidate = resetEvent.WaitOne(timeout + timeout);

			// assert
			Assert.That(changedToCandidate, Is.False, "An election timout should not have occurred because the follower is disposed");

			// cleanup
			scheduler.Dispose();
			resetEvent.Dispose();
		}
		[Test]
		public void StartsElectionAfterElectionTimeout()
		{
			// arrange
			var resetEvent = new ManualResetEvent(false);
			var timeout = TimeSpan.FromMilliseconds(100);
			var configMock = new Mock<IServerConfiguration>();
			configMock.Setup(mocked => mocked.ElectionTimeout).Returns(timeout);
			var config = configMock.Object;
			var scheduler = new EventLoop();
			var serverMock = new Mock<IConsensusServerStateApi>();
			serverMock.Setup(mocked => mocked.StartElection()).Callback(() => resetEvent.Set());
			serverMock.Setup(mocked => mocked.Configuration).Returns(config);
			serverMock.Setup(mocked => mocked.Scheduler).Returns(scheduler);
			var server = serverMock.Object;
			var state = new Follower(server);

			// act
			state.Start();
			var changedToCandidate = resetEvent.WaitOne(timeout + timeout);

			// assert
			Assert.That(changedToCandidate, Is.True, "An election timout should have occurred and the state should be changed to candidate");

			// cleanup
			scheduler.Dispose();
			resetEvent.Dispose();
		}
	}
}