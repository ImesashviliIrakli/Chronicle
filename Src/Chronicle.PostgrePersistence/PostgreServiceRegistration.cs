﻿using Chronicle.Domain.Entities;
using Chronicle.Domain.Repositories;
using Chronicle.PostgrePersistence.Data;
using Chronicle.PostgrePersistence.Implementations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Chronicle.PostgrePersistence;

public static class PostgreServiceRegistration
{
    public static IServiceCollection AddPostgrePersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AuthDbContext>(options =>
           options.UseNpgsql(configuration.GetConnectionString("POSTGRES_CONNECTION_STRING")));

        services.AddIdentityCore<ApplicationUser>()
            .AddSignInManager()
            .AddEntityFrameworkStores<AuthDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<IAuthUnitOfWork, AuthUnitOfWork>();

        return services;
    }
}
