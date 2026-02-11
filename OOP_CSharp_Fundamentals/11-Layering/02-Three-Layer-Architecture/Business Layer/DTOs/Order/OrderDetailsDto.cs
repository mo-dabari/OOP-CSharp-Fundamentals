using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeLayerArchitecture.BusinessLayer.Domains;
using ThreeLayerArchitecture.BusinessLayer.DTOs;

namespace ThreeLayerArchitecture.BusinessLayer.DTOs
{
    public record OrderDetailsDto
    (
        Guid OrderId,
        CustomerDetailsDto Customer,
        string FullName,
        decimal Subtotal,
        decimal Tax,
        decimal Discount,
        decimal Total,
        DateTime OrderDate,
        string Status,
        IReadOnlyList<OrderItemCustomerDto> _items
    );
}
