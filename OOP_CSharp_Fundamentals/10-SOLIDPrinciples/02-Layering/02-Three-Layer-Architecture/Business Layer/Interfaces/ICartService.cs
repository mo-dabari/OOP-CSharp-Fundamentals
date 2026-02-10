using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeLayerArchitecture.BusinessLayer.DTOs;

namespace ThreeLayerArchitecture.BusinessLayer.Interfaces
{
    public interface ICartService
    {
        IReadOnlyList<OrderItemCustomerDto> GetCartItems(Guid customerId);
        bool AddItem(OrderItemDetailsDto item);
        bool RemoveItem(Guid ProductId);

        bool Increase(Guid ProductId);
        bool Decrease(Guid ProductId);

        decimal GetCartTotal(Guid customerId);

        bool ClearCart(Guid customerId);
    }
}
