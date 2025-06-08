using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Announcement.Application.Announcement.DTOs
{
    public record AnnouncementDto(Guid Id, string Title, string Description, DateTime DateAdded);
}
