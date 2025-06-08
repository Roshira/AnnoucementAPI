using MediatR;
using System;

namespace Announcement.Application.Announcement.Commands.Edit
{
    /// <summary>
    /// Command to edit an existing announcement.
    /// </summary>
    public class EditAnnouncementCommand : IRequest<bool>
    {
        /// <summary>Announcement ID.</summary>
        public Guid Id { get; set; }

        /// <summary>New title.</summary>
        public string Title { get; set; } = null!;

        /// <summary>New description.</summary>
        public string Description { get; set; } = null!;
    }
}
