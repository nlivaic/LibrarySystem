using Microsoft.Extensions.DependencyInjection;
using System;

namespace LibrarySystem.Application.Services.Sorting
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPropertyMappingService(
            this IServiceCollection services,
            Action<PropertyMappingOptions> configuration)
        {
            var options = new PropertyMappingOptions();
            configuration?.Invoke(options);
            services.AddSingleton<IPropertyMappingService>(_ => new PropertyMappingService(options));
        }
    }
}
