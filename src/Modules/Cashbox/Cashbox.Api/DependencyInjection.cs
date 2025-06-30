using Cashbox.Application.Services;
using Cashbox.Application.UseCases;
using Cashbox.Application.Util;
using Cashbox.Domain.Repositories;
using Cashbox.Infrastructure.Persistence;
using Cashbox.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cashbox.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCashboxModule(this IServiceCollection services, IConfiguration configuration)
        {
            // Services
            services.AddScoped<ICashRegisterService, CashRegisterService>();

            // Repositories
            services.AddScoped<ICashRegisterSessionRepository, CashRegisterSessionRepository>();
            services.AddScoped<ICashRegisterMovementRepository, CashRegisterMovementRepository>();
            services.AddScoped<ICashRegisterSessionOrderRepository, CashRegisterSessionOrderRepository>();

            // Unit Of Work
            services.AddScoped<IUnitOfWork, CashboxUnitOfWork>();

            // Database
            services.AddDbContext<CashboxDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("PostgresConnection"));
            });

            return services;
        }

        public static IMvcBuilder AddCashboxControllers(this IMvcBuilder mvcBuilder)
        {
            return mvcBuilder.AddApplicationPart(typeof(CashboxModuleMarker).Assembly);
        }
    }
}
