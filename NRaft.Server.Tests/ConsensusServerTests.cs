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
			var configuration = new ServerConfiguration();
			var scheduler = new Mock<IResourceTrackingScheduler>().Object;
			var log = new Mock<ILog>().Object;
			var protocol = new Mock<IProtocol>().Object;

			// assert
			Assert.That(() => new ConsensusServer(null, scheduler, log, protocol), Throws.InstanceOf<ArgumentNullException>());
			Assert.That(() => new ConsensusServer(configuration, null, log, protocol), Throws.InstanceOf<ArgumentNullException>());
			Assert.That(() => new ConsensusServer(configuration, scheduler, null, protocol), Throws.InstanceOf<ArgumentNullException>());
			Assert.That(() => new ConsensusServer(configuration, scheduler, log, null), Throws.InstanceOf<ArgumentNullException>());
		}
		[Test]
		public void DisposesLogAndProtocol()
		{
			// arrange
			var configuration = new ServerConfiguration();
			var scheduler = new Mock<IResourceTrackingScheduler>().Object;
			var logMock = new Mock<ILog>();
			var log = logMock.Object;
			var protocolMock = new Mock<IProtocol>();
			var protocol = protocolMock.Object;
			var server = new ConsensusServer(configuration, scheduler, log, protocol);

			// act
			server.Dispose();

			// assert
			logMock.Verify(x => x.Dispose(), Times.Once());
			protocolMock.Verify(x => x.Dispose(), Times.Once());
		}
	}
}