using Inventory.Core.Interfaces;
using Inventory.DataProvider.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Inventory.Web.App
{
    public static class StartupExtension
    {
        public static void AddBindings(this IServiceCollection services, IConfiguration config)
        {
            var httpDataProviderBaseUrl = config["HttpDataProvider:BaseUrl"];
            services.AddHttpClient("InventoryApi", c =>
            {
                c.BaseAddress = new Uri(httpDataProviderBaseUrl);
            });
            services.AddScoped<IInventoryDataProvider, HttpDataProvider>();
        }
    }
}
