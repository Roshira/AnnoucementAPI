using Announcement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Announcement.Domain.Interfaces
{
    /// <summary>
    /// Defines methods for managing <see cref="Announcements"/> entities in a data store.
    /// </summary>
    public interface IAnnouncementRepository
    {
        /// <summary>
        /// Adds a new announcement asynchronously.
        /// </summary>
        /// <param name="announcement">The announcement entity to add.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>A task representing the asynchronous add operation.</returns>
        Task AddAsync(Announcements announcement, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves all announcements asynchronously.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>A task that returns a list of all announcements.</returns>
        Task<List<Announcements>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves an announcement by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the announcement.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>A task that returns the announcement if found; otherwise, null.</returns>
        Task<Announcements?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Updates an existing announcement asynchronously.
        /// </summary>
        /// <param name="announcement">The announcement entity with updated data.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>A task representing the asynchronous update operation.</returns>
        Task UpdateAsync(Announcements announcement, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes an announcement asynchronously.
        /// </summary>
        /// <param name="announcement">The announcement entity to delete.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>A task representing the asynchronous delete operation.</returns>
        Task DeleteAsync(Announcements announcement, CancellationToken cancellationToken);
    }
}
