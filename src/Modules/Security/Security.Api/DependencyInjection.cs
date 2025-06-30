using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Security.Application.Auth;
using Security.Application.Services;
using Security.Application.UseCases;
using Security.Application.Util;
using Security.Application.Validators;
using Security.Domain.Repositories;
using Security.Infrastructure.Persistence;
using Security.Infrastructure.Repositories;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using System.Globalization;

namespace Security.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSecurityModule(this IServiceCollection services, IConfiguration configuration)
        {
            // Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IPermissionService, PermissionService>();

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();

            // Unit Of Work
            services.AddScoped<IUnitOfWork, SecurityUnitOfWork>();

            // Authentication
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            // Validator
            services.AddFluentValidationAutoValidation();
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("en");
            services.AddValidatorsFromAssemblyContaining<SecurityValidatorMarker>();

            // Database
            services.AddDbContext<SecurityDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("PostgresConnection"));
            });

            return services; 
        }

        public static IMvcBuilder AddSecurityControllers(this IMvcBuilder mvcBuilder)
        {
            return mvcBuilder.AddApplicationPart(typeof(SecurityModuleMarker).Assembly);
        }
    }
}
