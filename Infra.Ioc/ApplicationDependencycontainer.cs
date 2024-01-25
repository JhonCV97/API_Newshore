using Application.Interfaces.Auths;
using Application.Interfaces.EncryptAndDecrypt;
using Application.Interfaces.Roles;
using Application.Interfaces.User;
using Application.Services.Auths;
using Application.Services.EncryptAndDecrypt;
using Application.Services.Roles;
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

        }
    }
}
