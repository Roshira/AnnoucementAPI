using Announcement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Announcement.Domain.Interfaces
{
    public interface IAnnouncementRepository
    {
        Task AddAsync(Announcements announcement, CancellationToken cancellationToken);
        Task<List<Announcements>> GetAllAsync(CancellationToken cancellationToken);
    }
}
