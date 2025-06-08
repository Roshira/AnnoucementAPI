using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Announcement.Domain.Interfaces;
using Announcement.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Announcement.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // Add support for MVC controllers
            services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();
         

            return services;
        }
    }
}
