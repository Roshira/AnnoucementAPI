using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Announcement.Application.Announcement.Queries.GetSimilarAnnouncements;
using Announcement.Domain.Interfaces;
using Announcement.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Announcement.Infrastructure
{
    /// <summary>
    /// Provides extension methods to register infrastructure services.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds infrastructure services, such as repositories and similarity calculators, to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The service collection to add infrastructure services to.</param>
        /// <returns>The updated <see cref="IServiceCollection"/> with infrastructure services added.</returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // Register the announcement repository and similarity calculator with scoped lifetime
            services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();
            services.AddScoped<IAnnouncementSimilarityCalculator, AnnouncementSimilarityCalculator>();

            return services;
        }
    }
}
