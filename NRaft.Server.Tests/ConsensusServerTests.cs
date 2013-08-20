using System;
using Moq;
using NLoop.Core;
using NRaft.Server.Configuration;
using NRaft.Server.Protocols;
using NRaft.Server.Storage;
using NUnit.Framework;

namespace NRaft.Server.Tests
{
	[TestFixture]
	public class ConsensusServerTests
	{
		[Test]
		public void Constructor()
		{
			// arrange
			var configuration = new Mock<IServerConfiguration>().Object;
			var log = new Mock<ILog>().Object;
			var protocol = new Mock<IProtocol>().Object;
			var scheduler = new Mock<IResourceTrackingScheduler>().Object;

			// assert
			Assert.That(() => new ConsensusServer(null, log, protocol, scheduler), Throws.InstanceOf<ArgumentNullException>());
			Assert.That(() => new ConsensusServer(configuration, null, protocol, scheduler), Throws.InstanceOf<ArgumentNullException>());
			Assert.That(() => new ConsensusServer(configuration, log, null, scheduler), Throws.InstanceOf<ArgumentNullException>());
			Assert.That(() => new ConsensusServer(configuration, log, protocol, null), Throws.InstanceOf<ArgumentNullException>());

			// act
			var server = new ConsensusServer(configuration, log, protocol, scheduler);

			// assert
			Assert.That(server.Configuration, Is.SameAs(configuration));
			Assert.That(server.Log, Is.SameAs(log));
			Assert.That(server.Protocol, Is.SameAs(protocol));
			Assert.That(server.Scheduler, Is.SameAs(scheduler));
		}
		[Test]
		public void DisposesLogAndProtocol()
		{
			// arrange
			var configuration = new Mock<IServerConfiguration>().Object;
			var scheduler = new EventLoop();
			var logMock = new Mock<ILog>();
			var log = logMock.Object;
			var protocolMock = new Mock<IProtocol>();
			var protocol = protocolMock.Object;
			var server = new ConsensusServer(configuration, log, protocol, scheduler);

			// act
			server.Dispose();

			// assert
			logMock.Verify(x => x.Dispose(), Times.Once());
			protocolMock.Verify(x => x.Dispose(), Times.Once());

			// cleanup
			scheduler.Dispose();
		}
	}
}