using Inventory.Core.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.Core.Interfaces
{
    public interface IInventoryDataProvider
    {
        /// <summary>
        /// Upload Inventory File into Database
        /// </summary>
        /// <param name="inventoryfile">Inventory File</param>
        /// <returns>Task</returns>
        Task Upload(InventoryFile inventoryfile);

        /// <summary>
        /// Retrieve Groceries from Inventory
        /// </summary>
        /// <returns>Collection of Groceries</returns>
        Task<IEnumerable<Fruit>> Retrieve();

        /// <summary>
        /// Download Inventory File
        /// </summary>
        /// <returns>Inventory File as Byte Stream</returns>
        Task<byte[]> Download();
    }
}
