using Announcement.Application.Announcement.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Announcement.Application.Announcement.Query
{
    public record GetAnnouncementsListQuery() : IRequest<List<AnnouncementDto>>;
}
