using Announcement.Application.Announcement.DTOs;
using Announcement.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Announcement.Application.Announcement.Query.GetById
{
    /// <summary>
    /// Handles the query to retrieve an announcement by its unique identifier.
    /// </summary>
    public class GetAnnouncementByIdQueryHandler : IRequestHandler<GetAnnouncementByIdQuery, AnnouncementDto?>
    {
        private readonly IAnnouncementRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAnnouncementByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="repository">The announcement repository.</param>
        public GetAnnouncementByIdQueryHandler(IAnnouncementRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Handles the query request to get an announcement by ID.
        /// </summary>
        /// <param name="request">The query containing the ID of the announcement.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns an <see cref="AnnouncementDto"/> if found; otherwise, <c>null</c>.
        /// </returns>
        public async Task<AnnouncementDto?> Handle(GetAnnouncementByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null) return null;

            return new AnnouncementDto(entity.Id, entity.Title, entity.Description, entity.DateAdded);
        }
    }
}
