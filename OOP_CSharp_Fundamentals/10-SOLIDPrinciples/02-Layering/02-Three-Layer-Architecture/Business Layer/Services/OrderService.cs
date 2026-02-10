using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeLayerArchitecture.BusinessLayer.DTOs;
using ThreeLayerArchitecture.BusinessLayer.Interfaces;

namespace ThreeLayerArchitecture.BusinessLayer.Services
{
    public class OrderService : IOrderService
    {
        public bool CancelOrder(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public bool ConfirmOrder(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public bool CreateOrder(Guid customerId, List<OrderItemDetailsDto> items)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderDetailsDto> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderCustomerDto> GetCustomerOrders(Guid customerId)
        {
            throw new NotImplementedException();
        }
    }
}
