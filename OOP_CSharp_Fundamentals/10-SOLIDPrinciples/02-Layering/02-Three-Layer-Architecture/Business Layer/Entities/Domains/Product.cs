using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OOP_CSharp_Fundamentals;
using ThreeLayerArchitecture.BusinessLayer.Enums;

namespace ThreeLayerArchitecture.BusinessLayer.Domains
{
    public class Product
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public enProductCategory Category { get; set; }

        public Product(string name, decimal price, int stockQuantity, enProductCategory category)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price, nameof(price));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(stockQuantity, nameof(stockQuantity));

            if (!Enum.IsDefined(typeof(enProductCategory), category) || category == enProductCategory.Undefined)
                throw new ArgumentOutOfRangeException(nameof(enProductCategory), "Invalid product category.");

            Id = Guid.NewGuid();
            Name = name;
            Price = price;
            StockQuantity = stockQuantity;
            Category = category;
        }
    }
}
