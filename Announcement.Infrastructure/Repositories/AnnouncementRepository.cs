using Announcement.Domain.Entities;
using Announcement.Domain.Interfaces;
using Announcement.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Announcement.Infrastructure.Repositories
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly AppDbContext _context;

        public AnnouncementRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Announcements announcement, CancellationToken cancellationToken)
        {
            _context.Announcements.Add(announcement);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Announcements>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Announcements.ToListAsync(cancellationToken);
        }
    }


}
