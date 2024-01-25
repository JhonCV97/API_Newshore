using Application.Interfaces.Auths;
using Application.Interfaces.EncryptAndDecrypt;
using Application.Interfaces.Journey;
using Application.Interfaces.Roles;
using Application.Interfaces.SearchAlgorithm;
using Application.Interfaces.User;
using Application.Services.Auths;
using Application.Services.EncryptAndDecrypt;
using Application.Services.Journey;
using Application.Services.Roles;
using Application.Services.SearchAlgorithm;
using Application.Services.User;
using Domain.Interfaces;
using Infra.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Ioc
{
    public static class ApplicationDependencycontainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            // Auth
            services.AddScoped<IAuthService, AuthService>();
            // User
            services.AddScoped<IUserService, UserService>();
            // Role
            services.AddScoped<IRolesService, RolesService>();
            // Password
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            // EncryptAndDecrypt
            services.AddScoped<IEncryptAndDecryptService, EncryptAndDecryptService>();
            //JourneyService
            services.AddScoped<IJourneyService, JourneyService>();
            //SearchShortRouteService
            services.AddScoped<ISearchShortRouteService, SearchShortRouteService>();
            //SearchLongestRouteService
            services.AddScoped<ISearchLongestRouteService, SearchLongestRouteService>();

        }
    }
}
