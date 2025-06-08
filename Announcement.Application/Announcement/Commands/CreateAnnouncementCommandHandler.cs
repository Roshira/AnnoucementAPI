using Announcement.Domain.Entities;
using Announcement.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Announcement.Application.Announcement.Commands
{
    public class CreateAnnouncementCommandHandler : IRequestHandler<CreateAnnouncementCommand, Guid>
    {
        private readonly IAnnouncementRepository _repository;

 public CreateAnnouncementCommandHandler(IAnnouncementRepository repository)
        {
            _repository = repository;
        }

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
