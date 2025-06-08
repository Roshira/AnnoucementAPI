using System;

namespace Announcement.Domain.Entities
{
    /// <summary>
    /// Represents an announcement entity with a unique identifier, title, description, and creation date.
    /// </summary>
    public class Announcements
    {
        /// <summary>
        /// Gets or sets the unique identifier of the announcement.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the announcement.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the detailed description of the announcement.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time when the announcement was added.
        /// </summary>
        public DateTime DateAdded { get; set; }
    }
}
