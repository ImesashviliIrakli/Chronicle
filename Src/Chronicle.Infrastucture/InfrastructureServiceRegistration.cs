using Chronicle.Application.Interfaces;
using Chronicle.Infrastucture.Implementations;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Chronicle.Infrastucture;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var googleClientId = configuration["GoogleSettings:ClientId"] ?? throw new ArgumentNullException(nameof(configuration));
        var googleClientSecret = configuration["GoogleSettings:ClientSecret"] ?? throw new ArgumentNullException(nameof(configuration));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })
        .AddCookie()
        .AddGoogle(options =>
        {
            options.ClientId = googleClientId;
            options.ClientSecret = googleClientSecret;
        });

        services.AddScoped<IUserContext, UserContext>();

        return services;
    }
}
