// using Announcement.Application.Announcement.DTOs; // Contains data transfer objects like AnnouncementDto
// using Announcement.Application.Announcement.Query.GetById; // Contains GetAnnouncementByIdQuery and its handler
// using Announcement.Domain.Interfaces; // Contains IAnnouncementRepository interface
// using Announcement.Domain.Entities; // Contains Announcements entity
// using MediatR; // Provides IRequestHandler interface for handling queries
// using Moq; // Used for creating mocks in unit tests
// using NUnit.Framework; // Provides NUnit testing attributes and assertions
// using System; // Provides base .NET types like Guid and DateTime
// using System.Threading; // Provides CancellationToken support
// using System.Threading.Tasks; // Provides Task-based async support

using Announcement.Application.Announcement.Query.GetById;
using Announcement.Domain.Interfaces;
using Moq;

namespace Announcement.Application.Tests.Announcement.Query.GetById
{
    /// <summary>
    /// Unit tests for GetAnnouncementByIdQueryHandler.
    /// Verifies correct behavior when an announcement is found or not found by ID.
    /// </summary>
    [TestFixture]
    public class GetAnnouncementByIdQueryHandlerTests
    {
        private Mock<IAnnouncementRepository> _repositoryMock;
        private GetAnnouncementByIdQueryHandler _handler;

        /// <summary>
        /// Sets up the mock repository and handler before each test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IAnnouncementRepository>();
            _handler = new GetAnnouncementByIdQueryHandler(_repositoryMock.Object);
        }

        /// <summary>
        /// Verifies that the handler returns the expected AnnouncementDto when the ID exists.
        /// </summary>
        [Test]
        public async Task Handle_WithExistingId_ShouldReturnAnnouncementDto()
        {
            // Arrange
            var announcementId = Guid.NewGuid();
            var dateAdded = DateTime.UtcNow;
            var announcement = new Domain.Entities.Announcements
            {
                Id = announcementId,
                Title = "Test Title",
                Description = "Test Description",
                DateAdded = dateAdded
            };

            _repositoryMock.Setup(x => x.GetByIdAsync(announcementId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(announcement);

            var query = new GetAnnouncementByIdQuery { Id = announcementId };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(announcementId));
            Assert.That(result.Title, Is.EqualTo("Test Title"));
            Assert.That(result.Description, Is.EqualTo("Test Description"));
            Assert.That(result.DateAdded, Is.EqualTo(dateAdded));
        }

        /// <summary>
        /// Verifies that the handler returns null when no announcement is found with the given ID.
        /// </summary>
        [Test]
        public async Task Handle_WithNonExistingId_ShouldReturnNull()
        {
            // Arrange
            var announcementId = Guid.NewGuid();

            _repositoryMock.Setup(x => x.GetByIdAsync(announcementId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Domain.Entities.Announcements)null);

            var query = new GetAnnouncementByIdQuery { Id = announcementId };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Null);
        }
    }
}
