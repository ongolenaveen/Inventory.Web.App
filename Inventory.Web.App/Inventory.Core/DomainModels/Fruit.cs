using System;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Core.DomainModels
{
    /// <summary>
    /// Fruit Domain Model
    /// </summary>
    public class Fruit
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Quantity In Stock
        /// </summary>
        [Display(Name = "Quantity In Stock")]
        public int QuantityInStock { get; set; }

        /// <summary>
        /// Updated Date
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "Updated Date")]
        public DateTime UpdatedDate { get; set; }
    }
}
