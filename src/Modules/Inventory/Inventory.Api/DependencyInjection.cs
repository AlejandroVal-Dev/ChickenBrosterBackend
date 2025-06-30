using Inventory.Application.Services;
using Inventory.Application.UseCases;
using Inventory.Application.Util;
using Inventory.Domain.Repositories;
using Inventory.Infrastructure.Persistence;
using Inventory.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInventoryModule(this IServiceCollection services, IConfiguration configuration)
        {
            // Services
            services.AddScoped<IIngredientService, IngredientService>();
            services.AddScoped<IInventoryService, InventoryService>();
            services.AddScoped<IIngredientCategoryService, IngredientCategoryService>();
            services.AddScoped<IRecipeService, RecipeService>();
            services.AddScoped<IUnitOfMeasureService, UnitOfMeasureService>();
            services.AddScoped<IInventoryMovementService, InventoryMovementService>();

            // Repositories
            services.AddScoped<IIngredientRepository, IngredientRepository>();
            services.AddScoped<IInventoryRepository, InventoryRepository>();
            services.AddScoped<IIngredientCategoryRepository, IngredientCategoryRepository>();
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<IUnitOfMeasureRepository, UnitOfMeasureRepository>();
            services.AddScoped<IInventoryMovementRepository, InventoryMovementRepository>();

            // Unit Of Work
            services.AddScoped<IUnitOfWork, InventoryUnitOfWork>();

            // Validator
            //services.AddFluentValidationAutoValidation();
            //ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("en");
            //services.AddValidatorsFromAssemblyContaining<InventoryValidatorMarker>();

            // Database
            services.AddDbContext<InventoryDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("PostgresConnection"));
            });

            return services;
        }

        public static IMvcBuilder AddInventoryControllers(this IMvcBuilder mvcBuilder)
        {
            return mvcBuilder.AddApplicationPart(typeof(InventoryModuleMarker).Assembly);
        }
    }
}
