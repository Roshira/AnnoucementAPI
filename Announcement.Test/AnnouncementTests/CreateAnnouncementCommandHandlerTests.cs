using Announcement.Application.Announcement.Commands;
using Announcement.Domain.Interfaces;
using Announcement.Domain.Entities;
using Moq;
using NUnit.Framework;
using System;
using Announcement.Application.Announcement.Commands.Create;

namespace Announcement.Application.Tests.Handlers;

/// <summary>
/// Unit tests for CreateAnnouncementCommandHandler.
/// Ensures new announcements are created and persisted properly.
/// </summary>
[TestFixture]
public class CreateAnnouncementCommandHandlerTests
{
    private Mock<IAnnouncementRepository> _mockRepository = null!;
    private CreateAnnouncementCommandHandler _handler = null!;
    /// <summary>
    /// Initializes the mock repository and handler before each test.
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        _mockRepository = new Mock<IAnnouncementRepository>();
        _handler = new CreateAnnouncementCommandHandler(_mockRepository.Object);
    }
    /// <summary>
    /// Verifies that a valid announcement is added to the repository and returns a valid Guid.
    /// </summary>
    [Test]
    public async Task Handle_ShouldAddAnnouncement_AndReturnId()
    {
        // Arrange
        var command = new CreateAnnouncementCommand("Test title", "Test description");

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mockRepository.Verify(r => r.AddAsync(It.Is<Announcements>(
            a => a.Title == "Test title" && a.Description == "Test description"), It.IsAny<CancellationToken>()), Times.Once);

        Assert.That(result, Is.Not.EqualTo(Guid.Empty));
    }
}