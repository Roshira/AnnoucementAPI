using Announcement.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Announcement.Application.Announcement.Commands.Delete
{
    /// <summary>
    /// Handles the command to delete an announcement by ID.
    /// </summary>
    public class DeleteAnnouncementCommandHandler : IRequestHandler<DeleteAnnouncementCommand, bool>
    {
        private readonly IAnnouncementRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteAnnouncementCommandHandler"/> class.
        /// </summary>
        /// <param name="repository">Announcement repository.</param>
        public DeleteAnnouncementCommandHandler(IAnnouncementRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Handles deleting the announcement.
        /// </summary>
        /// <param name="request">Delete command with the announcement ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if deleted; otherwise, false.</returns>
        public async Task<bool> Handle(DeleteAnnouncementCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity is null) return false;

            await _repository.DeleteAsync(entity, cancellationToken);
            return true;
        }
    }
}
