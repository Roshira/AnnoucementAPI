using System;

namespace Announcement.Application.Announcement.DTOs
{
    /// <summary>
    /// Data Transfer Object representing an announcement.
    /// </summary>
    /// <param name="Id">Unique identifier of the announcement.</param>
    /// <param name="Title">Title of the announcement.</param>
    /// <param name="Description">Description or content of the announcement.</param>
    /// <param name="DateAdded">The date and time when the announcement was added.</param>
    public record AnnouncementDto(Guid Id, string Title, string Description, DateTime DateAdded);
}
