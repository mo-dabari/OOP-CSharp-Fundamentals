using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeLayerArchitecture.BusinessLayer.Domains;
using ThreeLayerArchitecture.BusinessLayer.DTOs;

namespace ThreeLayerArchitecture.BusinessLayer.Interfaces
{
    public interface IOrderService
    {
        bool CreateOrder(Guid customerId, List<OrderItemDetailsDto> items);
        bool CancelOrder(Guid orderId);
        bool ConfirmOrder(Guid orderId);
        IEnumerable<OrderDetailsDto> GetAllOrders();
        IEnumerable<OrderCustomerDto> GetCustomerOrders(Guid customerId);
    }
}
