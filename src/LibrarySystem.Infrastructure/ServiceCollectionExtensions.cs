using LibrarySystem.Application.Interfaces;
using LibrarySystem.Infrastructure.Scanner;
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
            services.AddScoped<IScannerService, IdentityCardScannerService>();
            services.AddHttpClient(HttpClientName.MicroBlinkClient, client =>
            {
                client.BaseAddress = options.MbUri;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            }).AddHttpMessageHandler<ApiKeyHandler>();
            services.AddTransient<ApiKeyHandler>();
            services.AddTransient<IUserParser, UserParser>();
            services.AddSingleton(options);
            services.AddScoped<ICheckDigitService, Td1CheckDigitService>();
        }
    }
}
