using LibrarySystem.Application.Interfaces;
using LibrarySystem.Infrastructure.Scanner;
using LibrarySystem.Infrastructure.UserParser;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using System;

namespace LibrarySystem.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureServices(
            this IServiceCollection services,
            Action<InfrastructureOptions> optionsConfiguration)
        {
            var options = new InfrastructureOptions();
            optionsConfiguration?.Invoke(options);
            services.AddScoped<IIdentityCardScannerService, IdentityCardScannerService>();
            services.AddHttpClient(HttpClientName.MicroBlinkClient, client =>
            {
                client.BaseAddress = options.MbUri;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            }).AddHttpMessageHandler<ApiKeyHandler>();
            services.AddTransient<ApiKeyHandler>();
            services.AddTransient<IUserParser, UserParser.UserParser>();
            services.AddSingleton<InfrastructureOptions>(options);
        }
    }
}
