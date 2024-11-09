using Chronicle.Application;
using Chronicle.PostgrePersistence;

namespace Chronicle.Api.Configurations;

public class MainServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationServices(configuration);
        services.AddPostgrePersistenceServices(configuration);
    }
}
