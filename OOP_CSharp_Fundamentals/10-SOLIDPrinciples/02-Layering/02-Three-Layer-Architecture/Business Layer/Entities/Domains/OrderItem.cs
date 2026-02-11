using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeLayerArchitecture.BusinessLayer.Domains
{
    public class OrderItem
    {
        public Guid ProductId { get; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Total => UnitPrice * Quantity;

        public OrderItem(Guid productId, string productName, decimal unitPrice, int quantity)
        {
            if (productId == Guid.Empty)
                throw new ArgumentException("Id cannot be empty.", nameof(productId));

            ArgumentException.ThrowIfNullOrWhiteSpace(productName, nameof(productName));

            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(unitPrice, nameof(unitPrice));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity, nameof(quantity));

            ProductId = productId;
            ProductName = productName;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }
    }
}
