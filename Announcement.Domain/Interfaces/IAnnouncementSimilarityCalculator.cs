using System;

namespace Announcement.Domain.Interfaces
{
    /// <summary>
    /// Defines a method to calculate similarity between two announcements.
    /// </summary>
    public interface IAnnouncementSimilarityCalculator
    {
        /// <summary>
        /// Calculates a similarity score between two announcement entities.
        /// </summary>
        /// <param name="a1">The first announcement.</param>
        /// <param name="a2">The second announcement.</param>
        /// <returns>A double value representing the similarity score, typically between 0 and 1.</returns>
        double CalculateSimilarity(Domain.Entities.Announcements a1, Domain.Entities.Announcements a2);
    }
}
