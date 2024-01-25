using Infra.Data.Context;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Ioc
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {

            services.AddScoped<AplicationDBContext>();

            //Application Layer.
            ApplicationDependencycontainer.RegisterServices(services);

            // Infra.Data Layer
            InfraDependencycontainer.RegisterServices(services);


        }
    }
}
