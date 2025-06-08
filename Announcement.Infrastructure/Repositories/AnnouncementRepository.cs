using Announcement.Domain.Entities;
using Announcement.Domain.Interfaces;
using Announcement.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Announcement.Infrastructure.Repositories
{
    /// <summary>
    /// Repository implementation for managing <see cref="Announcements"/> entities.
    /// Provides CRUD operations using Entity Framework Core.
    /// </summary>
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnnouncementRepository"/> class.
        /// </summary>
        /// <param name="context">The database context used for data access.</param>
        public AnnouncementRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a new announcement to the database asynchronously.
        /// </summary>
        /// <param name="announcement">The announcement entity to add.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task AddAsync(Announcements announcement, CancellationToken cancellationToken)
        {
            _context.Announcements.Add(announcement);
            await _context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Retrieves all announcements from the database asynchronously.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>A task that returns a list of all announcements.</returns>
        public async Task<List<Announcements>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Announcements.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Retrieves an announcement by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the announcement.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>A task that returns the announcement if found; otherwise, null.</returns>
        public async Task<Announcements?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Announcements.FindAsync(new object[] { id }, cancellationToken);
        }

        /// <summary>
        /// Updates an existing announcement in the database asynchronously.
        /// </summary>
        /// <param name="announcement">The announcement entity with updated values.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task UpdateAsync(Announcements announcement, CancellationToken cancellationToken)
        {
            _context.Announcements.Update(announcement);
            await _context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Deletes an announcement from the database asynchronously.
        /// </summary>
        /// <param name="announcement">The announcement entity to delete.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task DeleteAsync(Announcements announcement, CancellationToken cancellationToken)
        {
            _context.Announcements.Remove(announcement);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
