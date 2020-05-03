using Inventory.Core.DomainModels;
using Inventory.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Web.App.Controllers
{
    //[Authorize(Roles = "Customer")]
    public class CustomerController:BaseController
    {
        private readonly IInventoryDataProvider _inventoryDataProvider;
        public CustomerController(IInventoryDataProvider inventoryDataProvider)
        {
            _inventoryDataProvider = inventoryDataProvider;
        }

        /// <summary>
        /// Groceries Action
        /// </summary>
        /// <returns>Groceries View</returns>
        [HttpGet]
        public async Task<ActionResult> Groceries()
        {
            IEnumerable<Fruit> sortedGroceries = default;

            var groceries = await _inventoryDataProvider.Retrieve();

            if (groceries.Any())
                sortedGroceries = groceries.OrderByDescending(x => x.UpdatedDate);

            return View(sortedGroceries);
        }
    }
}
