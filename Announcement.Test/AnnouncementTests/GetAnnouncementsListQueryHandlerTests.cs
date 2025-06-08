// using Announcement.Application.Announcement.Query; // Contains the query object GetAnnouncementsListQuery
// using Announcement.Domain.Interfaces; // Contains IAnnouncementRepository interface
// using Announcement.Domain.Entities; // Contains Announcements entity
// using Moq; // Used for mocking dependencies
// using Announcement.Application.Announcement.Query.GetAll; // Contains GetAnnouncementsListQueryHandler

using Announcement.Application.Announcement.Query.GetAll;
using Announcement.Domain.Entities;
using Announcement.Domain.Interfaces;
using Moq;

namespace Announcement.Application.Tests.Handlers;

/// <summary>
/// Unit tests for GetAnnouncementsListQueryHandler.
/// Verifies that a list of AnnouncementDto is returned correctly from the handler.
/// </summary>
[TestFixture]
public class GetAnnouncementsListQueryHandlerTests
{
    private Mock<IAnnouncementRepository> _mockRepository = null!;
    private GetAnnouncementsListQueryHandler _handler = null!;

    /// <summary>
    /// Initializes mock repository and handler before each test.
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        _mockRepository = new Mock<IAnnouncementRepository>();
        _handler = new GetAnnouncementsListQueryHandler(_mockRepository.Object);
    }

    /// <summary>
    /// Tests that the handler returns the expected list of announcement DTOs.
    /// </summary>
    [Test]
    public async Task Handle_ShouldReturnListOfAnnouncementDto()
    {
        // Arrange
        var announcements = new List<Announcements>
        {
            new Announcements
            {
                Id = Guid.NewGuid(),
                Title = "Test 1",
                Description = "Desc 1",
                DateAdded = DateTime.UtcNow
            },
            new Announcements
            {
                Id = Guid.NewGuid(),
                Title = "Test 2",
                Description = "Desc 2",
                DateAdded = DateTime.UtcNow
            }
        };

        _mockRepository.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(announcements);

        // Act
        var result = await _handler.Handle(new GetAnnouncementsListQuery(), CancellationToken.None);

        // Assert
        Assert.That(result, Has.Count.EqualTo(2));
        Assert.That(result[0].Title, Is.EqualTo("Test 1"));
        Assert.That(result[1].Description, Is.EqualTo("Desc 2"));
    }
}
