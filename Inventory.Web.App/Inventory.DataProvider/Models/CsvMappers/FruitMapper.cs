using CsvHelper.Configuration;
using Inventory.Core.DomainModels;

namespace Inventory.DataProvider.Models.CsvMappers
{
    /// <summary>
    /// Fruit Domain Model To CSV Mapper
    /// </summary>
    public class FruitMap : ClassMap<Fruit>
    {
        public FruitMap()
        {
            Map(m => m.Name).Name("fruit");
            Map(m => m.Price).Name("price");
            Map(m => m.QuantityInStock).Name("quantity_in_stock");
            Map(m => m.UpdatedDate).Name("updated_date");
        }
    }
}
