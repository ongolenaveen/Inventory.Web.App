using Inventory.Core.DomainModels;
using Inventory.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController:BaseController
    {
        private readonly IInventoryDataProvider _inventoryDataProvider;
        public AdminController(IInventoryDataProvider inventoryDataProvider)
        {
            _inventoryDataProvider = inventoryDataProvider;
        }

        /// <summary>
        /// Upload File
        /// </summary>
        /// <returns></returns>
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
            var uploadedFile = files.First();
            var inventoryFile = new InventoryFile { Name = uploadedFile.FileName };
            using (var memoryStream = new MemoryStream())
            {
                using(var sourceStream = uploadedFile.OpenReadStream())
                {
                    sourceStream.CopyTo(memoryStream);
                    inventoryFile.Content = memoryStream.ToArray();
                }
            }
            await _inventoryDataProvider.Upload(inventoryFile);
            return RedirectToAction("Groceries");
        }

        /// <summary>
        /// Groceries Action
        /// </summary>
        /// <returns>Groceries View</returns>
        [HttpGet]
        public async Task<ActionResult> Groceries()
        {
            var groceries = await _inventoryDataProvider.Retrieve();
            return View(groceries);
        }
    }
}
