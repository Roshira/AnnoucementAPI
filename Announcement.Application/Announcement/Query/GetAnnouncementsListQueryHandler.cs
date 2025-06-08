using Announcement.Application.Announcement.DTOs;
using Announcement.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Announcement.Application.Announcement.Query
{
    public class GetAnnouncementsListQueryHandler : IRequestHandler<GetAnnouncementsListQuery, List<AnnouncementDto>>
    {
        private readonly IAnnouncementRepository _repository;

        public GetAnnouncementsListQueryHandler(IAnnouncementRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<AnnouncementDto>> Handle(GetAnnouncementsListQuery request, CancellationToken cancellationToken)
        {
            var announcements = await _repository.GetAllAsync(cancellationToken);

            return announcements
                .Select(a => new AnnouncementDto(a.Id, a.Title, a.Description, a.DateAdded))
                .ToList();
        }
    }
}
