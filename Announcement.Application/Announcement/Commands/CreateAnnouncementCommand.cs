using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Announcement.Application.Announcement.Commands
{
    public record CreateAnnouncementCommand(string Title, string Description) : IRequest<Guid>;
}
