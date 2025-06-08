using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Announcement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Announcement.Persistence.Data
{
    /// <summary>
    /// Represents the Entity Framework database context for the Announcement application.
    /// Manages the entity sets and database mappings.
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext"/> class with specified options.
        /// </summary>
        /// <param name="options">The options to configure the context.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> representing the announcements table.
        /// </summary>
        public DbSet<Announcements> Announcements { get; set; }

        /// <summary>
        /// Configures the entity mappings for the context using the model builder.
        /// Applies configurations from the current assembly.
        /// </summary>
        /// <param name="modelBuilder">The builder used to construct the model for the context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
