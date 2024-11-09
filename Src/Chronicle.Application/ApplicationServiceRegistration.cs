using Chronicle.Application.Behaviors;
using Chronicle.Application.Identity.Commands.Login;
using Chronicle.Application.Options;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Chronicle.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly));

        services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));

        services.AddValidatorsFromAssemblyContaining<LoginCommandValidator>();

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

        //services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}
