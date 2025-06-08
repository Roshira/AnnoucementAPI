using MediatR;
using System;

namespace Announcement.Application.Announcement.Commands.Create
{
    /// <summary>
    /// Command to create a new announcement with a title and description.
    /// Returns the GUID of the created announcement.
    /// </summary>
    /// <param name="Title">Title of the announcement.</param>
    /// <param name="Description">Description of the announcement.</param>
    public record CreateAnnouncementCommand(string Title, string Description) : IRequest<Guid>;
}
