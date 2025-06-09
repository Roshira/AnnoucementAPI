using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace AnnouncementAPI
{
    /// <summary>
    /// Provides extension methods to register services for the Announcement API.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds API-related services to the specified <see cref="IServiceCollection"/>, including MVC controllers and Swagger/OpenAPI support.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to which the services will be added.</param>
        /// <returns>The updated <see cref="IServiceCollection"/> with the API services added.</returns>
        /// <remarks>
        /// This method registers MVC controllers, configures Swagger/OpenAPI generation, and adds endpoint API explorer support.
        /// </remarks>
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            // Add support for MVC controllers
            services.AddControllers();
            services.AddSwaggerGen();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            services.AddEndpointsApiExplorer();

            // Add support for Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Announcement API",
                    Version = "v1"
                });
            });
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    // WARNING: AllowAnyOrigin() is NOT recommended in production
                    // Instead, use .WithOrigins() and specify trusted frontend domains

                    policy
                        .WithOrigins(
                            "http://localhost:5173"
                        )
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials(); // Required if frontend is on a different domain
                });
            });
            return services;
        }
    }
}
