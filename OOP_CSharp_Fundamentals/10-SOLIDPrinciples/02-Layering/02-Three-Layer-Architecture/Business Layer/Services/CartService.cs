using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeLayerArchitecture.BusinessLayer.DTOs;
using ThreeLayerArchitecture.BusinessLayer.Interfaces;

namespace ThreeLayerArchitecture.BusinessLayer.Services
{
    public class CartService : ICartService
    {
        public bool AddItem(OrderItemDetailsDto item)
        {
            throw new NotImplementedException();
        }

        public bool Decrease(Guid ProductId)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<OrderItemCustomerDto> GetCartItems(Guid customerId)
        {
            throw new NotImplementedException();
        }

        public bool Increase(Guid ProductId)
        {
            throw new NotImplementedException();
        }

        public bool RemoveItem(Guid ProductId)
        {
            throw new NotImplementedException();
        }

        public decimal GetCartTotal(Guid customerId)
        {
            throw new NotImplementedException();
        }

        public bool ClearCart(Guid customerId)
        {
            throw new NotImplementedException();
        }
    }
}
