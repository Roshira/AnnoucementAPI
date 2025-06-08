using Announcement.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Announcement.Application.Announcement.Commands.Edit
{
    /// <summary>
    /// Handles the command to edit an existing announcement.
    /// </summary>
    public class EditAnnouncementCommandHandler : IRequestHandler<EditAnnouncementCommand, bool>
    {
        private readonly IAnnouncementRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditAnnouncementCommandHandler"/> class.
        /// </summary>
        /// <param name="repository">The announcement repository.</param>
        public EditAnnouncementCommandHandler(IAnnouncementRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Handles the edit announcement command.
        /// </summary>
        /// <param name="request">The edit announcement command containing updated data.</param>
        /// <param name="cancellationToken">Token to observe cancellation requests.</param>
        /// <returns>
        /// Returns <c>true</c> if the announcement was found and updated successfully; otherwise, <c>false</c>.
        /// </returns>
        public async Task<bool> Handle(EditAnnouncementCommand request, CancellationToken cancellationToken)
        {
            var announcement = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (announcement is null) return false;

            announcement.Title = request.Title;
            announcement.Description = request.Description;

            await _repository.UpdateAsync(announcement, cancellationToken);
            return true;
        }
    }
}
