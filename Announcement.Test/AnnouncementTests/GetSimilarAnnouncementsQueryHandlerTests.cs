// GetSimilarAnnouncementsQueryHandlerTests.cs
using Announcement.Application.Announcement.DTOs;
using Announcement.Application.Announcement.Queries.GetSimilarAnnouncements;
using Announcement.Domain.Entities;
using Announcement.Domain.Interfaces;
using MediatR;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Announcement.Application.Tests.Announcement.Queries.GetSimilarAnnouncements
{
    /// <summary>
    /// Тести для обробника запитів на отримання схожих оголошень.
    /// </summary>
    [TestFixture]
    public class GetSimilarAnnouncementsQueryHandlerTests
    {
        private Mock<IAnnouncementRepository> _repositoryMock;
        private GetSimilarAnnouncementsQueryHandler _handler;

        /// <summary>
        /// Ініціалізація перед кожним тестом.
        /// Створює мок репозиторію та екземпляр обробника.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IAnnouncementRepository>();
            _handler = new GetSimilarAnnouncementsQueryHandler(_repositoryMock.Object);
        }

        /// <summary>
        /// Перевіряє, що метод Handle повертає найбільш схожі оголошення.
        /// </summary>
        [Test]
        public async Task Handle_ShouldReturnMostSimilarAnnouncements()
        {
            // Arrange - підготовка тестових даних
            var targetId = Guid.NewGuid();
            var targetAnnouncement = new Announcements
            {
                Id = targetId,
                Title = "Sell iPhone 13 Pro Max",
                Description = "Brand new iPhone 13 Pro Max 256GB"
            };

            var otherAnnouncements = new List<Announcements>
            {
                new Announcements
                {
                    Id = Guid.NewGuid(),
                    Title = "Sell iPhone 12",
                    Description = "Good condition iPhone 12 128GB"
                },
                new Announcements
                {
                    Id = Guid.NewGuid(),
                    Title = "MacBook Pro 16",
                    Description = "M1 Pro chip 16GB RAM 512GB SSD"
                },
                new Announcements
                {
                    Id = Guid.NewGuid(),
                    Title = "iPhone 13 Pro Max case",
                    Description = "Leather case for iPhone 13 Pro Max"
                },
                new Announcements
                {
                    Id = Guid.NewGuid(),
                    Title = "iPhone 13 Pro",
                    Description = "Like new iPhone 13 Pro 128GB"
                }
            };

            _repositoryMock.Setup(x => x.GetByIdAsync(targetId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(targetAnnouncement);

            _repositoryMock.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(otherAnnouncements);

            var query = new GetSimilarAnnouncementsQuery { Id = targetId, Count = 3 };

            // Act - виклик методу Handle
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert - перевірка результатів
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(3));

            // Перевіряємо, що найбільш схожі оголошення повертаються першими
            var firstResult = result.First();
            Assert.That(firstResult.Title, Does.Contain("iPhone 13 Pro Max"));
        }

        /// <summary>
        /// Перевіряє, що метод Handle повертає порожній список, якщо оголошення з заданим Id не знайдено.
        /// </summary>
        [Test]
        public async Task Handle_WithNonExistingId_ShouldReturnEmptyList()
        {
            // Arrange
            var nonExistingId = Guid.NewGuid();

            _repositoryMock.Setup(x => x.GetByIdAsync(nonExistingId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Announcements)null);

            var query = new GetSimilarAnnouncementsQuery { Id = nonExistingId };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }
    }
}
