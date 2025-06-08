// AnnouncementSimilarityCalculator.cs
using Announcement.Domain.Entities;
using Announcement.Domain.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Announcement.Application.Announcement.Queries.GetSimilarAnnouncements
{
    /// <summary>
    /// Calculates similarity between two announcements based on their titles and descriptions.
    /// </summary>
    public class AnnouncementSimilarityCalculator : IAnnouncementSimilarityCalculator
    {
        /// <summary>
        /// Calculates a similarity score between two announcements.
        /// The score is based on the ratio of common words in their titles and descriptions.
        /// </summary>
        /// <param name="a1">The first announcement.</param>
        /// <param name="a2">The second announcement.</param>
        /// <returns>A similarity score between 0 and 1, where 1 means identical content.</returns>
        public double CalculateSimilarity(Domain.Entities.Announcements a1, Domain.Entities.Announcements a2)
        {
            // Get words from titles and descriptions
            var words1 = GetWords(a1.Title + " " + a1.Description);
            var words2 = GetWords(a2.Title + " " + a2.Description);

            // Count common words
            var commonWords = words1.Intersect(words2).Count();

            // Normalize the result (other metrics can be used)
            double maxPossible = Math.Max(words1.Count, words2.Count);
            return maxPossible > 0 ? commonWords / maxPossible : 0;
        }

        /// <summary>
        /// Extracts a list of lowercase words from a given text.
        /// Splits the text by spaces and common punctuation.
        /// </summary>
        /// <param name="text">The input text to split into words.</param>
        /// <returns>A list of lowercase words extracted from the text.</returns>
        private List<string> GetWords(string text)
        {
            // Simple method for extracting words (can be improved)
            return text.Split(new[] { ' ', ',', '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries)
                       .Select(w => w.ToLower())
                       .ToList();
        }
    }
}
