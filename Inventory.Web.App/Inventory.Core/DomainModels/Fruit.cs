using System;

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
        public int QuantityInStock { get; set; }

        /// <summary>
        /// Updated Date
        /// </summary>
        public DateTime UpdatedDate { get; set; }
    }
}
