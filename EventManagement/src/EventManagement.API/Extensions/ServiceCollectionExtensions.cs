using EventManagement.Application.Interfaces;
using EventManagement.Application.Services;
using EventManagement.Domain.Interfaces;
using EventManagement.Infrastructure.Data;
using EventManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("PostgeSQL");

        services.AddDbContext<EventManagementDbContext>(options =>
            options.UseNpgsql(connectionString));

        // Repositories
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IParticipantRepository, ParticipantRepository>();

        // Services
        services.AddScoped<IEventService, EventService>();
        services.AddScoped<IParticipantService, ParticipantService>();

        return services;
    }

}
