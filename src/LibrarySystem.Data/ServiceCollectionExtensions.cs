using LibrarySystem.Application.Interfaces;
using LibrarySystem.Common.Interfaces;
using LibrarySystem.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace LibrarySystem.Data
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSpecificRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }

        public static void AddGenericRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
