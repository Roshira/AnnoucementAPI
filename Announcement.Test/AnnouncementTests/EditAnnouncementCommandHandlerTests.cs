// using Announcement.Application.Announcement.Commands.Edit; // Contains EditAnnouncementCommand and its handler
// using Announcement.Domain.Interfaces; // Contains IAnnouncementRepository interface
// using Announcement.Domain.Entities; // Contains Announcements entity
// using MediatR; // Provides support for IRequestHandler
// using Moq; // Used for mocking repository interactions in tests
// using NUnit.Framework; // Provides NUnit testing functionality
// using System; // Provides base types like Guid
// using System.Threading; // Provides CancellationToken
// using System.Threading.Tasks; // Enables asynchronous programming with Task

using Announcement.Application.Announcement.Commands.Edit;
using Announcement.Domain.Interfaces;
using Moq;

namespace Announcement.Application.Tests.Announcement.Commands.Edit
{
    /// <summary>
    /// Unit tests for EditAnnouncementCommandHandler.
    /// Verifies behavior when editing an existing or non-existing announcement.
    /// </summary>
    [TestFixture]
    public class EditAnnouncementCommandHandlerTests
    {
        private Mock<IAnnouncementRepository> _repositoryMock;
        private EditAnnouncementCommandHandler _handler;

        /// <summary>
        /// Sets up the mock repository and handler before each test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IAnnouncementRepository>();
            _handler = new EditAnnouncementCommandHandler(_repositoryMock.Object);
        }

        /// <summary>
        /// Verifies that the handler updates the announcement and returns true when the ID exists.
        /// </summary>
        [Test]
        public async Task Handle_WithExistingId_ShouldUpdateAndReturnTrue()
        {
            // Arrange
            var announcementId = Guid.NewGuid();
            var existingAnnouncement = new Domain.Entities.Announcements
            {
                Id = announcementId,
                Title = "Old Title",
                Description = "Old Description"
            };

            _repositoryMock.Setup(x => x.GetByIdAsync(announcementId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingAnnouncement);

            var command = new EditAnnouncementCommand
            {
                Id = announcementId,
                Title = "New Title",
                Description = "New Description"
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.True);
            Assert.That(existingAnnouncement.Title, Is.EqualTo("New Title"));
            Assert.That(existingAnnouncement.Description, Is.EqualTo("New Description"));
            _repositoryMock.Verify(x => x.UpdateAsync(existingAnnouncement, It.IsAny<CancellationToken>()), Times.Once);
        }

        /// <summary>
        /// Verifies that the handler returns false when the announcement is not found.
        /// </summary>
        [Test]
        public async Task Handle_WithNonExistingId_ShouldReturnFalse()
        {
            // Arrange
            var announcementId = Guid.NewGuid();

            _repositoryMock.Setup(x => x.GetByIdAsync(announcementId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Domain.Entities.Announcements)null);

            var command = new EditAnnouncementCommand
            {
                Id = announcementId,
                Title = "New Title",
                Description = "New Description"
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.False);
            _repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Domain.Entities.Announcements>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
