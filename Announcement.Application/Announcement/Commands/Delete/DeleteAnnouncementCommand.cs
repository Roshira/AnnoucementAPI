using MediatR;
using System;

namespace Announcement.Application.Announcement.Commands.Delete
{
    /// <summary>
    /// Command to delete an announcement by its ID.
    /// </summary>
    public class DeleteAnnouncementCommand : IRequest<bool>
    {
        /// <summary>
        /// ID of the announcement to delete.
        /// </summary>
        public Guid Id { get; set; }
    }
}
