using InventoryBook.BLL.Services.Implementations;
using InventoryBook.BLL.Services.Interfaces;
using InventoryBook.DAL.Repositories.Implementations;
using InventoryBook.DAL.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryBook.DI
{
    public static class IoC
    {
        public static void AddServiceDependency(this IServiceCollection services)
        {
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ICryptographerService, CryptographerService>();
            services.AddTransient<IItemService, ItemService>();
            services.AddTransient<IUserService, UserService>();

            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IItemRepository, ItemRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}
