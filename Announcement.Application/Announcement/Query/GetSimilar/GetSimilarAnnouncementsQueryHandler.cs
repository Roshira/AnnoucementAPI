using Announcement.Application.Announcement.DTOs;
using Announcement.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Announcement.Application.Announcement.Queries.GetSimilarAnnouncements
{
    /// <summary>
    /// Handles the query to get a list of announcements similar to a specified announcement.
    /// </summary>
    public class GetSimilarAnnouncementsQueryHandler : IRequestHandler<GetSimilarAnnouncementsQuery, List<AnnouncementDto>>
    {
        private readonly IAnnouncementRepository _repository;
        private const double SimilarityThreshold = 0.1;
        /// <summary>
        /// Initializes a new instance of the <see cref="GetSimilarAnnouncementsQueryHandler"/> class.
        /// </summary>
        /// <param name="repository">The announcement repository.</param>
        public GetSimilarAnnouncementsQueryHandler(IAnnouncementRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Handles the <see cref="GetSimilarAnnouncementsQuery"/> request.
        /// Retrieves announcements similar to the one specified by the query.
        /// </summary>
        /// <param name="request">The query containing the announcement ID and count of similar announcements to return.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>A list of similar announcements as <see cref="AnnouncementDto"/>.</returns>
        public async Task<List<AnnouncementDto>> Handle(GetSimilarAnnouncementsQuery request, CancellationToken cancellationToken)
        {
            var targetAnnouncement = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (targetAnnouncement == null)
                return new List<AnnouncementDto>();

            var allAnnouncements = await _repository.GetAllAsync(cancellationToken);
            var otherAnnouncements = allAnnouncements.Where(a => a.Id != request.Id).ToList();

            var similarityCalculator = new AnnouncementSimilarityCalculator();
            var announcementsWithScores = otherAnnouncements
      .Select(a => new
      {
          Announcement = a,
          SimilarityScore = similarityCalculator.CalculateSimilarity(targetAnnouncement, a)
      })
      .Where(x => x.SimilarityScore >= SimilarityThreshold) // фільтрація
      .OrderByDescending(x => x.SimilarityScore)
      .Take(request.Count)
      .Select(x => new AnnouncementDto(
          x.Announcement.Id,
          x.Announcement.Title,
          x.Announcement.Description,
          x.Announcement.DateAdded))
      .ToList();


            return announcementsWithScores;
        }
    }
}
