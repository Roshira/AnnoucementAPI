using Announcement.Application.Announcement.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Announcement.Application.Announcement.Query.GetAll
{
    /// <summary>
    /// Represents a query to retrieve a list of all announcements.
    /// </summary>
    public record GetAnnouncementsListQuery() : IRequest<List<AnnouncementDto>>;
}
