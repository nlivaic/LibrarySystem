using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace LibrarySystem.Application
{
    public static class ServiceCollectionExtensions
    {
        public static void AddLibrarySystemApplicationHandlers(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ServiceCollectionExtensions).Assembly);
        }
    }
}
