using Domain.Interfaces;
using Infra.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Ioc
{
    public static class InfraDependencycontainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IAuthRepository, AuthRepository>();

            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();

        }
    }
}
