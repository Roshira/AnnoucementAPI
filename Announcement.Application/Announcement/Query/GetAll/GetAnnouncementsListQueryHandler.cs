using Announcement.Application.Announcement.DTOs;
using Announcement.Domain.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Announcement.Application.Announcement.Query.GetAll
{
    /// <summary>
    /// Handles the query to retrieve a list of all announcements.
    /// </summary>
    public class GetAnnouncementsListQueryHandler : IRequestHandler<GetAnnouncementsListQuery, List<AnnouncementDto>>
    {
        private readonly IAnnouncementRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAnnouncementsListQueryHandler"/> class.
        /// </summary>
        /// <param name="repository">The announcement repository.</param>
        public GetAnnouncementsListQueryHandler(IAnnouncementRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Handles the request to get all announcements.
        /// </summary>
        /// <param name="request">The query request.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>A list of announcements as <see cref="AnnouncementDto"/> objects.</returns>
        public async Task<List<AnnouncementDto>> Handle(GetAnnouncementsListQuery request, CancellationToken cancellationToken)
        {
            var announcements = await _repository.GetAllAsync(cancellationToken);

            return announcements
                .Select(a => new AnnouncementDto(a.Id, a.Title, a.Description, a.DateAdded))
                .ToList();
        }
    }
}
