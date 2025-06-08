// using Announcement.Application.Announcement.Commands.Delete; // Contains DeleteAnnouncementCommand and its handler
// using Announcement.Domain.Interfaces; // Interface for announcement repository
// using Announcement.Domain.Entities; // Contains Announcements entity
// using MediatR; // Provides MediatR request/response interfaces
// using Moq; // Library for mocking interfaces
// using NUnit.Framework; // NUnit framework for unit testing
// using System; // Includes Guid
// using System.Threading; // Supports CancellationToken
// using System.Threading.Tasks; // Enables async/await pattern

using Announcement.Application.Announcement.Commands.Delete;
using Announcement.Domain.Interfaces;
using Moq;

namespace Announcement.Application.Tests.Announcement.Commands.Delete
{
    /// <summary>
    /// Unit tests for DeleteAnnouncementCommandHandler.
    /// Checks correct behavior when deleting existing or non-existing announcements.
    /// </summary>
    [TestFixture]
    public class DeleteAnnouncementCommandHandlerTests
    {
        private Mock<IAnnouncementRepository> _repositoryMock;
        private DeleteAnnouncementCommandHandler _handler;

        /// <summary>
        /// Sets up a mocked repository and command handler before each test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IAnnouncementRepository>();
            _handler = new DeleteAnnouncementCommandHandler(_repositoryMock.Object);
        }

        /// <summary>
        /// Should return true and call DeleteAsync when the announcement exists.
        /// </summary>
        [Test]
        public async Task Handle_WithExistingId_ShouldReturnTrue()
        {
            // Arrange
            var announcementId = Guid.NewGuid();
            var announcement = new Domain.Entities.Announcements { Id = announcementId };

            _repositoryMock.Setup(x => x.GetByIdAsync(announcementId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(announcement);

            var command = new DeleteAnnouncementCommand { Id = announcementId };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.True);
            _repositoryMock.Verify(x => x.DeleteAsync(announcement, It.IsAny<CancellationToken>()), Times.Once);
        }

        /// <summary>
        /// Should return false and not call DeleteAsync when the announcement does not exist.
        /// </summary>
        [Test]
        public async Task Handle_WithNonExistingId_ShouldReturnFalse()
        {
            // Arrange
            var announcementId = Guid.NewGuid();

            _repositoryMock.Setup(x => x.GetByIdAsync(announcementId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Domain.Entities.Announcements)null);

            var command = new DeleteAnnouncementCommand { Id = announcementId };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.False);
            _repositoryMock.Verify(x => x.DeleteAsync(It.IsAny<Domain.Entities.Announcements>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
