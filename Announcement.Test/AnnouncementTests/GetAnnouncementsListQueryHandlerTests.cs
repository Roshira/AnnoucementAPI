using Announcement.Application.Announcement.Query;
using Announcement.Domain.Interfaces;
using Announcement.Domain.Entities;
using Moq;

namespace Announcement.Application.Tests.Handlers;

[TestFixture]
public class GetAnnouncementsListQueryHandlerTests
{
    private Mock<IAnnouncementRepository> _mockRepository = null!;
    private GetAnnouncementsListQueryHandler _handler = null!;

    [SetUp]
    public void SetUp()
    {
        _mockRepository = new Mock<IAnnouncementRepository>();
        _handler = new GetAnnouncementsListQueryHandler(_mockRepository.Object);
    }

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