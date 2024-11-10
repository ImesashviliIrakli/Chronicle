using Chronicle.Application;
using Chronicle.PostgrePersistence;
using Chronicle.Infrastucture;
namespace Chronicle.Api.Configurations;

public class MainServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationServices(configuration);
        services.AddPostgrePersistenceServices(configuration);
        services.AddInfrastructureServices(configuration);
    }
}
