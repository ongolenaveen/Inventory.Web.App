using Inventory.Core.DomainModels;
using Inventory.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Web.App.Controllers
{
    /// <summary>
    /// Admin Controller
    /// </summary>
    //[Authorize(Roles ="Admin")]
    public class AdminController:BaseController
    {
        private readonly IInventoryDataProvider _inventoryDataProvider;

        /// <summary>
        /// Constructor For Admin Controller
        /// </summary>
        /// <param name="inventoryDataProvider">Inventory Data Provider</param>
        public AdminController(IInventoryDataProvider inventoryDataProvider)
        {
            _inventoryDataProvider = inventoryDataProvider?? throw new ArgumentNullException(nameof(inventoryDataProvider));
        }

        /// <summary>
        /// Upload File
        /// </summary>
        /// <returns>Action Result</returns>
        [HttpGet]
        public ActionResult Upload()
        {
            return View();
        }

        /// <summary>
        /// Upload File
        /// </summary>
        /// <param name="files">Uploaded Files</param>
        /// <returns>Redirect to Groceries View</returns>
        [HttpPost]
        public async Task<ActionResult> Upload(List<IFormFile> files)
        {
            // Validate the received request
            if (files == null || !files.Any())
                return BadRequest();

            var uploadedFile = files.First();
            var inventoryFile = new InventoryFile { Name = uploadedFile.FileName };
            // Get the content of the uploded file
            using (var memoryStream = new MemoryStream())
            {
                using(var sourceStream = uploadedFile.OpenReadStream())
                {
                    sourceStream.CopyTo(memoryStream);
                    inventoryFile.Content = memoryStream.ToArray();
                }
            }

            // Upload the file
            await _inventoryDataProvider.Upload(inventoryFile);

            // Redirect the user to Groceries
            return RedirectToAction("Groceries");
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

        /// <summary>
        /// Download Inventory File
        /// </summary>
        /// <returns>File Result</returns>
        [HttpGet]
        public async Task<ActionResult> Download()
        {
            var csvFile = await _inventoryDataProvider.Download();

            return File(csvFile, "text/csv","Inventory.csv");
        }
    }
}
