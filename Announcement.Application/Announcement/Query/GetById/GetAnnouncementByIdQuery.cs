using Announcement.Application.Announcement.DTOs;
using MediatR;
using System;

namespace Announcement.Application.Announcement.Query.GetById
{
    /// <summary>
    /// Represents a query to get a single announcement by its unique identifier.
    /// </summary>
    public class GetAnnouncementByIdQuery : IRequest<AnnouncementDto?>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the announcement to retrieve.
        /// </summary>
        public Guid Id { get; set; } // or int, depending on your ID type
    }
}
