using Inventory.Core.DomainModels;
using Inventory.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Inventory.Web.App.Controllers
{
    /// <summary>
    /// Customers Controller
    /// </summary>
    //[Authorize(Roles = "Customer")]
    public class CustomerController:BaseController
    {
        private readonly IInventoryDataProvider _inventoryDataProvider;

        /// <summary>
        /// Constructor for Customers Controller
        /// </summary>
        /// <param name="inventoryDataProvider">Inventory Data Provider</param>
        public CustomerController(IInventoryDataProvider inventoryDataProvider)
        {
            _inventoryDataProvider = inventoryDataProvider?? throw new ArgumentNullException(nameof(inventoryDataProvider));
        }

        /// <summary>
        /// Groceries Action
        /// </summary>
        /// <returns>Groceries View</returns>
        [HttpGet]
        public async Task<ActionResult> Groceries()
        {
            IEnumerable<Fruit> sortedGroceries = default;

            // Get the Groceries from Inventory
            var groceries = await _inventoryDataProvider.Retrieve();

            // If there exists Groceries, sort them by descending order
            if (groceries.Any())
                sortedGroceries = groceries.OrderByDescending(x => x.UpdatedDate);

            // Send the sorted Groceries to view
            return View(sortedGroceries);
        }
    }
}
