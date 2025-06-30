using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sales.Application.Services;
using Sales.Application.UseCases;
using Sales.Application.Util;
using Sales.Domain.Repositories;
using Sales.Infrastructure.Persistence;
using Sales.Infrastructure.Repository;

namespace Sales.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSalesModule(this IServiceCollection services, IConfiguration configuration)
        {
            // Services
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductCategoryService, ProductCategoryService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<ITableService, TableService>();
            services.AddScoped<IProductRecipeService, ProductRecipeService>();

            // Repositories
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<ITableRepository, TableRepository>();
            services.AddScoped<IProductRecipeRepository, ProductRecipeRepository>();

            // Unit Of Work
            services.AddScoped<IUnitOfWork, SalesUnitOfWork>();

            // Validator
            //services.AddFluentValidationAutoValidation();
            //ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("en");
            //services.AddValidatorsFromAssemblyContaining<InventoryValidatorMarker>();

            // Database
            services.AddDbContext<SalesDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("PostgresConnection"));
            });

            return services;
        }

        public static IMvcBuilder AddSalesControllers(this IMvcBuilder mvcBuilder)
        {
            return mvcBuilder.AddApplicationPart(typeof(SalesModuleMarker).Assembly);
        }
    }
}
