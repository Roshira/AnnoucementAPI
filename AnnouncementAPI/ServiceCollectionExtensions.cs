using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace AnnouncementAPI
{
    public static class ServiceCollectionExtensions
    {
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

            return services;
        }
    }
}
