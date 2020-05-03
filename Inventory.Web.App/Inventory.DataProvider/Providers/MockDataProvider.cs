using CsvHelper;
using Inventory.Core.DomainModels;
using Inventory.Core.Interfaces;
using Inventory.DataProvider.Models.CsvMappers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace Inventory.DataProvider.Providers
{
    public class MockDataProvider: IInventoryDataProvider
    {
        /// <summary>
        /// Retrieve Groceries
        /// </summary>
        /// <returns>Groceries</returns>
        public async Task<IEnumerable<Fruit>> Retrieve()
        {
            var fruits = new List<Fruit>{
                new Fruit{ Name = "banana", Price=0.29m,QuantityInStock =20,UpdatedDate = new DateTime(2014,4,11) },
                new Fruit{ Name = "honeydew melon", Price=1.01m,QuantityInStock =3,UpdatedDate = new DateTime(2014,4,29) },
                new Fruit{ Name = "watermelon", Price=1.54m,QuantityInStock =4,UpdatedDate = new DateTime(2014,4,30)},
                new Fruit{ Name = "apple", Price=0.41m,QuantityInStock =241,UpdatedDate = new DateTime(2014,3,11)},
                new Fruit{ Name = "pear", Price=0.64m,QuantityInStock =100,UpdatedDate = new DateTime(2014,3,14)},
                new Fruit{ Name = "kumquat", Price=2.04m,QuantityInStock =1,UpdatedDate = new DateTime(2014,7,14)},
            };

            return await Task.FromResult(fruits);
        }

        /// <summary>
        /// Upload Inventory File
        /// </summary>
        /// <param name="inventoryfile">Inventory File</param>
        /// <returns>Task</returns>
        public async Task Upload(InventoryFile inventoryfile)
        {
            await Task.CompletedTask;
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
