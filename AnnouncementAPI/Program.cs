using AnnouncementAPI;
using Microsoft.Extensions.DependencyInjection;
using Announcement.Infrastructure;
using Announcement.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddApi()
    .AddInfrastructure()
    .AddPersistence();// Adds controllers, Swagger (without JWT) etc. from Web layer


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
