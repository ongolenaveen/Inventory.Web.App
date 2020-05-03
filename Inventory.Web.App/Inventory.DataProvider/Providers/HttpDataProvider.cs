using CsvHelper;
using Inventory.Core.DomainModels;
using Inventory.Core.Interfaces;
using Inventory.DataProvider.Models.CsvMappers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Inventory.DataProvider.Providers
{
    /// <summary>
    /// Http Data Provider
    /// </summary>
    public class HttpDataProvider : IInventoryDataProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;

        /// <summary>
        /// Constructor for HttpDataProvider
        /// </summary>
        /// <param name="httpClientFactory">Http Client Factory</param>
        public HttpDataProvider(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Retrieve Groceries
        /// </summary>
        /// <returns>Groceries</returns>
        public async Task<IEnumerable<Fruit>> Retrieve()
        {
            IEnumerable<Fruit> groceries = default;
            using (var client = _httpClientFactory.CreateClient("InventoryApi"))
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, "api/groceries"))
                {
                    var response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var resposeString = await response.Content.ReadAsStringAsync();
                        groceries = JsonConvert.DeserializeObject<IEnumerable<Fruit>>(resposeString);
                    }
                }
            }
            return groceries;
        }

        /// <summary>
        /// Upload Inventory File
        /// </summary>
        /// <param name="inventoryfile">Inventory File</param>
        /// <returns>Task</returns>
        public async Task Upload(InventoryFile inventoryfile)
        {
            using (var client = _httpClientFactory.CreateClient("InventoryApi"))
            {
                await client.PostAsJsonAsync("api/groceries/upload", inventoryfile);
            }
        }

        /// <summary>
        /// Download Inventory File
        /// </summary>
        /// <returns>Inventory File as Byte Stream</returns>
        public async Task<byte[]> Download()
        {
            byte[] csvFile = default;

            // Get the List of Groceries Avilable from the Inventory
            var groceries = await Retrieve();
           
            // Convert the groceries to Byte stream and send them back
            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new StreamWriter(memoryStream))
                {
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.Configuration.RegisterClassMap<FruitMap>();
                        csv.WriteRecords(groceries);
                        csvFile = memoryStream.ToArray();
                    }
                }
            }
            // Send the Byte Stream Back
            return csvFile;
        }
    }
}
