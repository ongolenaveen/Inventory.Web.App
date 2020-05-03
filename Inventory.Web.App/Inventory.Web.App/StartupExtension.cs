using Inventory.Core.Interfaces;
using Inventory.DataProvider.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Inventory.Web.App
{
    /// <summary>
    /// Start up Extension Class
    /// </summary>
    public static class StartupExtension
    {
        /// <summary>
        /// Add Bindings to Services
        /// </summary>
        /// <param name="services">Service Collection</param>
        /// <param name="config">Configuration</param>
        public static void AddBindings(this IServiceCollection services, IConfiguration config)
        {
            // Read Http Data provider configuration from appsettings.json
            var httpDataProviderBaseUrl = config["HttpDataProvider:BaseUrl"];

            // Add Http Client Factory
            services.AddHttpClient("InventoryApi", c =>
            {
                c.BaseAddress = new Uri(httpDataProviderBaseUrl);
            });

            // Bind Services
            services.AddScoped<IInventoryDataProvider, MockDataProvider>();
        }
    }
}
