using Announcement.Domain.Entities;
using Announcement.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Announcement.Application.Announcement.Commands.Create
{
    /// <summary>
    /// Handles the creation of a new announcement.
    /// </summary>
    public class CreateAnnouncementCommandHandler : IRequestHandler<CreateAnnouncementCommand, Guid>
    {
        private readonly IAnnouncementRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAnnouncementCommandHandler"/> class.
        /// </summary>
        /// <param name="repository">Announcement repository.</param>
        public CreateAnnouncementCommandHandler(IAnnouncementRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Creates a new announcement and returns its ID.
        /// </summary>
        /// <param name="request">Command with announcement data.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Created announcement's GUID.</returns>
        public async Task<Guid> Handle(CreateAnnouncementCommand request, CancellationToken cancellationToken)
        {
            var entity = new Announcements
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                DateAdded = DateTime.UtcNow
            };

            await _repository.AddAsync(entity, cancellationToken);
            return entity.Id;
        }
    }
}
