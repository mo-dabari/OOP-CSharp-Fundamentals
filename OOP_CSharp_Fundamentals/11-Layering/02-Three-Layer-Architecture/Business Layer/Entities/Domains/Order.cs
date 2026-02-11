using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OOP_CSharp_Fundamentals;
using ThreeLayerArchitecture.BusinessLayer.Enums;

namespace ThreeLayerArchitecture.BusinessLayer.Domains
{
    public class Order
    {
        public Guid Id { get; }
        public Guid CustomerId { get; set; }
        public DateTime OrderDate { get; }
        public enOrderStatus Status { get; set; }
        private List<OrderItem> _items;

        public IReadOnlyList<OrderItem> ItemsValues { get; }

        public Order(Guid customerId)
        {
            if (customerId == Guid.Empty)
                throw new ArgumentException("Id cannot be empty.", nameof(customerId));

            Id = Guid.NewGuid();
            CustomerId = customerId;
            OrderDate = DateTime.UtcNow;
            Status = enOrderStatus.Pending;
            _items = new();
            ItemsValues = _items.AsReadOnly();
        }

        public decimal Subtotal => ItemsValues.Sum(i => i.Total);
        public decimal Tax { get; set; }
        public decimal Discount { get; set; }
        public decimal Total => Subtotal + Tax - Discount;

        public void AddItem(OrderItem item)
        {
            ArgumentNullException.ThrowIfNull(item, nameof(item));
            _items.Add(item);
        }
    }
}
