using Announcement.Application.Announcement.DTOs;
using MediatR;
using System;
using System.Collections.Generic;

namespace Announcement.Application.Announcement.Queries.GetSimilarAnnouncements
{
    /// <summary>
    /// Represents a query to retrieve a list of announcements similar to a specified announcement.
    /// </summary>
    public class GetSimilarAnnouncementsQuery : IRequest<List<AnnouncementDto>>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the announcement to find similarities for.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the number of similar announcements to return. Defaults to 3.
        /// </summary>
        public int Count { get; set; } = 3;
    }
}
